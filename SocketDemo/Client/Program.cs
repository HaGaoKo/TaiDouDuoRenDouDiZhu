using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static Socket clientSocket = null;

        static void Main(string[] args)
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //客户端连接 不需要bind
            IPEndPoint remoteEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9999);
            clientSocket.Connect(remoteEP);
            Console.WriteLine("连接到远程服务器");

            byte[] result = new byte[1024];
            int length = clientSocket.Receive(result);
            Console.WriteLine("收到消息" + Encoding.Default.GetString(result, 0, length));

            clientSocket.Send(Encoding.Default.GetBytes("服务器你好，我是客户端"));

            while (true) { }
        }

      
    }
}
