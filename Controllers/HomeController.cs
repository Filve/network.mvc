using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using network.mvc.Models;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Security;
using System.Net.Sockets;


namespace network.mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var networkAddressInterface = NetworkInterface.GetAllNetworkInterfaces();
            var currentIp = GetCurrentIPAddress.GetCurrentIP();
            var addressIp = Address.GetIpAddress();
            var portAddressIp = GetOpenPorts.Ports(currentIp);
            var stream = new MemoryStream();
            var networking = NetworkStream.Null;
            networking = NetworkStream.Synchronized(stream);
            var publicAddress = Address.GetPublicIPAddress();
            var serverScanner = ServerScanner.Scan;
            var socketExample = SocketExample.Socket;
            //AntiSabotage.Antisabotage(currentIp, portAddressIp);
            DeAnon.De();

            //var ip = IPAddress.Parse();
            var address = new SocketAddress(AddressFamily.Ipx);
            var addressFamily = address.Family;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}