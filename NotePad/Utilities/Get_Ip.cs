using System.Net;
namespace NotePad.Utilities
{
    public class Get_Ip
    {
        private readonly IHttpContextAccessor _accessor;

        public Get_Ip( IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }
        public async Task<string> Invoke(HttpContext context)
        {

            IPAddress remoteIpAddress = _accessor.HttpContext.Connection.RemoteIpAddress;
            string result = "";
            if (remoteIpAddress != null)
            {
                // If we got an IPV6 address, then we need to ask the network for the IPV4 address 
                // This usually only happens when the browser is on the same machine as the server.
                if (remoteIpAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                {
                    remoteIpAddress = System.Net.Dns.GetHostEntry(remoteIpAddress).AddressList
            .First(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
                }
                result = remoteIpAddress.ToString();
            }
            return result;
        }
    }
}
