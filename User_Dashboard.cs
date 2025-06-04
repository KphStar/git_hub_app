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
    public partial class User_Dashboard : Form
    {
        public User_Dashboard()
        {
            InitializeComponent();
        }



        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;


        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void User_Dashboard_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void User_Dashboard_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point diff = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(diff));
            }
        }

        private void User_Dashboard_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }
    }
}
