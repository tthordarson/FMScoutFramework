using FMDraft.Library;
using FMDraft.WPF.Tabs;
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

namespace FMDraft.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GameCore game = null;

        public MainWindow()
        {
            InitializeComponent();
            InitializeGame();
        }

        public bool IsGameLoaded
        {
            get
            {
                return game != null && game.IsLoaded;
            }
        }

        private void InitializeGame()
        {
            game = new GameCore();

            game.LoadFailedCallback += () =>
            {
                MessageBox.Show("Could not load data. Ensure that Football Manager 2015 is running and that no Firewall is prevent this application from accessing it.");
            };

            game.LoadCompleteCallback += () =>
            {
                MessageBox.Show("Data has been loaded");
            };
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            game.Load();
            
        }

        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ConfederationTab_Selected(object sender, RoutedEventArgs e)
        {
            ConfederationTab.Content = new ConfederationTabView(game);
        }
    }
}
