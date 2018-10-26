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

namespace BattleShipWpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        int gridSize = 10;
        String[,] gridArray;
        Button[,] buttonArray;

        public MainWindow()
        {
            InitializeComponent();
            gridArray = new String[gridSize, gridSize];
            buttonArray = new Button[gridSize, gridSize];

            Random random = new Random();
            int randomNumber1 = random.Next();
            int randomNumber2 = random.Next();

            gridArray[randomNumber1, randomNumber2] = "Skib";

            for (int i = 0; i < gridSize; i++)
            {
                RowDefinition rd = new RowDefinition();
                rd.Height = new GridLength(50, GridUnitType.Pixel);
                ViewGrid.RowDefinitions.Add(rd);

                ColumnDefinition cd = new ColumnDefinition();
                cd.Width = new GridLength(50, GridUnitType.Pixel);
                ViewGrid.ColumnDefinitions.Add(cd);
            }

            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    Button btn = new Button();
                    btn.Click += gridClicked(i, j);
                    btn.Content = "[" + i + "," + j + "]";
                    buttonArray[i, j] = btn;
                    btn.Margin = new Thickness(2, 2, 2, 2);
                    ViewGrid.Children.Add(btn);
                    Grid.SetColumn(btn, i);
                    Grid.SetRow(btn, j);
                }
            }
        }

        private RoutedEventHandler gridClicked(int i, int j)
        {
            //return (btn, e) => buttonArray[i, j].Content = "ok";
            return (btn, e) => buttonArray[i, j].Content = gridArray[i,j];
        }
            }
}
