using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BattleShip
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BattleshipVM battleshipVM = new BattleshipVM();
        Random rnd = new Random();

       
        public MainWindow()
        {
            DataContext = battleshipVM;
            InitializeComponent();
        }

        //private void StartBtn_Click(object sender, RoutedEventArgs e)
        //{
        //    battleshipVM.Start();
        //}

        //private void StopBtn_Click(object sender, RoutedEventArgs e)
        //{
        //    battleshipVM?.Stop();
        //}

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
          var brd = sender as Border;
            var cellVM= brd.DataContext as CellVM;
            cellVM.ToShot();
            ;


            var x = rnd.Next(10);
            var y = rnd.Next(10);
            battleshipVM.ShotToOurMap(x, y);
        }
    }

   
}
