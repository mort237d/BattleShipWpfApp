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
    public partial class MainWindow : Window
    {
        private int koordinat1;
        private int koordinat2;
        private int retning;
        Random random = new Random();

        private bool check = true;
        private int counter = 0;

        int gridSize = 10;
        String[,] gridArray;
        Button[,] buttonArray;

        private int missed = 0;
        private int hit = 0;
        private TextBlock hitsAndMissed;
        private TextBlock ships;

        private RowDefinition rd;
        private ColumnDefinition cd;
        private TextBlock victory;
        private Button btn;

        public MainWindow()
        {
            InitializeComponent();
            gridArray = new String[gridSize, gridSize];
            buttonArray = new Button[gridSize, gridSize];

            for (int i = 0; i < gridSize; i++) for (int j = 0; j < gridSize; j++) gridArray[i, j] = "Plask";

            NewShip(5, "Hangarskib");
            NewShip(4, "Slagskib");
            NewShip(3, "Destroyer");
            NewShip(3, "Ubåd");
            NewShip(2, "Patruljebåd");

            for (int i = 0; i < gridSize; i++)
            {
                rd = new RowDefinition {Height = new GridLength(50, GridUnitType.Pixel)};
                ViewGrid.RowDefinitions.Add(rd);

                cd = new ColumnDefinition {Width = new GridLength(50, GridUnitType.Pixel)};
                ViewGrid.ColumnDefinitions.Add(cd);
            }

            rd = new RowDefinition {Height = new GridLength(50, GridUnitType.Pixel)};
            ViewGrid.RowDefinitions.Add(rd);

            cd = new ColumnDefinition {Width = new GridLength(100, GridUnitType.Pixel)};
            ViewGrid.ColumnDefinitions.Add(cd);

            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    btn = new Button();
                    btn.Click += GridClicked(i, j);
                    //if (gridArray[i, j] == "Hangarskib") btn.Content = "H";
                    //else if (gridArray[i, j] == "Slagskib") btn.Content = "S";
                    //else if (gridArray[i, j] == "Destroyer") btn.Content = "D";
                    //else if (gridArray[i, j] == "Ubåd") btn.Content = "U";
                    //else if (gridArray[i, j] == "Patruljebåd") btn.Content = "P";
                    //else
                    //{
                    //    btn.Content = "[" + i + "," + j + "]";
                    //}
                    btn.Content = "[" + i + "," + j + "]";
                    buttonArray[i, j] = btn;
                    btn.Margin = new Thickness(2, 2, 2, 2);
                    ViewGrid.Children.Add(btn);
                    Grid.SetColumn(btn, i);
                    Grid.SetRow(btn, j);
                }
            }

            hitsAndMissed = new TextBlock
            {
                Foreground = new SolidColorBrush(Colors.White),
                Text = "Missed: " + missed +
                       "\nHit: " + hit
            };
            ViewGrid.Children.Add(hitsAndMissed);
            Grid.SetColumn(hitsAndMissed, 10);
            Grid.SetRow(hitsAndMissed, 0);

            ships = new TextBlock
            {
                Foreground = new SolidColorBrush(Colors.White),
                Text = "Skibe og størrelse:" +
                       "\n- Hangarskib: 5" +
                       "\n- Slagskib: 4" +
                       "\n- Destroyer: 3" +
                       "\n- Ubåd: 3" +
                       "\n- Patruljebåd: 2"
            };
            ViewGrid.Children.Add(ships);
            Grid.SetRowSpan(ships, 2);
            Grid.SetColumn(ships, 10);
            Grid.SetRow(ships, 7);

            victory = new TextBlock {Foreground = new SolidColorBrush(Colors.White), Text = ""};
            ViewGrid.Children.Add(victory);
            Grid.SetColumn(victory, 10);
            Grid.SetRow(victory, 9);
        }

        private void NewShip(int size, string name)
        {
            check = true;
            retning = random.Next(0, 2);

            while (check)
            {
                koordinat1 = random.Next(0 , 10);
                koordinat2 = random.Next(0 , 10);

                for (int i = 0; i < size; i++)
                {
                    if (retning == 0)
                    {
                        if (koordinat2 < 5 && gridArray[koordinat1, koordinat2 + i] == "Plask") counter++;
                        else if (koordinat2 >= 5 && gridArray[koordinat1, koordinat2 - i] == "Plask") counter++;
                        else counter = 0;
                    }
                    else
                    {
                        if (koordinat1 < 5 && gridArray[koordinat1 + i, koordinat2] == "Plask") counter++;
                        else if (koordinat1 >= 5 && gridArray[koordinat1 - i, koordinat2] == "Plask") counter++;
                        else counter = 0;
                    }
                }
                if (counter == size)
                {
                    if (retning == 0)
                    {
                        if (koordinat2 < 5) for (int i = 0; i < size; i++) gridArray[koordinat1, koordinat2 + i] = "Ramt";
                        else if (koordinat2 >= 5) for (int i = 0; i < size; i++) gridArray[koordinat1, koordinat2 - i] = "Ramt";
                    }
                    else
                    {
                        if (koordinat1 < 5) for (int i = 0; i < size; i++) gridArray[koordinat1 + i, koordinat2] = "Ramt";
                        else if (koordinat1 >= 5) for (int i = 0; i < size; i++) gridArray[koordinat1 - i, koordinat2] = "Ramt";
                    }

                    check = false;
                }
            }

        }

        private RoutedEventHandler GridClicked(int i, int j)
        {
            return (btn, e) =>
            {
                buttonArray[i, j].Content = gridArray[i, j];
                if (gridArray[i, j] == "Plask")
                {
                    buttonArray[i, j].Background = new SolidColorBrush(Colors.Blue);
                    buttonArray[i, j].Foreground = new SolidColorBrush(Colors.White);

                    missed++;
                    hitsAndMissed.Text = "Missed: " + missed +
                                         "\nHit: " + hit;
                }
                else
                {
                    buttonArray[i, j].Background = new SolidColorBrush(Colors.Red);
                    buttonArray[i, j].Foreground = new SolidColorBrush(Colors.White);

                    hit++;
                    hitsAndMissed.Text = "Missed: " + missed +
                                         "\nHit: " + hit;
                }

                if (hit == 17)
                {
                    victory.Text = "Victory!!!";
                    for (int k = 0; k < gridSize; k++)
                    {
                        for (int l = 0; l < gridSize; l++)
                        {
                            buttonArray[k, l].Content = "Victory";
                        }
                    }
                }
            };
        }
            }
}
