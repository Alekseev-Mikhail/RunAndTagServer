using Core;
using LiteNetLib.Utils;
using RunAndTagCore;

namespace RunAndTagServer;

public class ServerSerializer(NetDataWriter writer, Map map, Player seeker, Player hider)
{
    public void SerializeFullSnapshot(byte role)
    {
        writer.Put(MessageType.FullSnapshotType);

        SerializeMap();
        SerializePlayer(seeker);
        SerializePlayer(hider);
        writer.Put(role);
    }

    public void SerializeDeltaSnapshot(byte inputIndex, Player first, Player second)
    {
        writer.Put(MessageType.DeltaSnapshotType);

        writer.Put(inputIndex);
        SerializePosition(first);
        SerializePosition(second);
    }

    public static MovementInput DeserializeMovementInput(NetDataReader reader) =>
        new(reader.GetByte(), reader.GetByte(), reader.GetByte(), reader.GetByte(), reader.GetByte());

    private void SerializePosition(Player player)
    {
        writer.Put(player.X);
        writer.Put(player.Y);
    }
    
    private void SerializePlayer(Player player)
    {
        writer.Put(player.X);
        writer.Put(player.Y);
        writer.Put(player.Direction);
        writer.Put(player.RayStep);
        writer.Put(player.MaxRayDistance);
        writer.Put(player.Fov);
        writer.Put(player.RotationVelocity);
        writer.Put(player.MovementVelocity);
    }

    private void SerializeMap()
    {
        writer.Put(map.TileSet);
        writer.Put(map.Width);
        writer.Put(map.WallTile);
    }
}