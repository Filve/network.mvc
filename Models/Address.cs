using System;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;


namespace network.mvc.Models
{
    public class Address
    {
        public static void Main(string[] args)
        {
            string publicIPAddress = GetPublicIPAddress();
            Console.WriteLine("L'indirizzo IP pubblico è: " + publicIPAddress);
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
        public static void Scan()
        {
            // Intervallo di porte da scannerizzare
            int startPort = 1;
            int endPort = 65535;

            // Indirizzo IP del target
            string targetIP = Address.GetPublicIPAddress();

            Console.WriteLine("Inizio scansione server...");

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
                        Console.WriteLine("Porta aperta trovata: " + port);
                    }
                }
                catch (SocketException)
                {
                    // La porta è chiusa o non è possibile connettersi
                }
            }

            Console.WriteLine("Scansione completata.");
        }
    }


}
