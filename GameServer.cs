using LiteNetLib;
using Server;

namespace RunAndTagServer;

public class GameServer(Game game) : RemoteServer
{
    protected override void OnTick()
    {
        game.Test();
    }

    protected override void OnConnectionRequest(ConnectionRequest request)
    {
        if (Manager.ConnectedPeersCount < 2) request.Accept();
        else request.Reject();
    }

    protected override void OnPeerConnected(NetPeer peer)
    {
        Send(game.Join(peer.Id), peer);
    }

    protected override void OnPeerDisconnected(NetPeer peer, DisconnectInfo info)
    {
        game.Leave(peer.Id);
    }
}