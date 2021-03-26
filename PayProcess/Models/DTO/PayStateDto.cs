using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayProcess.Models.DTO
{
    public class PayStateDto
    {
        public PayStateEnum PayState{get;set;}
        public DateTime PaymentStateDate{get;set;}
    }
}