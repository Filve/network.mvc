using System.Net.Sockets;
using System.Net;

namespace network.mvc.Models
{
    public class AntiSabotage
    {
        public static void Antisabotage(string ipAddress, int portNumber)
        {
            IPAddress ip = IPAddress.Parse(ipAddress); // Replace with the IP address of your server
            int port = portNumber; // Replace with the port number you want to use
            TcpListener listener = new TcpListener(ip, port);
            //listener.Stop();
            //listener.Start();

            Console.WriteLine("Waiting for incoming connections...");

            while (true)
            {
                //TcpClient client = listener.AcceptTcpClient();
                Console.WriteLine("Client connected!");

                Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClient));
                clientThread.Start(listener.AcceptTcpClient());
            }
        }

        static void HandleClient(object obj)
        {
            TcpClient client = (TcpClient)obj;
            NetworkStream stream = client.GetStream();
            byte[] data = new byte[1024];
            string message = null;

            while (true)
            {
                int bytesRead = stream.Read(data, 0, data.Length);
                message = System.Text.Encoding.ASCII.GetString(data, 0, bytesRead);
                Console.WriteLine("Received: {0}", message);

                if (message.ToLower() == "exit")
                {
                    break;
                }

                byte[] reply = System.Text.Encoding.ASCII.GetBytes("Message received!");
                stream.Write(reply, 0, reply.Length);
                Console.WriteLine("Sent: {0}", "Message received!");
            }

            client.Close();
        }
    }
}
