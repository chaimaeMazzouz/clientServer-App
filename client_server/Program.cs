using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace client_server
{
    class Program
    {
        static void Main(string[] args)
        {

            // 1- Create Socket
            Socket m_ListenSockeet;
            m_ListenSockeet = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // 2- Bind the socket
            // Set port number
            int iPort = 2020;     // 2020 cause it was such a great year "just kidding"  
            IPEndPoint m_LocalIPEndPoint = new IPEndPoint(IPAddress.Any, iPort);
            // Bind
            m_ListenSockeet.Bind(m_LocalIPEndPoint);   

            // 3- display IP Address and Port Number
            Console.WriteLine(" Server IP Address : " + LocalIpAdress());
            Console.WriteLine(" Listening on Port : " + iPort);

            // 4- Start Listening
            m_ListenSockeet.Listen(4);

            // 5- Accept incoming connection
            Socket m_AcceptedSocket = m_ListenSockeet.Accept();

            // 6- Start Receiving (Can send too)
            byte[] ReceiveBuffer = new byte[1024];
            int iReceiveByteCount;

            iReceiveByteCount = m_AcceptedSocket.Receive(ReceiveBuffer, SocketFlags.None);
            string message = Encoding.ASCII.GetString(ReceiveBuffer, 0, iReceiveByteCount);
            Console.WriteLine(message);

            while (message != "Exit")
            {
                iReceiveByteCount = m_AcceptedSocket.Receive(ReceiveBuffer, SocketFlags.None);

                if (iReceiveByteCount > 0)
                {
                    //Display the message
                    message = Encoding.ASCII.GetString(ReceiveBuffer, 0, iReceiveByteCount);
                    Console.WriteLine(message);
                }
            }
            // 7- Shutdown the socket
            m_AcceptedSocket.Shutdown(SocketShutdown.Both);

            // 8- Close the socket
            m_AcceptedSocket.Close();
        }
        public static string LocalIpAdress()
        {
            IPHostEntry host;
            string localIP = "";
            //get the name of the current host
            host = Dns.GetHostEntry(Dns.GetHostName());                 
            foreach (IPAddress ip in host.AddressList)
            {
                //Find the IP which maches internetwork
                if (ip.AddressFamily == AddressFamily.InterNetwork)     
                {
                    localIP = ip.ToString();
                    break;
                }
            }
            return localIP;
        }
    }
}
