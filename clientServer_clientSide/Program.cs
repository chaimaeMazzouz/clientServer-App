using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace clientServer_clientSide
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1- Create the socket
            // Input IP address and port
            string serverIP = "";
            string serverPort = "";
            Console.Write("Enter Server IP Address : ");
            serverIP = Console.ReadLine();
            Console.Write("Enter Server Port Number : ");
            serverPort = Console.ReadLine();
            Console.WriteLine("\nSending to : " + serverIP + " : " + serverPort);

            // Create the socket based on date input
            Socket m_sendSocket;
            m_sendSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // 2- Connect to the endpoint (Server)
            // convert string IP and Port into IPAddress and int data types
            IPAddress destinationIP = IPAddress.Parse(serverIP);
            int destinationPort = System.Convert.ToInt16(serverPort);

            IPEndPoint destinationEP = new IPEndPoint(destinationIP, destinationPort);

            // Message
            Console.WriteLine("\nWaiting to Connect... ");

            m_sendSocket.Connect(destinationEP);

            // Message
            Console.WriteLine("Connected... ");

            // 3- Send Information
            // Input data from user
            string message = "";

            while (message != "Exit")
            {
                Console.Write("\nEnter Message to Send (if you want to quit enter \"Exit\"): ");
                message = Console.ReadLine();
                byte[] b_Data = System.Text.Encoding.ASCII.GetBytes(message);

                //Message
                Console.WriteLine("\nSending Data... ");

                m_sendSocket.Send(b_Data, SocketFlags.None);

                //Message
                Console.WriteLine("Sending Complete... ");
            }



        }
    }
}
