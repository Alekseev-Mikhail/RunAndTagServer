using Core;
using LiteNetLib;
using RunAndTagCore;

namespace RunAndTagServer;

public class Game(GameMap map)
{
    private readonly RayMath _rayMath = new();

    public readonly ServerPlayer Seeker = new(map.SeekerPreset); 
    public readonly ServerPlayer Hider = new(map.HiderPreset);
    public GameMap Map => map;

    public byte Join(NetPeer client, int playerCount)
    {
        if (playerCount == 1)
        {
            Seeker.Client = client;
            return GameRole.Seeker;
        }

        Hider.Client = client;
        return GameRole.Hider;
    }

    public void Leave(int id)
    {
        if (Seeker.Client?.Id == id)
        {
            Seeker.Client = null;
            return;
        }

        Hider.Client = null;
    }

    public void Move(int id, MovementInput input)
    {
        var player = GetPlayerById(id);
        player.LastInputIndex = input.InputIndex;
        
        MovePlayer(player, 270f, input.Up);
        MovePlayer(player, 90f, input.Down);
        MovePlayer(player, 180f, input.Left);
        MovePlayer(player, 0f, input.Right);
    }

    private void MovePlayer(Player player, float angle, byte value)
    {
        for (var i = 0; i < value; i++)
        {
            _rayMath.Step(player, map, angle, player.MovementVelocity);
            
            if (_rayMath.ResultRay.IsWallExist) break;
            player.X = _rayMath.ResultRay.Target.X;
            player.Y = _rayMath.ResultRay.Target.Y;
        }
    }

    private ServerPlayer GetPlayerById(int id) => id == Seeker.Client?.Id ? Seeker : Hider;
}