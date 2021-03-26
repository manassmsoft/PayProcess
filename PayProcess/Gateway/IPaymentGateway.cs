using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PayProcess.Models.DTO;

namespace PayProcess
{
    public interface IPaymentGateway
    {
        PayStateDto ProcessPayment(PayRequestDto payRequest);

    }
}