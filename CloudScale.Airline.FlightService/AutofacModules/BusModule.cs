﻿using Autofac;
using Nimbus;
using Nimbus.Configuration;
using Nimbus.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nimbus.Logger.Serilog;

namespace AutofacModules
{
    public class BusModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            var connectionString = Microsoft.WindowsAzure.CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");

            builder.RegisterType<SerilogStaticLogger>()
                   .AsImplementedInterfaces()
                   .SingleInstance();

            // This is how you tell Nimbus where to find all your message types and handlers.
            var messagesAssembly = typeof(CloudScale.Airline.Messages.NewFlightCommand).Assembly;
            //var handlersAssembly = typeof(CloudScale.Airline.Modules).Assembly;
            var nimbusAssembly = typeof(Bus).Assembly; // for stock interceptors

            var handlerTypesProvider = new AssemblyScanningTypeProvider(ThisAssembly, nimbusAssembly, messagesAssembly);

            builder.RegisterNimbus(handlerTypesProvider);
            builder.Register(componentContext => new BusBuilder()
                                 .Configure()
                                 .WithConnectionString(connectionString)
                                 .WithNames("CloudScale.Airline.FlightService", Environment.MachineName)
                                 .WithTypesFrom(handlerTypesProvider)
                                 .WithAutofacDefaults(componentContext)
                                 .Build())
                   .As<IBus>()
                   .AutoActivate()
                   .OnActivated(c => c.Instance.Start())
                   .SingleInstance();
        }
    }
}