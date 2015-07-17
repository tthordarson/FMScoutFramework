using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FM_Draft.Library;

namespace FM_Draft
{
    public partial class Form1 : Form
    {
        private GameCore game = null;

        public Form1()
        {
            InitializeComponent();
        }

        public bool IsLoaded
        {
            get
            {
                return game != null && game.IsLoaded;
            }
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitializeGame();
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

            game.Load();
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

        //private void tabControl1_TabIndexChanged(object sender, EventArgs e)
        //{
        //    MessageBox.Show(tabControl1.SelectedTab.Name);
        //}
    }
}
