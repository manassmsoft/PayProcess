using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;


namespace PayProcess.Models.Entity
{
   [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class PayProcessorContext:DbContext
    {
        public PayProcessorContext(): base("DefaultConnectionString")
        { 
        
        }
        //providerName="MySql.Data.MySqlClient"
        public DbSet<Payment> Payments{get;set;}
        public DbSet<PaymentState> PaymentStates { get; set;}
    }
}