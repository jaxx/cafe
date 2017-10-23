using EventFlow.Aggregates;
using EventFlow.EventStores;

namespace Cafe.Domain.Events
{
    [EventVersion(nameof(TabOpenedEvent), 1)]
    public class TabOpenedEvent : AggregateEvent<TabAggregate, TabId>
    {
    }
}