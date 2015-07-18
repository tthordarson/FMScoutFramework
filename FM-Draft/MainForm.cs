using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FMDraft.Library;

namespace FM_Draft
{
    public partial class MainForm : Form
    {
        private GameCore game = null;

        public MainForm()
        {
            InitializeComponent();
            InitializeGame();
        }

        public bool IsLoaded
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

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.game.Load();
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void confederationTab_Enter(object sender, EventArgs e)
        {
            if (IsLoaded)
            {
                principalCountryCombobox.DataSource = game.QueryService.GetNations().ToList();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var dialog = new AddToPoolDialog(game.GameState.DraftPool, game.QueryService);
            dialog.Show();
        }
    }
}
