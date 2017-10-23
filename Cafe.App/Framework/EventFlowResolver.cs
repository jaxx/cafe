using System;
using System.Reflection;
using Cafe.Util.Settings;
using EventFlow;
using EventFlow.Configuration;
using EventFlow.Core;
using EventFlow.Extensions;
using EventFlow.MetadataProviders;
using EventStore.ClientAPI;

namespace Cafe.App.Framework
{
    public static class EventFlowResolver
    {
        public static IRootResolver Resolve(EventFlowSettings eventFlowSettings)
        {
            return EventFlowOptions
                .New
                .RegisterServices(sr => sr.Register<IJsonSerializer, EventStoreJsonSerializer>())
                .AddDefaults(typeof(Domain.Events.TabOpenedEvent).GetTypeInfo().Assembly)
                .AddDefaults(typeof(Application.Commands.OpenTabCommand).GetTypeInfo().Assembly)
                .AddMetadataProvider<AddGuidMetadataProvider>()
                .UseEventStoreEventStore(new Uri(eventFlowSettings.ConnectionString), ConnectionSettings.Create())
                .CreateResolver();
        }
    }
}