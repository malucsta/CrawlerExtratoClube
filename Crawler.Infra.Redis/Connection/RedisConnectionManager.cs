using StackExchange.Redis;

namespace Crawler.Infra.Redis.Connection
{
    public class RedisConnectionManager
    {
        private readonly Lazy<ConnectionMultiplexer> lazyConnection;

        public RedisConnectionManager(RedisSettings settings)
        {
            lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
            {
                return ConnectWithRetry(settings);
            });
        }

        public ConnectionMultiplexer GetConnection() => lazyConnection.Value;

        private static ConnectionMultiplexer ConnectWithRetry(RedisSettings settings)
        {
            int retryCount = 0;
            while (retryCount < settings.MaxRetryConnectionAttempts)
            {
                try
                {
                    return ConnectionMultiplexer.Connect(settings.ConnectionString, (conn) => { conn.AbortOnConnectFail = true; });
                }
                catch (Exception)
                {
                    retryCount++;
                    Console.WriteLine($"Trying to connect to redis. Attempt: {retryCount}");

                    if (retryCount <= settings.MaxRetryConnectionAttempts)
                        Thread.Sleep((int)Math.Pow(2, settings.InitialRetryDelayInSeconds));
                }
            }

            throw new Exception($"Failed to connect to Redis after {settings.MaxRetryConnectionAttempts} attempts.");
        }
    }
}
