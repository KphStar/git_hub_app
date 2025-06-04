using Newtonsoft.Json.Linq;
using System;
using System.Data.Common;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Windows.Forms;

namespace git_hub_app
{
    public partial class User_Profile : Form
    {
        public User_Profile(string username, string name, string email, string avatarUrl, string bio)
        {
            InitializeComponent();

            lblUsername.Text = $"@{username}";
            lblName.Text = string.IsNullOrWhiteSpace(name) ? "Unknown" : name;
            lblEmail.Text = string.IsNullOrWhiteSpace(email) ? "Email: Not public" : $"Email: {email}";
            textBoxBio.Text = string.IsNullOrWhiteSpace(bio) ? "(No bio available)" : bio;
            labelWelcome.Text = $"Welcome, {lblName.Text}!";

            // Make avatar circular
            pictureBoxAvatar.SizeMode = PictureBoxSizeMode.StretchImage;

            try
            {
                using (var client = new WebClient())
                {
                    byte[] data = client.DownloadData(avatarUrl);
                    using (var ms = new System.IO.MemoryStream(data))
                    {
                        Image avatar = Image.FromStream(ms);
                        pictureBoxAvatar.Image = GetCircularImage(avatar, pictureBoxAvatar.Width, pictureBoxAvatar.Height);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Failed to load avatar image.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            LoadPopularRepos(username);


        }

        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        private Bitmap GetCircularImage(Image srcImage, int width, int height)
        {
            Bitmap dstImage = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(dstImage))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                Rectangle rect = new Rectangle(0, 0, width, height);
                using (var path = new System.Drawing.Drawing2D.GraphicsPath())
                {
                    path.AddEllipse(rect);
                    g.SetClip(path);
                    g.DrawImage(srcImage, rect);

                    // Optional teal border
                    using (Pen borderPen = new Pen(Color.Teal, 4))
                    {
                        g.ResetClip();
                        g.DrawEllipse(borderPen, rect);
                    }
                }
            }
            return dstImage;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private async void LoadPopularRepos(string username)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.UserAgent.ParseAdd("DevConnectApp");
                    var response = await client.GetAsync($"https://api.github.com/users/{username}/repos?per_page=100");

                    string json = await response.Content.ReadAsStringAsync();
                    JArray repos = JArray.Parse(json);

                    var sortedRepos = repos.OrderByDescending(r => (int?)r["stargazers_count"] ?? 0).Take(4);

                    foreach (var repo in sortedRepos)
                    {
                        string name = repo["name"]?.ToString();
                        string desc = repo["description"]?.ToString() ?? "No description";
                        string lang = repo["language"]?.ToString() ?? "Unknown";
                        string htmlUrl = repo["html_url"]?.ToString();

                        var card = CreateRepoCard(name, desc, lang, htmlUrl);
                        flowPanelRepos.Controls.Add(card);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load repositories.\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Panel CreateRepoCard(string name, string description, string language, string url)
        {
            Panel panel = new Panel
            {
                Width = 350,
                Height = 100,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(10)
            };

            Label lblTitle = new Label
            {
                Text = name,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(10, 5)
            };

            Label lblLang = new Label
            {
                Text = $"● {language}",
                ForeColor = Color.DarkGreen,
                AutoSize = true,
                Location = new Point(10, 25)
            };

            Label lblDesc = new Label
            {
                Text = description,
                AutoSize = false,
                Size = new Size(220, 40),
                Location = new Point(10, 45)
            };

            panel.Controls.Add(lblTitle);
            panel.Controls.Add(lblLang);
            panel.Controls.Add(lblDesc);

            // Make panel clickable
            panel.Cursor = Cursors.Hand;
            panel.Click += (s, e) =>
            {
                Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
            };

            return panel;
        }

        private void BtnDash_Click(object sender, EventArgs e)
        {
            User_Dashboard user_Dashboard = new User_Dashboard();
            user_Dashboard.Show();
        }

        private void User_Profile_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void User_Profile_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void User_Profile_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point diff = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(diff));
            }
        }
    }
}
