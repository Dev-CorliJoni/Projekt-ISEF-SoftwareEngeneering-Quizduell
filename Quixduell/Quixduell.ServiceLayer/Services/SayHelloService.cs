namespace Quixduell.ServiceLayer.Services
{
    /// <summary>
    /// <c>SayHelloService</c> is used as an example to show code documentation
    /// </summary>
    public class SayHelloService
    {
        /// <summary>
        /// Variables need to be descriped 
        /// </summary>
        private readonly string _serviceName = nameof(SayHelloService);


        /// <summary>
        /// Construct hello String 
        /// </summary>
        /// <exception cref="ArgumentNullException">Throws when value is null</exception>
        /// <param name="toUser">Name of user</param>
        /// <returns>Hello String</returns>
        public string SayHello(string toUser)
        {
            ArgumentNullException.ThrowIfNull(toUser);
            return $"Hello from {_serviceName} to {toUser}";
        }

    }
}