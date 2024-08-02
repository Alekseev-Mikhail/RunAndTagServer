using Core;

namespace RunAndTagServer;

public class ServerPlayer(
    int id,
    float x,
    float y,
    float direction,
    float velocity
) : Player(x, y, direction, velocity)
{
    public int Id => id;

    public static ServerPlayer FromPlayer(Player player, int id) =>
        new(id, player.X, player.Y, player.Direction, player.Velocity);
}