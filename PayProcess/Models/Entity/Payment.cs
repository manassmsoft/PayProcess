using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PayProcess.Models.Entity
{
    [Table("Payment")]
    public class Payment
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long PayId { get; set; }
        [Required]
        public string CreditCardNumber { get; set;}
        [Required]
        public string CardHolder { get; set; }
        [Required]
        public DateTime ExpirationDate { get; set;}
        [MaxLength(3)]
        public string SecurityCode { get; set;}
        [Required]
        //[Column("Amount", TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        [InverseProperty("Payment")]
        public virtual ICollection<PaymentState> PaymentStates { get; set; }
    
    }
}