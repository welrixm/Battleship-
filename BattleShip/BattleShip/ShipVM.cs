﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{

    enum DirectionShip { Horisont, Vertical }

    internal class ShipVM: ViewModelBase
    {

        public double Opacity { get; set; }

        int rang = 1;
        (int x, int y) pos;
        DirectionShip dir = DirectionShip.Horisont;

        public ShipVM() { }
        public ShipVM(MapVM.Ship ship)
        {
            pos = (ship.X, ship.Y);
            rang = ship.Rang;
            dir = ship.Dir;
        }

        public DirectionShip Direct
        {
            get => dir;
            set => Set(ref dir, value, "Angle");
        }
        public int Rang
        {
            get => rang;
            set => Set(ref rang, value, "RangView");
        }
        public int RangView => rang * App.CellSize - 5;
        public int Angle => dir == DirectionShip.Horisont ? 0 : 90;

        public (int, int) Pos
        {
            get => pos;
            set => Set(ref pos, value, "X", "Y");
        }
        public int X => pos.x * App.CellSize + 3;
        public int Y => pos.y * App.CellSize + 3;

    }
}
