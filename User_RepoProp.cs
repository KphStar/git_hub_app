using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace git_hub_app
{
    public partial class User_RepoProp : Form
    {
        public User_RepoProp()
        {
            InitializeComponent();

            dataGridView1.ColumnCount = 3;
            dataGridView1.Columns[0].Name = "Name";
            dataGridView1.Columns[1].Name = "Type";
            dataGridView1.Columns[2].Name = "Last Updated";

            labelTitle.Text = "Repository Details";
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            dataGridView1.Rows.Clear();
            labelTitle.Text = "Repository Details";
        }

     
    }
}
