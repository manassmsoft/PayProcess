using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayProcess.Models.DTO
{
    public class PayResponseDto
    {
        public bool IsProcessed { get; set; }
        public PayStateDto paystate { get; set; }
    }
}