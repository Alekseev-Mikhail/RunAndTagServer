using Core;
using RunAndTagCore;

namespace RunAndTagServer;

public class GameMap(
    string tileSet,
    int width,
    char wallTile,
    Player seekerPreset,
    Player hiderPreset
) : Map(tileSet, width, wallTile)
{
    public Player SeekerPreset = seekerPreset;
    public Player HiderPreset = hiderPreset;
}