using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace BattleShip
{
    internal class MapVM: ViewModelBase
    {

        static Random rnd = new Random();
        CellVM[,] map;

        public ObservableCollection<ShipVM> Ships { get; } = new ObservableCollection<ShipVM>();
        public CellVM this[int x, int y] { 
            get { return map[y, x]; } 
        }
        public IReadOnlyCollection<IReadOnlyCollection<CellVM>> Map
        {
            get
            {
                var map = new List<List<CellVM>>();
                for (int i = 0; i < 10; i++)
                {
                    map.Add(new List<CellVM>());
                    for (int j = 0; j < 10; j++)
                    {
                        map[i].Add(this.map[i, j]);
                    }
                }
                return map;
            }
        }

        internal void SetShips(params ShipVM[] ships)
        {
            foreach (var ship in ships)
            {
                Ships.Add(ship);
                var (x, y) = ship.Pos;
                var rang = ship.Rang;
                var dir = ship.Direct;
                if (dir == DirectionShip.Horisont)
                    for (int j = x; j < x + rang - 1; j++)
                        this[j, y].ToShip();
                else
                    for (int i = y; i < y + rang - 1; i++)
                        this[x, i].ToShip();
            }
        }

        public MapVM(string str) : this()
        {
            var mp = str.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (mp[i][j] == 'X')
                        map[i, j].ToShip();
                }
            }
        }

        public MapVM()
        {
            map = new CellVM[10, 10];
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    map[i, j] = new CellVM();
                }
            }
        }

       
        private List<Ship> fillMap(List<Ship> ships, params int[] navy)
        {
            var p = navy.Length - 1;

            while (p > 0 && navy[p] == 0) p--;

            if (p < 1)
            {
                return ships;
            }
            else
            {
                var ship = new Ship();
                ship.Rang = p;
                navy[p]--;
                int k = 0;
                while (k < 10)
                {
                    if (rnd.Next(2) == 0)
                    {
                        ship.Dir = DirectionShip.Horisont;
                        ship.X = rnd.Next(10 - p);
                        ship.Y = rnd.Next(10);
                    }
                    else
                    {
                        ship.Dir = DirectionShip.Vertical;
                        ship.X = rnd.Next(10);
                        ship.Y = rnd.Next(10 - p);
                    }
                    if (ships.All(other => !ship.Croos(ref other)))
                    {
                        ships.Add(ship);
                        var res = fillMap(ships, navy);
                        if (res != null)
                            return res;
                        ships.RemoveAt(ships.Count - 1);
                    }
                }
                navy[p]++;
            }
            return null;
        }

        public void FillMap(params int[] navy)
        {
            List<Ship> ships = null;

            while (ships == null)
                ships = fillMap(new List<Ship>(), navy);

            foreach (var ship in ships)
            {
                if (ship.Dir == DirectionShip.Horisont)
                {
                    for (int x = ship.X; x < ship.X + ship.Rang; x++)
                    {
                        this[x, ship.Y].ToShip();
                    }
                }
                else
                {
                    for (int y = ship.Y; y < ship.Y + ship.Rang; y++)
                    {
                        this[ship.X, y].ToShip();
                    }
                }
            }

            Ships.Clear();
            foreach (var ship in ships)
            {
                Ships.Add(new ShipVM(ship));
            }
        }

        internal struct Ship
        {
            public int X = 0, Y = 0, Rang = 1;
            public DirectionShip Dir = DirectionShip.Horisont;

            public Ship(int x, int y, int rang, DirectionShip dir)
            {
                X = x; Y = y; Rang = rang; Dir = dir;
            }

            public bool Croos(ref Ship other)
            {
                int x = X, y = Y, xx = X, yy = Y;
                if (Dir == DirectionShip.Horisont)
                    xx = x + Rang - 1;
                else
                    yy = y + Rang - 1;
                int ox = other.X, oy = other.X, oxx = ox, oyy = oy;
                if (other.Dir == DirectionShip.Horisont)
                    oxx += other.Rang - 1;
                else
                    oyy += other.Rang - 1;

                return x <= ox && ox <= xx && y <= oy && oy <= yy ||
                       x <= oxx && oxx <= xx && y <= oyy && oyy <= yy;
            }
            public override string ToString()
            {
                return $"x:{X} y:{Y} R:{Rang}{(Dir == DirectionShip.Horisont ? '-' : '|')}";
            }
        }
    }
}
