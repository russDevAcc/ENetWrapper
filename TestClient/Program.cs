using ENet;
using NetLib;
using System;
using System.Threading;

namespace TestClient
{
    public class TestClient : NetSocketBase
    {
        public TestClient(int maxArrayLength, int maxArraysPerBucket) : base(maxArrayLength, maxArraysPerBucket)
        {
        }

        public override void OnSocketConnect(Peer peer)
        {
            var buf = NetBuffer.Create(21, 1024);
            NetBuffer.AddInt(ref buf, ushort.MaxValue + 1);
            NetBuffer.AddBool(ref buf, false);
            NetBuffer.AddUShort(ref buf, ushort.MaxValue);

            Console.WriteLine(NetBuffer.ReadInt(ref buf));
            Console.WriteLine(NetBuffer.ReadBool(ref buf).ToString());
            Console.WriteLine(NetBuffer.ReadUShort(ref buf));

            SendMessage(ref buf, PacketFlags.Reliable | PacketFlags.NoAllocate);
            NetBuffer.Destroy(ref buf);
        }

        public override void OnSocketDisconnect(Peer peer)
        {
            
        }

        public override void OnSocketReceived(Peer peer, ushort id, ref NetBufferData data)
        {
            
        }

        public override void OnSocketTimeout(Peer peer)
        {
            
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            
            var socket = new TestClient(1024, 1024);
            socket.InitializeClient();
            socket.Connect("127.0.0.1", 7777);
            while(true)
            {
                socket.Tick();
                Thread.Sleep(16);
            }
        }
    }
}
