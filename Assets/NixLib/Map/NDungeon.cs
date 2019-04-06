using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToolBox.Map.CellularAutomata;

namespace ToolBox.Map
{
    public enum DungeonBuilding { Null = 0, Port = 1, Door, Treasure, Trap, Enemy,Tree,Misc = 254, Wall = 255, Block = 256 };

    public static class NDungeon
    {
        public static NMap CaveWallMap(int width, int height, float rate, int holeLeft)//dig map
        {
            NMap map = new NMap(width, height);
            map.Noise(rate);
            CA_Rule rule = new CA_Rule("s45678b5678");
            map = rule.Run(map, 3);
            var blobs = NBlob.Find(map, 255);
            blobs.FillByLeftBlob(map, holeLeft, 0);
            blobs.NoiseConnect(map, 255);

            map = map.InverseVal();
            bool HorV = RandomNum.Roll(0.5f);
            NLocate Entrance = new NLocate();
            NLocate Exit = new NLocate();
            if (HorV)
            {
                Entrance = RandomSelect<NLocate>.Select(new List<NLocate>(map.LeftLocates(0))).Right();
                Exit = RandomSelect<NLocate>.Select(new List<NLocate>(map.RightLocates(0))).Left();
            }
            else
            {
                Entrance = RandomSelect<NLocate>.Select(new List<NLocate>(map.TopLocates(0))).Down();
                Exit = RandomSelect<NLocate>.Select(new List<NLocate>(map.BottomLocates(0))).Up();
            }
            map.SetBlock(Entrance.Square(1), 0);
            map.SetBlock(Exit.Square(1), 0);
            map.SetBlock(Entrance, (byte)DungeonBuilding.Port);
            map.SetBlock(Exit, (byte)DungeonBuilding.Port);

            NLocationRecogition.FindTreasure(map, 3);
            NLocationRecogition.FindDeadEnd(map, 3);

            NLocationRecogition.FindPassage(map, 4);

            return map;
        }

        public static NMap ArenaBlockMap(int width, int height, float rate, int holeLeft)
        {
            NMap map = new NMap(width, height);
            map.Noise(rate);
            CA_Rule rule = new CA_Rule("s45678b5678");
            map = rule.Run(map,3);
            var blobs = NBlob.Find(map, 255);
            blobs.FillByLeftBlob(map, holeLeft, 0);
            blobs = NBlob.Find(map, 0);
            blobs.NoiseConnect(map, 0);
            return map;
        }

        public static NMap MazeWallMap(int width, int height, float rate)
        {
            NMap map = new NMap(width, height);
            map.Noise(rate);
            CA_Rule rule = new CA_Rule(CAClassicRules.GetClassicRule("Mazectric"));
            map = rule.Run(map, 3);
            var blobs = NBlob.Find(map, 0);
            blobs.NoiseConnect(map, 0);
            return map;
        }

        public static NMap DrunkMazeWallMap(int width, int height)
        {
            NMap map = new NMap(width, height,255);
            NMove.DrunkardWalk(map, map.Center(), 0);
            return map;
        }

        public static NMap RoomWallMap(int width, int height)
        {
            NMap map = new NMap(width, height, 255);
            NBSP.Division(map, new NRect(0, 0, map.Width, map.Height));
            var blobs = NBlob.Find(map, 0);
            blobs.FillByLeftBlob(map, 12);
            blobs.AxisConnect(map, 0);
            //NLocationRecogition.FindDoor(map, 5);
            return map;
        }

        public static void SelfTest()
        {
            var cave = CaveWallMap(64, 64, 0.45f, 10);
            Console.WriteLine("Type:Cave DigMap");
            Console.WriteLine(cave.Print());

            var arena = ArenaBlockMap(64, 64, 0.45f, 10);
            Console.WriteLine("Type:Arena BlockMap");
            Console.WriteLine(arena.Print());

            //var maze = MazeWallMap(64, 64, 0.40f);
            //Console.WriteLine("Type:Maze WallMap");
            //Console.WriteLine(maze.Print());

            var maze2 = DrunkMazeWallMap(64, 64);
            Console.WriteLine("Type:Maze2 WallMap");
            Console.WriteLine(maze2.Print());

            var room = RoomWallMap(64, 64);
            Console.WriteLine("Type:Room WallMap");
            Console.WriteLine(room.Print());
        }
    }
}
