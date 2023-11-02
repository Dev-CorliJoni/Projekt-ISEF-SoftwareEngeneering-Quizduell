using Microsoft.Extensions.Logging;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace Quixduell.ServiceLayer.Helper
{
    public class DBHelper
    {
        /// <summary>
        /// Gets Server from Connection String and wait for Database to become available 
        /// Check if Port is open for Connection
        /// </summary>
        /// <param name="ConnectingString">SQL Connection String</param>
        /// <param name="logger">Logger for Debugging</param>
        /// <param name="Port">Port, if not given in Connection String</param>
        /// <param name="Retry">How many Connection attempts</param>
        public static bool WaitForSQLDB(string ConnectingString, ILogger logger, int Port = 3306, int Retry = 100)
        {
            int Start = ConnectingString.IndexOf("Server=") + 7;
            int End = ConnectingString.IndexOf(";", ConnectingString.IndexOf("Server=")) - (ConnectingString.IndexOf("Server=") + 7);

            string Server = ConnectingString.Substring(Start, End);

            //Port in Connection String
            if (Server.Contains(","))
            {
                Port = int.Parse(Server.Substring(Server.IndexOf(",") + 1));
                Server = Server.Substring(0, Server.IndexOf(","));
            }

            int i = 1;

            while (i < Retry)
            {
                logger.LogInformation($"Try to connect to Server: {Server}... ({i} - {Retry})");
                Ping PingSender = new Ping();
                try
                {
                    var Reply = PingSender.Send(Server);

                    if (Reply.Status == IPStatus.Success)
                    {
                        logger.LogInformation($"Ping Success, try to connect to Port: {Port}... ({i})");
                        using (TcpClient tcpClient = new TcpClient())
                        {
                            try
                            {
                                tcpClient.Connect(Reply.Address.ToString(), Port);
                                logger.LogInformation("Port open");
                                return true;
                            }
                            catch
                            {
                                logger.LogInformation("Port closed Wait and Try again... ");
                                Thread.Sleep(4000);
                                i++;
                            }
                        }
                    }
                }
                catch
                {
                    logger.LogInformation("Wait and Try again... ");
                    Thread.Sleep(500);
                    i++;
                }


            }
            logger.LogError("No connection to Database: {DBServer} on Port: {DBServerPort}", Server, Port);
            return false;

        }
    }
}
