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



        public void ShowFileCards(List<(string Name, string Type, string Updated)> files)
        {
            flowPanelFiles.Controls.Clear();

            foreach (var file in files)
            {
                var card = CreateFileCard(file.Name, file.Type, file.Updated);
                flowPanelFiles.Controls.Add(card);
            }
        }
        private Panel CreateFileCard(string name, string type, string updated)
        {
            Panel card = new Panel
            {
                Width = 350,
                Height = 60,
                BackColor = Color.WhiteSmoke,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(8),
                Cursor = Cursors.Hand
            };

            PictureBox icon = new PictureBox
            {
                Width = 28,
                Height = 28,
                Location = new Point(10, 15),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = type == "dir"
                    ? Properties.Resources.folder // add your own icon
                    : Properties.Resources.documents   // add your own icon
            };

            Label lblName = new Label
            {
                Text = name,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(50, 8),
                AutoSize = true
            };

            Label lblUpdated = new Label
            {
                Text = updated,
                Font = new Font("Segoe UI", 8, FontStyle.Italic),
                ForeColor = Color.DimGray,
                Location = new Point(50, 30),
                AutoSize = true
            };

            card.Controls.Add(icon);
            card.Controls.Add(lblName);
            card.Controls.Add(lblUpdated);

            card.MouseEnter += (s, e) => card.BackColor = Color.Gainsboro;
            card.MouseLeave += (s, e) => card.BackColor = Color.WhiteSmoke;

            // Optional: Card click to open or preview file
            card.Click += (s, e) =>
            {
                MessageBox.Show($"You clicked {name}", "File", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };

            return card;
        }




        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            dataGridView1.Rows.Clear();
            labelTitle.Text = "Repository Details";
        }

     
    }
}
