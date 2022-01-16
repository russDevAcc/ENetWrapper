using ENet;
using NetLib;
using System;
using System.Threading;

namespace TestServer
{

    public class TestServerSocket : NetSocketBase
    {
        public TestServerSocket(int maxArrayLength, int maxArraysPerBucket) : base(maxArrayLength, maxArraysPerBucket)
        {

        }

        public override void OnSocketConnect(Peer peer)
        {
            Console.WriteLine("connection received from " + peer.IP + ":" + peer.Port);
        }

        public override void OnSocketDisconnect(Peer peer)
        {
            Console.WriteLine("Peer disconnected " + peer.IP + ":" + peer.Port);
        }

        public override void OnSocketReceived(Peer peer, ushort id, ref NetBufferData data)
        {
            Console.WriteLine("Message received from " + peer.IP + ":" + peer.Port);
            Console.WriteLine(id);
            Console.WriteLine(NetBuffer.ReadInt(ref data));
            Console.WriteLine(NetBuffer.ReadBool(ref data).ToString());
            Console.WriteLine(NetBuffer.ReadUShort(ref data));
        }

        public override void OnSocketTimeout(Peer peer)
        {
           Console.WriteLine ("Peer timed out " + peer.IP + ":" + peer.Port);
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var socket = new TestServerSocket(1024, 1024);
            socket.InitializeServer("127.0.0.1", 7777, 100);

            while(true)
            {
                socket.Tick();
                Thread.Sleep(16);
            }
        }
    }
}
