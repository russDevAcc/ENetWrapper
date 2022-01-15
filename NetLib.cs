using ENet;

namespace NetLib
{
    public abstract class NetSocketBase
    {
        private Host host;
       

        public NetSocketBase()
        {
            ENet.Library.Initialize();
            host = new Host();
        }

#if SERVER
        public void InitializeServer(string ip, ushort port, int maxConnections)
        {
            Address address = new Address();
            address.SetHost(ip);
            address.Port = port;

            host.Create(address, maxConnections, 32);
        }
#endif

        public void Tick()
        {
            ENet.Event netEvent = default;
            host.Service(0, out netEvent);
        }

        private void HandleNetEvent(ref ENet.Event netEvent)
        {
            switch(netEvent.Type)
            {
                case EventType.Connect:
                    break;

                case EventType.Disconnect:
                    break;

                case EventType.Timeout:
                    break;

                case EventType.Receive:
                    break;
            }
        }

        public abstract void OnSocketConnect(Peer peer);
        public abstract void OnSocketDisconnect(Peer peer);
        public abstract void OnSocketTimeout(Peer peer);
        public abstract void OnMessageReceived(Peer peer, NetBufferData data);
    }
}