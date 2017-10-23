using System;
using System.Linq;
using System.Reflection;
using Cafe.Util.Settings;
using DryIoc;
using EventFlow;
using EventFlow.Configuration;
using EventFlow.Core;
using EventFlow.Extensions;
using EventFlow.MetadataProviders;
using EventStore.ClientAPI;

namespace Cafe.App.DryIoc
{
    public class Bootstrapper
    {
        private readonly IContainer container = new Container();

        private Bootstrapper()
        {
            container.Register<Program>(Reuse.Singleton);

            RegisterSettings();

            //RegisterTypesWithSuffix(typeof(FileDao).Assembly, "Dao");

            // TODO: kuidas seda dryioc-s registreerida???
            EventFlowOptions
                .New
                .RegisterServices(sr => sr.Register<IJsonSerializer, EventStoreJsonSerializer>())
                .AddDefaults(typeof(Domain.Events.TabOpenedEvent).GetTypeInfo().Assembly)
                .AddDefaults(typeof(Application.Commands.OpenTabCommand).GetTypeInfo().Assembly)
                .AddMetadataProvider<AddGuidMetadataProvider>()
                //.UseEventStore(new Uri(connectionString), ConnectionSettings.Create())
                .CreateResolver();
        }

        private IContainer GetContainer()
        {
            return container;
        }

        public static IContainer Bootstrap()
        {
            return new Bootstrapper().GetContainer();
        }

        private void RegisterSettings()
        {
            container.Register<ISettingsProvider, SettingsProvider>(Reuse.Singleton);
            container.RegisterSettings<EventFlowSettings>();
        }

        private void RegisterTypesWithSuffix(Assembly assembly, string suffix)
        {
            var implementingClasses = assembly.GetTypes()
                .Where(type => type.Name.EndsWith(suffix) && type.IsPublic && !type.IsAbstract && type.GetInterfaces().Length != 0);

            foreach (var implementingClass in implementingClasses)
            {
                var iface = implementingClass.GetInterfaces().SingleOrDefault(x => x.Name.EndsWith(implementingClass.Name));
                if (iface != null)
                    container.Register(iface, implementingClass, Reuse.Singleton, PropertiesAndFields.Auto, ifAlreadyRegistered: IfAlreadyRegistered.Keep);
            }
        }
    }
}