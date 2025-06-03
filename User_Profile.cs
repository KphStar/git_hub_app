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
    public partial class User_Profile : Form
    {
        public User_Profile(string username, string name, string email, string avatarUrl, string bio)
        {
            InitializeComponent();

            lblUsername.Text = $"Username: {username}";
            lblName.Text = $"Name: {name}";
            lblEmail.Text = $"Email: {email}";
            lblBio.Text = $"Bio: {bio}";

            // Load avatar image
            using (var client = new System.Net.WebClient())
            {
                pictureBoxAvatar.Load(avatarUrl);
            }



        }

    }
}
