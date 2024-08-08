using RunAndTagCore;
using RunAndTagServer;

const string tileSet = "###########" +
                       "#         #" +
                       "#  #  ##  #" +
                       "#     ##  #" +
                       "#  #      #" +
                       "# #####   #" +
                       "# #   # # #" +
                       "#     # # #" +
                       "# #   # # #" +
                       "# #       #" +
                       "###########";
const int mapWidth = 11;

var seeker = new Player(
    5f,
    2.5f,
    165f,
    0.01f,
    5f,
    75f,
    3f,
    0.03f
);
var hider = new Player(
    4f,
    7f,
    90f,
    0.01f,
    1f,
    25f,
    3f,
    0.01f
);
var map = new GameMap(tileSet, mapWidth, '#', seeker, hider);

var game = new Game(map);
var server = new GameServer(game);

server.Start(8080, 10);

Console.ReadKey();