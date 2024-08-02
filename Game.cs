using RunAndTagCore;

namespace RunAndTagServer;

public class Game(GameMap map)
{
    private ServerPlayer? _seeker;
    private ServerPlayer? _hider;

    public PlayerIdentifier Join(int id)
    {
        uint role;

        if (_seeker == null)
        {
            role = GameRole.Seeker;
            _seeker = ServerPlayer.FromPlayer(map.SeekerPreset, id);
        }
        else
        {
            role = GameRole.Hider;
            _hider = ServerPlayer.FromPlayer(map.HiderPreset, id);
        }

        var identifier = new PlayerIdentifier(Guid.NewGuid().ToString(), role);
        return identifier;
    }

    public void Leave(int id)
    {
        if (id == _seeker?.Id) _seeker = null;
        if (id == _hider?.Id) _hider = null;
    }
    
    public void Test()
    {
        Console.WriteLine("----------");
        Console.WriteLine(_seeker?.Id);
        Console.WriteLine(_hider?.Id);
    }
}