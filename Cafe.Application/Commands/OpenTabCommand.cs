using Cafe.Domain;
using EventFlow.Commands;

namespace Cafe.Application.Commands
{
    public class OpenTabCommand : Command<TabAggregate, TabId>
    {
        public int TableNumber { get; set; }
        public string Waiter { get; set; }

        public OpenTabCommand(TabId id) : base(id)
        { }
    }
}