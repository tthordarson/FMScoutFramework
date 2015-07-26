using FMDraft.Library;
using FMDraft.WPF.Templates;
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
        private MainWindowViewModel viewModel;

        public MainWindow()
        {
            InitializeComponent();

            viewModel = new MainWindowViewModel();
            this.DataContext = viewModel;

            viewModel.LoadCompleteCallback += () =>
            {
                MessageBox.Show("Data has been loaded");
            };

            viewModel.LoadFailedCallback += () =>
            {
                MessageBox.Show("Could not load data. Ensure that Football Manager 2015 is running and that no Firewall is prevent this application from accessing it.");
            };

            viewModel.QuitProgramCallback += () => 
            {
                Close();
            };
        }
    }
}
