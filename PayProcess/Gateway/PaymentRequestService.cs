using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PayProcess.Models.DTO;
using PayProcess.Models.Entity.Repository;
using PayProcess.Models.Entity;
using PayProcess.Services.Profile;
namespace PayProcess.Services
{
    public class PaymentRequestService:IPaymentRequestService
    {
      // private readonly IMapper map;
        private readonly ICheapPaymentGateway cheapPayGateway;
        private readonly IExpensivePaymentGateway expensivePayGateway;
        private readonly IPaymentRepository payRepo;
        private readonly IPaymentStateRepository payStateRepo;

        public PaymentRequestService(ICheapPaymentGateway ICheap, IExpensivePaymentGateway IExp, IPaymentRepository IpayRepo, IPaymentStateRepository payStRepo)
        {
           // map = mapper;
            cheapPayGateway = ICheap;
            expensivePayGateway = IExp;
            payRepo = IpayRepo;
            payStateRepo = payStRepo;
            
        }

        public PayStateDto ProcessPayment(PayRequestDto PayReqDto)
        {
            var payEntity = Mapper.Map<PayRequestDto, Payment>(PayReqDto);
           // payEntity = payRepo.Create(payEntity);
            var payStateEntity = new PaymentState() { Payment = payEntity, PayId = payEntity.PayId, CreatedDate = DateTime.Now, State = PayStateEnum.Pending.ToString() };
            payStateEntity = payStateRepo.Create(payStateEntity);
            if (PayReqDto.Amount <= 20)
            {
                var Paystdto = ProcessPaymentStateDto(cheapPayGateway, PayReqDto, payEntity);
                return Paystdto;
            }
            else if (PayReqDto.Amount > 20 && PayReqDto.Amount <= 500)
            {
               
                PayStateDto PayStDto = new PayStateDto() { PayState = PayStateEnum.Failed, PaymentStateDate = DateTime.Now };
                int counter = 1;
                try {
                    PayStDto = ProcessPaymentStateDto(expensivePayGateway, PayReqDto, payEntity);
                        if(PayStDto!=null && PayStDto.PayState==PayStateEnum.Processed)
                        {
                            return PayStDto;
                        }
                        else
                        {
                            counter++;
                           PayStDto = ProcessPaymentStateDto(cheapPayGateway, PayReqDto, payEntity);
                           return PayStDto;
                        }
                }
                catch(Exception)
                    {
                        if (counter == 0)
                        {
                            PayStDto = ProcessPaymentStateDto(cheapPayGateway, PayReqDto, payEntity);
                            return PayStDto;
                        }
                    }
                return PayStDto;
            }
            else{
                int counter = 0;
                PayStateDto payStDto = new PayStateDto() { PayState = PayStateEnum.Failed, PaymentStateDate = DateTime.Now };
                while (counter < 3)
                {
                    
                    try
                    {
                        payStDto = ProcessPaymentStateDto(expensivePayGateway, PayReqDto, payEntity);
                        if (payStDto != null && payStDto.PayState == PayStateEnum.Processed)
                        {
                            return payStDto;
                        }
                    }
                    catch (Exception)
                    {

                    }
                    finally {
                        counter++;
                    }

                }
                return payStDto;
            }
            throw new Exception("Payment could not be processed");
        }
      
        private PayStateDto ProcessPaymentStateDto(IPaymentGateway payGateway,PayRequestDto payReqDto,Payment payEntity)
        {
            var paymentSateDto = payGateway.ProcessPayment(payReqDto);
            var payStEntity = new PaymentState() {Payment=payEntity,PayId=payEntity.PayId,CreatedDate=paymentSateDto.PaymentStateDate,State=paymentSateDto.PayState.ToString()};
            payStEntity = payStateRepo.Create(payStEntity);
            return paymentSateDto;
        }
    }
}