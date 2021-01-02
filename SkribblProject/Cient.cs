using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
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
        IPEndPoint localEndPoint;
        UdpClient sender;
        public Client(string ip, int port)
        {
            this.sender = new UdpClient(port);
            this.ip = ip;
            this.port = port;
            this.CreateThread();

            IPHostEntry host = Dns.GetHostEntry(ip);
            IPAddress ipAddr = host.AddressList[0];
            this.localEndPoint = new IPEndPoint(ipAddr, this.port);
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
            sender.Send(messageSent, messageSent.Length);
        }

        public int[] getPos()
        {
            byte[] data = new byte[7];
            sender.Receive(ref localEndPoint);
            string posi = Encoding.UTF8.GetString(data);
            if (posi == "")
            {
                return null;
            }
            string pos = posi.Replace('?', ' ');
            string[] positionStr = pos.Split();
            int[] position = { int.Parse(positionStr[0]), int.Parse(positionStr[1]) };
            return position;
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