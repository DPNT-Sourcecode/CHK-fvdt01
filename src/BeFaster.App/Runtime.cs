using BeFaster.Core.Data;
using BeFaster.Core.Factories;
using BeFaster.Core.Services;
using BeFaster.Data;
using BeFaster.Domain.Cqrs;
using BeFaster.Domain.DSL;
using BeFaster.Domain.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;

namespace BeFaster.App
{
    public class Runtime
    {        
        private static IServiceProvider _serviceProvider;

        public Runtime()
        {
            if(_serviceProvider == null)
                RegisterServices();
        }
        private void RegisterServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton<ILoggerFactory, LoggerFactory>()
                    .AddSingleton(typeof(ILogger<>), typeof(Logger<>))
                    .AddSingleton<ICalculatorService, CalculatorService>()
                    .AddSingleton<IMessageService, MessageService>()
                    .AddSingleton<IProductRepository, ProductRepositoryInMemory>()
                    .AddSingleton<IOfferRepository, OfferRepositoryInMemory>()
                    .AddSingleton<IOfferFactory, OfferFactory>()
                    .AddSingleton<ICartFactory, CartFactory>()
                    .AddSingleton<IOfferService, OfferService>()
                    .AddSingleton<IProductService, ProductService>()
                    .AddSingleton<ICartService, CartService>()
                    .AddSingleton<IGatewayService, GatewayService>()                    
                    .BuildServiceProvider();
                        
            services.AddMediatR(Assembly.GetExecutingAssembly(), Assembly.GetAssembly(typeof(CalculateSumCommand)));            
            services.AddMediatR(Assembly.GetExecutingAssembly(), Assembly.GetAssembly(typeof(HelloCommand)));
            services.AddMediatR(Assembly.GetExecutingAssembly(), Assembly.GetAssembly(typeof(CheckoutCommand)));
            _serviceProvider = services.BuildServiceProvider();            
        }

        public T GetInstance<T>()
        {
            return _serviceProvider.GetService<T>();
        }
    }
}
