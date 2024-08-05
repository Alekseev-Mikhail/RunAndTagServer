using Core;
using RunAndTagCore;

namespace RunAndTagServer;

public class Game(GameMap map)
{
    public GameMap Map => map;
    public readonly Player Seeker = map.SeekerPreset.Copy();
    public readonly Player Hider = map.HiderPreset.Copy();

    public byte GetRole(int index) => index == 1 ? GameRole.Seeker : GameRole.Hider;
}