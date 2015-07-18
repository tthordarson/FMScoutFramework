using FM_Draft.Library;
using FM_Draft.Library.Entities;
using FM_Draft.Library.DataProjections;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FM_Draft
{
    public partial class AddToPoolDialog : Form
    {
        private QueryService queryService;
        private DraftPool draftPool;

        public AddToPoolDialog(DraftPool draftPool, QueryService queryService)
        {
            InitializeComponent();
            this.draftPool = draftPool;
            this.queryService = queryService;
        }

        private void AddToPoolDialog_Load(object sender, EventArgs e)
        {
            gridView.DataSource = queryService
                .GetPlayers(x => true)
                .Select(x => x.AsProjected())
                .ToList();
        }
    }
}
