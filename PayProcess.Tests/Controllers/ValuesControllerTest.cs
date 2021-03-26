using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PayProcess;
using PayProcess.Services;
using PayProcess.Models.DTO;
using PayProcess.Models.Entity;
using PayProcess.Models.Entity.Repository;
using Moq;
using AutoMapper;
using NUnit.Framework;
using NUnit.Framework.Internal;


namespace PayProcess.Tests.Controllers
{
    [TestFixture]
    public class ValuesControllerTest
    {
        IPaymentRequestService PayReqService;
        Mock<ICheapPaymentGateway> cheapPayGateway;
        Mock<IExpensivePaymentGateway> expensivePayGateway;
        Mock<IMapper> mapper;
        Mock<IPaymentRepository> payRepository;
        Mock<IPaymentStateRepository> PayStateRepository;
        [SetUp]
        public void Setup()
        {
            cheapPayGateway = new Mock<ICheapPaymentGateway>();
            expensivePayGateway = new Mock<IExpensivePaymentGateway>();
            mapper = new Mock<IMapper>();
            payRepository = new Mock<IPaymentRepository>();
            PayStateRepository = new Mock<IPaymentStateRepository>();

            PayReqService = new PaymentRequestService(cheapPayGateway.Object, expensivePayGateway.Object, payRepository.Object, PayStateRepository.Object);
            mapper.Setup(c => c.Map<PayRequestDto, Payment>(It.IsAny<PayRequestDto>())).Returns((PayRequestDto pr) => new Payment() { Amount = pr.Amount, CardHolder = pr.CardHolder, CreditCardNumber = pr.CreditCardNumber, ExpirationDate = pr.ExpirationDate, SecurityCode = pr.SecurityCode });

            payRepository.Setup(c => c.Create(It.IsAny<Payment>())).Returns((Payment payEntity) => payEntity);
            PayStateRepository.Setup(c => c.Create(It.IsAny<PaymentState>())).Returns((PaymentState payStateEntity) => payStateEntity);

        }
        [Test, TestCaseSource(typeof(PayRequestServiceTestData), "PayRequestServiceTestData.Tests")]
        public void TestpayReqService(PayRequestDto payReqDto,PayStateDto cheapGateResDto,int cheapGateCount,PayStateDto expGateResponseDto,int ExpGateCount,PayStateEnum expPayStateEnum)
        {
            cheapPayGateway.Setup(c=>c.ProcessPayment(payReqDto)).Returns(cheapGateResDto);
            expensivePayGateway.Setup(c=>c.ProcessPayment(payReqDto)).Returns(expGateResponseDto);
            var payStateDto = PayReqService.ProcessPayment(payReqDto);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(payStateDto);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(payStateDto.PayState,expPayStateEnum);
            cheapPayGateway.Verify(c=>c.ProcessPayment(payReqDto),Times.Exactly(cheapGateCount));
            expensivePayGateway.Verify(c=>c.ProcessPayment(payReqDto),Times.Exactly(ExpGateCount));
        }
      
    }

     [SetUpFixture]
    public static class PayRequestServiceTestData
    {
        public static PayStateDto processPayStateDto {
            get { return new PayStateDto() { PayState = PayStateEnum.Processed, PaymentStateDate = DateTime.Now }; }
        }
        public static PayStateDto failedPayStateDto
        {
            get { return new PayStateDto() { PayState = PayStateEnum.Failed, PaymentStateDate = DateTime.Now }; }
        }
        public static PayRequestDto FirstRequest { get { return new PayRequestDto() { Amount = 26, CardHolder = "Joy Philip", CreditCardNumber = "5100 2900 2900 2909",ExpirationDate=DateTime.Now.AddYears(3),SecurityCode="1234567895" }; } }
        public static PayRequestDto SecondRequest { get { return new PayRequestDto() { Amount = 16, CardHolder = "Ben Jonson", CreditCardNumber = "5100 2900 2900 2909", ExpirationDate = DateTime.Now.AddYears(3), SecurityCode = "123" }; } }
        public static PayRequestDto ThirdRequest { get { return new PayRequestDto() { Amount = 510, CardHolder = "Billy Graham", CreditCardNumber = "5100 2900 2900 2909", ExpirationDate = DateTime.Now.AddYears(3), SecurityCode = "123" }; } }
     
        public static IEnumerable<TestCaseData> Tests
        {
           
            get {
                yield return new TestCaseData(FirstRequest, processPayStateDto, 0, processPayStateDto, 1, PayStateEnum.Processed).SetName("First_CheapProcessed_ExpensiveProcessed");
                yield return new TestCaseData(FirstRequest, failedPayStateDto, 0, processPayStateDto, 1, PayStateEnum.Processed).SetName("First_CheapFailed_ExpensiveProcessed");
                yield return new TestCaseData(FirstRequest, failedPayStateDto, 1, failedPayStateDto, 1, PayStateEnum.Failed).SetName("First_CheapFailed_ExpensiveFailed");

                yield return new TestCaseData(SecondRequest, processPayStateDto, 0, processPayStateDto, 0, PayStateEnum.Processed).SetName("Second_CheapProcessed_ExpensiveProcessed");
                yield return new TestCaseData(SecondRequest, failedPayStateDto, 0, processPayStateDto, 0, PayStateEnum.Processed).SetName("Second_CheapFailed_ExpensiveProcessed");
                yield return new TestCaseData(SecondRequest, failedPayStateDto, 1, failedPayStateDto, 0, PayStateEnum.Failed).SetName("Second_CheapFailed_ExpensiveFailed");

                yield return new TestCaseData(ThirdRequest, processPayStateDto, 0, processPayStateDto, 1, PayStateEnum.Processed).SetName("Third_CheapProcessed_ExpensiveProcessed");
                yield return new TestCaseData(ThirdRequest, failedPayStateDto, 0, processPayStateDto, 1, PayStateEnum.Processed).SetName("Third_CheapFailed_ExpensiveProcessed");
                yield return new TestCaseData(ThirdRequest, failedPayStateDto, 0, failedPayStateDto, 3, PayStateEnum.Failed).SetName("Third_CheapFailed_ExpensiveFailed");

            
             }
        }
    }
}
