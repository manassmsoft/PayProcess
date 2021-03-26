using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PayProcess.Models.DTO.Validation;
using System.Runtime.Serialization;
namespace PayProcess.Models.DTO
{
    public class PayRequestDto
    {
        [Required]
        [CreditCard]
        public string CreditCardNumber { get; set; }
        [Required]
        public string CardHolder { get; set;}
         [DataMember(IsRequired = true)]
        [DateValidation]
        public DateTime ExpirationDate { get; set; }
        [StringLength(maximumLength: 3, MinimumLength = 3)]
        public string SecurityCode { get; set;}
        [DataMember(IsRequired=true)]
        [Range(0, double.MaxValue)]
        public decimal Amount { get; set; }

    }
}