using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PayProcess.Models.DTO;
using PayProcess.Services;
using AutoMapper;

namespace PayProcess.Controllers
{
 
    public class ValuesController:ApiController 
    {
        readonly IPaymentRequestService ipay;
       

        public ValuesController(IPaymentRequestService paymentRequestService)
        {
            this.ipay = paymentRequestService;
        }
      
       [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        public string Get(int id)
        {
            return "value";
        }
        [HttpPost]
     
        public HttpResponseMessage Post(PayRequestDto PayReq)
        {
            
            try
            {
                if (ModelState.IsValid)
                {
                    var payState = ipay.ProcessPayment(PayReq);
                    var payResponse = new PayResponseDto()
                    {
                        IsProcessed = payState.PayState == PayStateEnum.Processed,
                        paystate = payState
                    };
                    if (payResponse.IsProcessed)
                    {

                        return Request.CreateResponse(HttpStatusCode.OK, payResponse);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.InternalServerError, new { error = "Payment could not be processed" });
                    }
                }
                else
                {

                    return Request.CreateErrorResponse(HttpStatusCode.BadGateway, ModelState);
                }

            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

        }

       
        public void Put(int id, [FromBody]string value)
        {
        }

       
        public void Delete(int id)
        {
        }
       
    }
}