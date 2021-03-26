using AutoMapper;
using PayProcess.Controllers;
using PayProcess.Models.Entity.Repository;
using PayProcess.Services;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace PayProcess
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            container.RegisterType<IMapper,Mapper>();
            container.RegisterType<IExpensivePaymentGateway, ExpensivePaymentGateway>();
            container.RegisterType<IPaymentRepository,PaymentRepository>();
            container.RegisterType<IPaymentStateRepository,PaymentStateRepository>();
            container.RegisterType<ICheapPaymentGateway,CheapPaymentGateway>();
            container.RegisterType<IPaymentRepository,PaymentRepository>();
            container.RegisterType<IPaymentStateRepository, PaymentStateRepository>();
            container.RegisterType<IPaymentRequestService, PaymentRequestService>();
            container.RegisterType<ApiController, ValuesController>("Values");
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
          
        }

       

    }
}