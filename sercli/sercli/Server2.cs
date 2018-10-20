using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace MulConServ
{
    class Server2
    {
        const int PORT_NO = 2201;
        const string SERVER_IP = "127.0.0.1";
        static void Main(string[] args)
        {
            //---listen at the specified IP and port no.---
            IPAddress localAdd = IPAddress.Parse(SERVER_IP);
            TcpListener listener = new TcpListener(localAdd, PORT_NO);
            Console.WriteLine("Listening...");
            listener.Start();
            //---incoming client connected---
            TcpClient client = listener.AcceptTcpClient();
            //---get the incoming data through a network stream---
            NetworkStream nwStream = client.GetStream();
            byte[] buffer = new byte[client.ReceiveBufferSize];
            //---read incoming stream---
            int bytesRead = nwStream.Read(buffer, 0, client.ReceiveBufferSize);
            //---convert the data received into a string---
            string dataReceived = Encoding.ASCII.GetString(buffer, 0, bytesRead);
            Console.WriteLine("Received : " + dataReceived);

            Details d=JsonConvert.DeserializeObject<Details>(dataReceived);
            Console.WriteLine("desrialisation started");
            Console.WriteLine("first name:"+d.firstName);
            Console.WriteLine("last name:"+d.lastName);
            Console.WriteLine("age:"+d.age);
            Console.WriteLine("end");

            //---write back the text to the client---
           
            nwStream.Write(buffer, 0, bytesRead);
            client.Close();
            listener.Stop();
            Console.ReadLine();
        }
    }
    class Details
    {
        public string firstName;
        public string lastName;
        public int age;
    }
}
