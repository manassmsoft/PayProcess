using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PayProcess.Models.Entity
{
    [Table("PaymentState")]
    public class PaymentState
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long PaymentStateId { get; set;}
        [Required]
        public string State { get; set; }
        [Required]
        [Column("CreatedDate",TypeName="datetime")]
        public DateTime CreatedDate { get; set;}
        public long PayId { get; set; }
        [ForeignKey("PayId")]
        public Payment Payment{ get; set; }
     }
}