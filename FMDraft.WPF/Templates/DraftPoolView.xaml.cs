using FMDraft.Library;
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

namespace FMDraft.WPF.Templates
{
    /// <summary>
    /// Interaction logic for DraftPoolView.xaml
    /// </summary>
    public partial class DraftPoolView : UserControl
    {
        public DraftPoolView(GameCore core)
        {
            InitializeComponent();
            this.DataContext = new DraftPoolViewModel(core);

            var playerSelectView = new SelectPlayersView(core);

            Grid.SetRow(playerSelectView, 1);
            Grid.SetColumn(playerSelectView, 1);

            this.MainGrid.Children.Add(playerSelectView);
        }
    }
}
