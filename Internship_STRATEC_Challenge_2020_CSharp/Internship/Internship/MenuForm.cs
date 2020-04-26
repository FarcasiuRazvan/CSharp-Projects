using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Internship
{
    public partial class MenuForm : Form
    {
        public MenuForm()
        {
            InitializeComponent();
        }

        private void MenuForm_Load(object sender, EventArgs e)
        {

        }

        private void btnTestRoutes_Click(object sender, EventArgs e)
        {
            this.Hide();
            (new TestRoutesForm()).ShowDialog();
            this.Show();
        }

        private void btnConnectPoints_Click(object sender, EventArgs e)
        {
            this.Hide();
            (new ConnectPointsForm()).ShowDialog();
            this.Show();
        }

        private void btnCustomGrid_Click(object sender, EventArgs e)
        {
            this.Hide();
            (new CustomGridForm()).ShowDialog();
            this.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
