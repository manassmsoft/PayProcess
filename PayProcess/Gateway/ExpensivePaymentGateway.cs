using PayProcess.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayProcess
{
    public class ExpensivePaymentGateway:IExpensivePaymentGateway
    {
        public PayStateDto ProcessPayment(PayRequestDto payReq)
        {
            PayStateDto pys = new PayStateDto();
            Random rd = new Random();
            int res = rd.Next(0, 10);
            int[] array = new int[5] { 2, 4, 6, 8, 10 };
            if (array.Contains(res))
            {
                pys = new PayStateDto() { PayState = PayStateEnum.Processed, PaymentStateDate = DateTime.Now };
            }
            else
            {
                pys = new PayStateDto() { PayState = PayStateEnum.Failed, PaymentStateDate = DateTime.Now };
            }
            return pys;
        }
    }
}