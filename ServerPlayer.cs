using LiteNetLib;
using RunAndTagCore;

namespace RunAndTagServer;

public class ServerPlayer(Player player) : Player(
    player.X,
    player.Y,
    player.Direction,
    player.RayStep,
    player.MaxRayDistance,
    player.Fov,
    player.RotationVelocity,
    player.MovementVelocity
)
{
    public NetPeer? Client = null;
    public byte LastInputIndex = 0;
}