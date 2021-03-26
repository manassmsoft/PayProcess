using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayProcess.Models.Entity.Repository
{
    public class PaymentStateRepository:GenRepository<PaymentState>,IPaymentStateRepository
    {
        PayProcessorContext db;
        public PaymentStateRepository()
        {
            db = new PayProcessorContext();
        }
        public override PaymentState GetById(long id)
        {
            var PayState=db.Set<PaymentState>().FirstOrDefault(c=>c.PayId==id);
            return PayState;
        }
    }
}