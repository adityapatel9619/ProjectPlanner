namespace ProjectPlanner
{
    public class ConnectionStringProvider
    {
        public string GetConnection { get; }

        public ConnectionStringProvider(string connectionString)
        {
            GetConnection = connectionString;
        }
    }
}
