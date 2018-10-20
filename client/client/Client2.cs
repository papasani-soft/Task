using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace MSClient
{
    class Client2
    {
        const int PORT_NO = 2201;
        const string SERVER_IP = "127.0.0.1";
        static void Main(string[] args)
        {
            //---data to send to the server---
            Console.WriteLine("Enter firstname");
            string firstName = Console.ReadLine();
            Console.WriteLine("Enter lastname");
            string lastName = Console.ReadLine();
            Console.WriteLine("Enter age");
            int age = Convert.ToInt32(Console.ReadLine());
            Details d = new Details();
            d.firstName = firstName;
            d.lastName = lastName;
            d.age = age;
            string s = JsonConvert.SerializeObject(d);
            Console.WriteLine(s);
            //---create a TCPClient object at the IP and port no.---
            TcpClient client = new TcpClient(SERVER_IP, PORT_NO);
            NetworkStream nwStream = client.GetStream();
            byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(s);
            //---send the text---
            Console.WriteLine("Sending : " + s);
            nwStream.Write(bytesToSend, 0, bytesToSend.Length);
            //---read back the text---
            byte[] bytesToRead = new byte[client.ReceiveBufferSize];
            //int bytesRead = nwStream.Read(bytesToRead, 0, client.ReceiveBufferSize);
           // Console.WriteLine("Received : " + Encoding.ASCII.GetString(bytesToRead, 0, bytesRead));
            Console.ReadLine();
            client.Close();
        }
    }
    class Details
    {
       public string firstName;
       public string lastName;
        public int age;
    }
}
