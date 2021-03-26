using PayProcess.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Unity;
using PayProcess.Models.Entity.Repository;
using PayProcess.Controllers;

namespace PayProcess
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //var container = new UnityContainer();
            //container.RegisterType<IPaymentRepository, PaymentRepository>();
            //container.RegisterType<IPaymentStateRepository, PaymentStateRepository>();
            //container.RegisterType<IPaymentRequestService, PaymentRequestService>();
            //container.RegisterType<ApiController, ValuesController>("Values");
            //config.DependencyResolver = new UnityResolver(container);
            
            //GlobalConfiguration.Configuration.DependencyResolver =new UnityResolver(container);
            //config.MapHttpAttributeRoutes();
            
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
            // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
            // For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
            //config.EnableQuerySupport();

            // To disable tracing in your application, please comment out or remove the following line of code
            // For more information, refer to: http://www.asp.net/web-api
            config.EnableSystemDiagnosticsTracing();
        }
    }
}
