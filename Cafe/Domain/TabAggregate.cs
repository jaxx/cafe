using EventFlow.Aggregates;
using EventFlow.Core;

namespace Cafe.Domain
{
    public class TabId : Identity<TabId>
    {
        public TabId(string value)
            : base(value)
        { }
    }

    public class TabAggregate : AggregateRoot<TabAggregate, TabId>
    {
        public TabAggregate(TabId id)
            : base(id)
        { }
    }
}