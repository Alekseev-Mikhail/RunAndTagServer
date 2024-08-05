using LiteNetLib;
using RunAndTagCore;
using Server;

namespace RunAndTagServer;

public class GameServer(Game game) : RemoteServer
{
    protected override void OnTick()
    {
    }

    protected override void OnConnectionRequest(ConnectionRequest request)
    {
        if (Manager.ConnectedPeersCount < 2) request.Accept();
        else request.Reject();
    }

    protected override void OnConnected(NetPeer client)
    {
        var writer = NetworkSerializer.SerializeFullSnapshot(
            game.Map,
            game.Seeker,
            game.Hider,
            game.GetRole(Manager.ConnectedPeersCount)
        );
        client.Send(writer, DeliveryMethod.ReliableOrdered);
    }

    protected override void OnDisconnected(NetPeer client, DisconnectInfo info)
    {
    }
}