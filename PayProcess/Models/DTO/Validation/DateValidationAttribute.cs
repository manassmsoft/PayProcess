using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PayProcess.Models.DTO.Validation
{
    public class DateValidationAttribute:ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime dt = Convert.ToDateTime(value);
            return dt.Date >= DateTime.Now.Date;
        }
    }
}