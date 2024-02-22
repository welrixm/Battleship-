using System;
using System.Windows;
using System.Windows.Threading;

namespace BattleShip
{
    class BattleshipVM : ViewModelBase
    {
        DispatcherTimer timer;
        DateTime startTime;
        string time = "";
//        string sampleMap = @"
//**********
//*XX***X*
//******X***
//XX*XX***XX
//******X***
//*XXX******
//*****XXX**
//**********
//*X********
//**********";


        public MapVM OurMap { get; private set;  }
        public MapVM EnemyMap { get; private set; }

        public string Time
        {
            get => time;
            private set => Set(ref time, value);
        }

        public BattleshipVM()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Tick += Timer_Tick;

            OurMap = new MapVM();
            OurMap.SetShips(
        new ShipVM { Rang = 4, Pos = (1, 1) },
        new ShipVM { Rang = 3, Pos = (6, 1), Direct = DirectionShip.Vertical, },
        new ShipVM { Rang = 3, Pos = (8, 1), Direct = DirectionShip.Vertical, },
        new ShipVM { Rang = 2, Pos = (1, 3), },
        new ShipVM { Rang = 2, Pos = (1, 5), },
        new ShipVM { Rang = 2, Pos = (4, 3), Direct = DirectionShip.Vertical, },
        new ShipVM { Rang = 1, Pos = (1, 9), },
        new ShipVM { Rang = 1, Pos = (2, 7), },
        new ShipVM { Rang = 1, Pos = (4, 7), },
        new ShipVM { Rang = 1, Pos = (8, 9), }
        );

            EnemyMap = new MapVM();
            EnemyMap.FillMap(0, 4, 3, 2, 1);

            foreach (var ship in EnemyMap.Ships)
            {
                ship.Opacity = 0; // Установите желаемую прозрачность
            }

        }




        //CellVM[][] MapFabrica (string str)
        //{
        //    var mp = str.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
        //    var map = new CellVM[10][];
        //    for (int i = 0; i < 10; i++)
        //    {
        //        map[i] = new CellVM[10];
        //        for (int j = 0; j < 10; j++)
        //        {
        //            map[i][j] = new CellVM(mp[i][j]);
        //        }

        //    }
        //    return map;
        //}


        private void Timer_Tick(object? sender, EventArgs e)
        {
            var now = DateTime.Now;
            var dt = now - startTime;
            Time = dt.ToString(@"mm\:ss");
        }

        internal void ShotToOurMap(int x, int y)
        {
            OurMap[x, y].ToShot();

        }

        public void Start()
        {
            startTime = DateTime.Now;
            timer.Start();
        }

        public void Stop() 
        {
            timer.Stop();
        }
    }

    // class CellVM : ViewModelBase
    //{
    //    // Visibility visibility  = Visibility.Collapsed;
    //    bool ship, shot;

    //    public CellVM(char state) {
    //        ship = state == 'X';
    //    }

    //    public Visibility Miss => 
    //        shot && !ship ? Visibility.Visible : Visibility.Collapsed;
    //        //private set => Set(ref visibility, value);
        

    //    public Visibility Shot =>
    //                shot && ship ? Visibility.Visible : Visibility.Collapsed;


    //    public void SetState() {
    //        shot = true;
    //        Fire("Miss", "Shot");
    //    }
    //}

}
