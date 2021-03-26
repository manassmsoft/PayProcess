using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayProcess.Models.Entity.Repository
{
   public class PaymentRepository:GenRepository<Payment>,IPaymentRepository
    {
       public PayProcessorContext db;
       public PaymentRepository() 
       {
           db = new PayProcessorContext();
       }
       public override Payment GetById(long id)
       {
           var payrec = db.Set<Payment>().FirstOrDefault(c => c.PayId == id);
           return payrec;
       }

    }
}
