using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        /// <summary>
        /// 服务器：
        ///     接受请求
        ///     发送数据
        ///     接收数据
        ///     断开连接
        /// </summary>
        static Socket serverSocket = null;

        static void Main(string[] args)
        {
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 9999);
            serverSocket.Bind(endPoint);
            serverSocket.Listen(10);
            //开启一个线程接收客户端连接
            Console.WriteLine("开始监听");
            Thread thread = new Thread(ListenClientConnent);
            thread.Start();

            while (true){}
        }

        /// <summary>
        /// 监听客户端链接
        /// </summary>
        static void ListenClientConnent()
        {
            //等待 有客户端连接的时候 会触发
            Socket clientSocket = serverSocket.Accept();
            Console.WriteLine("客户端连接成功 信息：" +clientSocket.AddressFamily.ToString());
            //给客户端发送一个消息
            clientSocket.Send(Encoding.Default.GetBytes("服务器告诉你连接成功了！"));
            Thread recThread = new Thread(ReceiveClientMessage);
            recThread.Start();
        }

        /// <summary>
        /// 接受来自客户端的消息
        /// </summary>
        static void ReceiveClientMessage(object clientSocket)
        {
            Socket socket = clientSocket as Socket;
            byte[] buffer = new byte[1024];
            //接受到数据的长度
            int length = socket.Receive(buffer);
            //显示出来
            Console.WriteLine(Encoding.Default.GetString(buffer,0,length));
        }
    }
}
