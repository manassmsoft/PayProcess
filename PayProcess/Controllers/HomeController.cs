using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PayProcess.Models.DTO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Net.Http.Formatting; 
namespace PayProcess.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
           
            return View();
        }
        [HttpPost]
        public ActionResult Index(PayRequestDto payObj)
        {
          var respp = payProcess(payObj);
          TempData["msgDATA"] = respp.Result.StatusCode.ToString();
           return RedirectToAction("index","Home");
        }
        public static async Task<HttpResponseMessage> payProcess(PayRequestDto pay)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:3884/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = (HttpResponseMessage) client.PostAsJsonAsync("/api/values", pay).Result;
            return response;
        }
    }
}
