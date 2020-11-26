using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace SkribblProject
{
    class Host : Online
    {
        public Host(int port)
        {
            this.port = port;
            this.CreateThread();
        }
        protected override void openSocket()
        {
            TcpListener serverSocket = new TcpListener(IPAddress.Any, port);
            serverSocket.Start();

        }
    }
    class Client : Online
    {
        string ip;
        int port;
        Socket sender;
        IPEndPoint localEndPoint;
        public Client(string ip, int port)
        {
            this.ip = ip;
            this.port = port;
            this.CreateThread();

            IPHostEntry host = Dns.GetHostEntry(ip);
            IPAddress ipAddr = host.AddressList[0];
            this.localEndPoint = new IPEndPoint(ipAddr, this.port);
            this.sender = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        }
        protected override void openSocket()
        {
            
        }

        public void Connect()
        {
            sender.Connect(localEndPoint);
        }

        public void Send(string data)
        {
            byte[] messageSent = Encoding.UTF8.GetBytes(data);
            sender.Send(messageSent);
        }

        public Texture2D Draw()
        {
            byte[] data = new byte[1024];
            sender.Receive(data);
            string pos = Encoding.UTF8.GetString(data);
            string[] position = pos.Split(",");
        }
    }
    abstract class Online
    {
        protected int port;
        protected Thread thread;
        protected void CreateThread()
        {
            thread = new Thread(openSocket);
            thread.IsBackground = true;
            thread.Start();
        }
        protected abstract void openSocket();
    }
}