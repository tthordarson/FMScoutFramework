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
        private GameState game = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitializeGame();
        }

        private void InitializeGame()
        {
            game = new GameState();

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
    }
}
