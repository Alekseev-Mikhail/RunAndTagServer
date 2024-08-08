using LiteNetLib;
using LiteNetLib.Utils;
using RunAndTagCore;
using Server;

namespace RunAndTagServer;

public class GameServer : RemoteServer
{
    private readonly Game _game;
    private readonly ServerSerializer _serializer;

    private bool _wasAnyPlayerMovement;

    public GameServer(Game game)
    {
        _game = game;
        _serializer = new ServerSerializer(Writer, game.Map, game.Seeker, game.Hider);
    }

    protected override void OnTick()
    {
        if (!_wasAnyPlayerMovement) return;
        _wasAnyPlayerMovement = false;

        _serializer.SerializeDeltaSnapshot(_game.Seeker.LastInputIndex, _game.Seeker, _game.Hider);
        _game.Seeker.Client?.Send(Writer, DeliveryMethod.Unreliable);
        Writer.Reset();
        
        _serializer.SerializeDeltaSnapshot(_game.Hider.LastInputIndex, _game.Hider, _game.Seeker);
        _game.Hider.Client?.Send(Writer, DeliveryMethod.Unreliable);
        Writer.Reset();                                                                                   
    }

    protected override void OnConnectionRequest(ConnectionRequest request)
    {
        if (Manager.ConnectedPeersCount < 2) request.Accept();
        else request.Reject();
    }

    protected override void OnConnected(NetPeer client)
    {
        _serializer.SerializeFullSnapshot(_game.Join(client, Manager.ConnectedPeersCount));
        client.Send(Writer, DeliveryMethod.ReliableOrdered);
        Writer.Reset();
    }

    protected override void OnDisconnected(NetPeer client, DisconnectInfo info)
    {
        if (info.Reason != DisconnectReason.ConnectionRejected ||
            info.Reason != DisconnectReason.ConnectionFailed ||
            info.Reason != DisconnectReason.Reconnect)
        {
            _game.Leave(client.Id);
        }
    }

    protected override void OnMessage(NetPeer client, NetDataReader reader)
    {
        switch (MessageType.GetType(reader))
        {
            case MessageType.MovementInputType:
                _wasAnyPlayerMovement = true;
                _game.Move(client.Id, ServerSerializer.DeserializeMovementInput(reader));
                break;
        }
    }
}