using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PayProcess.Models.DTO;
namespace PayProcess.Services
{
   public interface IPaymentRequestService
    {
       PayStateDto ProcessPayment(PayRequestDto PayReqDto);
    }
}
