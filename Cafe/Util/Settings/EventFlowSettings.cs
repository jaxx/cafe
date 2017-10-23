namespace Cafe.Util.Settings
{
    public sealed class EventFlowSettings
    {
        public string ConnectionString { get; }

        public EventFlowSettings(string connectionString)
        {
            ConnectionString = connectionString;
        }
    }
}