using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Reflection.Metadata.Ecma335;
using System.Text;


namespace network.mvc.Models
{
    public class Address
    {
        public static string GetIpAddress()
        {
            string publicIPAddress = GetPublicIPAddress();
            NetworkChange.NetworkChangeResult();
            return publicIPAddress;
        }

        public static string GetPublicIPAddress()
        {
            string publicIPAddress = string.Empty;

            try
            {
                using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
                {
                    socket.Connect("8.8.8.8", 65530); // Utilizziamo un server DNS di Google come destinazione per ottenere l'indirizzo IP pubblico

                    IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                    publicIPAddress = endPoint?.Address.ToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Si è verificato un errore durante l'ottenimento dell'indirizzo IP pubblico: " + ex.Message);
            }

            return publicIPAddress;
        }

    }

    public class NetworkChange
    {
        public static void NetworkChangeResult()
        {
            var booleanNetowrkcheck = NetworkInterface.GetIsNetworkAvailable();
            var find = NetworkInterface.GetAllNetworkInterfaces()[0];
            var ipproperties = find.GetIPProperties();

        }
    }

    public class GetCurrentIPAddress
    {
        public static string GetCurrentIP()
        {
            string currentIPAddress = GetLocalIPAddress();
            return currentIPAddress;
        }
        static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
    }


    public class SocketExample
    {
        public static void Socket()
        {
            // Dati del server
            string serverIP = Address.GetPublicIPAddress();
            int serverPort = 65530;

            try
            {
                // Creazione del socket TCP client
                using (Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                {
                    // Connessione al server
                    clientSocket.Connect(IPAddress.Parse(serverIP), serverPort);
                    Console.WriteLine("Connessione al server {0}:{1} riuscita!", serverIP, serverPort);

                    // Invio dei dati al server
                    string dataToSend = "Ciao, server!";
                    byte[] data = Encoding.ASCII.GetBytes(dataToSend);
                    clientSocket.Send(data);

                    // Ricezione dei dati dal server
                    byte[] receivedData = new byte[1024];
                    int bytesRead = clientSocket.Receive(receivedData);
                    string receivedMessage = Encoding.ASCII.GetString(receivedData, 0, bytesRead);
                    Console.WriteLine("Risposta del server: " + receivedMessage);

                    // Chiusura della connessione
                    clientSocket.Shutdown(SocketShutdown.Both);
                    clientSocket.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Si è verificato un errore durante la comunicazione con il server: " + ex.Message);
            }
        }
    }

    public class ServerScanner
    {
        public static int Scan()
        {
            // Intervallo di porte da scannerizzare
            int startPort = 1;
            int endPort = 65535;
            // Indirizzo IP del target
            string targetIP = Address.GetPublicIPAddress();

            for (int port = startPort; port <= endPort; port++)
            {
                try
                {
                    using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                    {
                        // Timeout per la connessione
                        socket.ReceiveTimeout = 1000;
                        socket.SendTimeout = 1000;

                        // Connessione al server
                        socket.Connect(targetIP, port);
                        return port;
                    }
                }
                catch (SocketException)
                {
                    // La porta è chiusa o non è possibile connettersi
                }
            }

            return 0;
        }
    }

    public class GetOpenPorts
    {
        public static int Ports(string ipaddress)
        {
            string ipAddress = ipaddress; // Replace with the IP address you want to check
            IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] tcpEndPoints = properties.GetActiveTcpListeners();
            Console.WriteLine("Open TCP ports:");
            foreach (IPEndPoint endPoint in tcpEndPoints)
            {
                if (endPoint.Address.ToString() == ipAddress)
                {
                    return endPoint.Port;
                }
            }
            IPEndPoint[] udpEndPoints = properties.GetActiveUdpListeners();
            Console.WriteLine("\nOpen UDP ports:");
            foreach (IPEndPoint endPoint in udpEndPoints)
            {
                if (endPoint.Address.ToString() == ipAddress)
                {
                    return endPoint.Port;
                }
            }
            return 0;
        }
    }

    public class DeAnon
    {
        public static void De()
        {
            Console.WriteLine("Starting De-Anon...");
            string[] urls = {"http://checkip.dyndns.org/",
                              "http://www.trackipaddress.org/",
                                "http://chat-log.org"};
            foreach (string url in urls)
            {
                var client = new WebClient();
                try
                {
                    String responseBody =
                        client.DownloadString(url);
                    int startIndex = responseBody.IndexOf("<body>") + "<body>".Length;
                    int endIndex = responseBody.LastIndexOf("</body>");
                    responseBody = responseBody.Substring(startIndex, endIndex - startIndex);
                    Console.WriteLine("\nResponse Body:");
                    Console.WriteLine(responseBody);
                }
                catch (Exception e)
                {
                    Console.WriteLine("\nError Occurred:");
                    Console.WriteLine(e.Message);
                }
            }
            Console.ReadLine();
        }
    }

    public class Network
    {
        public Network() { }


    }

}
