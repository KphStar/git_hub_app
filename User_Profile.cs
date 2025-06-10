using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace git_hub_app
{
    public partial class User_Profile : Form
    {
        private string accessToken;
        private Timer repoRefreshTimer;
        public User_Profile(string username, string name, string email, string avatarUrl, string bio, string token)
        {
            InitializeComponent();

            accessToken = token;
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

          

            _ = InitRepoPaging(); // fire and forget

            repoRefreshTimer = new Timer();
            repoRefreshTimer.Interval = refreshIntervalSeconds * 1000;
            repoRefreshTimer.Tick += RepoRefreshTimer_Tick;
            repoRefreshTimer.Start();


        }


        private async Task InitRepoPaging()
        {
            await FetchAllRepos();
            ShowRepoPage(currentPage);
        }

        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        private JArray allRepos;
        private int currentPage = 1;
        private int reposPerPage = 4;

        private int refreshIntervalSeconds = 10; // How often to refresh (e.g. every 10 seconds)

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
            if (repoRefreshTimer != null) repoRefreshTimer.Stop();
        }


  

        private async Task FetchAllRepos()
        {
            allRepos = new JArray();
            int page = 1;
            const int safeMaxPages = 1000;
            const int maxReposAllowed = 100_000;
            int totalLoaded = 0;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.UserAgent.ParseAdd("DevConnectApp");
                    client.DefaultRequestHeaders.Authorization =
                        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

                    while (page <= safeMaxPages && totalLoaded < maxReposAllowed)
                    {
                        var response = await client.GetAsync($"https://api.github.com/user/repos?per_page=100&page={page}");
                        string json = await response.Content.ReadAsStringAsync();
                        var batch = JArray.Parse(json);

                        if (batch.Count == 0)
                            break;

                        foreach (var repo in batch)
                        {
                            allRepos.Add(repo);
                            totalLoaded++;

                            if (totalLoaded >= maxReposAllowed)
                            {
                                MessageBox.Show("You've reached the max supported repository limit (100,000).");
                                break;
                            }
                        }

                        page++;
                    }

                    if (page == safeMaxPages)
                    {
                        MessageBox.Show("You've reached the max page limit (1,000 pages). Some repositories may not be shown.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to fetch repositories.\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowRepoPage(int page)
        {
            flowPanelRepos.Controls.Clear();

            var sorted = allRepos
                .OrderByDescending(r => (int?)r["stargazers_count"] ?? 0)
                .ToList();

            int start = (page - 1) * reposPerPage;
            int end = Math.Min(start + reposPerPage, sorted.Count);

            if (start >= sorted.Count)
            {
                MessageBox.Show("No more repositories.");
                return;
            }

            for (int i = start; i < end; i++)
            {
                var repo = sorted[i];
                string name = repo["name"]?.ToString();
                string desc = repo["description"]?.ToString() ?? "No description";
                string lang = repo["language"]?.ToString() ?? "Unknown";
                string htmlUrl = repo["html_url"]?.ToString();

                string fullName = repo["full_name"]?.ToString(); // e.g. joec2k/CoaScannerApp
                var card = CreateRepoCard(name, desc, lang, fullName, htmlUrl);
                flowPanelRepos.Controls.Add(card);
            }
        }


        public static string ToFriendlyTime(DateTime dt)
        {
            TimeSpan diff = DateTime.UtcNow - dt;
            if (diff.TotalSeconds < 60)
                return "just now";
            if (diff.TotalMinutes < 60)
                return $"{(int)diff.TotalMinutes} minutes ago";
            if (diff.TotalHours < 24)
                return $"{(int)diff.TotalHours} hours ago";
            if (diff.TotalDays < 7)
                return $"{(int)diff.TotalDays} days ago";
            if (dt.Year == DateTime.UtcNow.Year)
                return dt.ToString("MMM dd");
            return dt.ToString("MMM dd, yyyy");
        }


        private async Task OpenRepoPropForm(string repoName, string fullName, string htmlUrl)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.UserAgent.ParseAdd("DevConnectApp");
                    client.DefaultRequestHeaders.Authorization =
                        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

                    string apiUrl = $"https://api.github.com/repos/{fullName}/contents";
                    var response = await client.GetAsync(apiUrl);
                    string json = await response.Content.ReadAsStringAsync();

                    Console.WriteLine($"[DEBUG] Response from {apiUrl}:\n{json}");

                    if (!response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("API access failed. Opening repository in your browser...", "Redirecting", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Process.Start(new ProcessStartInfo(htmlUrl) { UseShellExecute = true });
                        return;
                    }

                    JToken token = JToken.Parse(json);

                    if (token.Type == JTokenType.Array)
                    {
                        JArray files = (JArray)token;
                        User_RepoProp propForm = new User_RepoProp();
            
                        var fileCards = new List<(string, string, string)>();

                        foreach (var file in files)
                        {
                            

                            string name = file["name"]?.ToString();
                            string type = file["type"]?.ToString();

                            // Commit time logic as before:
                            string commitTimeString = "Unknown";
                            try
                            {
                                string commitsApi = $"https://api.github.com/repos/{fullName}/commits?path={name}&per_page=1";
                                var commitResponse = await client.GetAsync(commitsApi);
                                if (commitResponse.IsSuccessStatusCode)
                                {
                                    string commitJson = await commitResponse.Content.ReadAsStringAsync();
                                    var commitArr = JArray.Parse(commitJson);
                                    if (commitArr.Count > 0)
                                    {
                                        var commitObj = commitArr[0];
                                        var dateStr = commitObj["commit"]["committer"]["date"]?.ToString()
                                                   ?? commitObj["commit"]["author"]["date"]?.ToString();
                                        if (dateStr != null)
                                        {
                                            DateTime dt = DateTime.Parse(dateStr).ToUniversalTime();
                                            commitTimeString = ToFriendlyTime(dt);
                                        }
                                    }
                                }
                            }
                            catch { }

                            fileCards.Add((name, type, commitTimeString));
                        }

                        // Show the modern card UI:
                        User_RepoProp User_propForm = new User_RepoProp();
                        User_propForm.ShowFileCards(fileCards);
                        propForm.labelTitle.Text = fullName;
                        User_propForm.Show();
                    }
                    else
                    {
                        MessageBox.Show("Unexpected data format received.", "API Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred.\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private Panel CreateRepoCard(string name, string description, string language, string fullName, string htmlUrl)

        {
            Panel panel = new Panel
            {
                Width = 350,
                Height = 100,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(10),
                Cursor = Cursors.Hand
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
            panel.Click += async (s, e) =>
            {
                //Cursor.Current = Cursors.WaitCursor; // Show spinner
                this.Cursor = Cursors.WaitCursor;
                panel.Enabled = false; // Optional: prevent double click

                try
                {
                    await OpenRepoPropForm(name, fullName, htmlUrl);
                }
                finally
                {
                    // Cursor.Current = Cursors.Default; // Restore cursor
                    this.Cursor = Cursors.Default;
                    panel.Enabled = true;
                }
            }; ;
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

        private void User_Profile_Load(object sender, EventArgs e)
        {
            BtnBackward.Visible = false;
        }

        private void BtnForward_Click(object sender, EventArgs e)
        {
          

            int maxPage = (int)Math.Ceiling((double)allRepos.Count / reposPerPage);
            if (currentPage < maxPage)
            {
                BtnBackward.Visible = true;
                currentPage++;
                ShowRepoPage(currentPage);
            }
            else
            {
                // Instead of showing MessageBox here, just clamp and quietly ignore
                currentPage = maxPage > 0 ? maxPage : 1;
                // Optionally show a non-intrusive toast/snackbar message instead
            }
        }
      

        private void BtnBackward_Click(object sender, EventArgs e)
        {
           

            if (currentPage > 1)
            {
                BtnForward.Visible = true;
                currentPage--;
                ShowRepoPage(currentPage);
            }
            else
            {
                currentPage = 1;
                // Optionally, no MessageBox
            }
        }

        private async void RepoRefreshTimer_Tick(object sender, EventArgs e)
        {
           

            int prevRepoCount = allRepos?.Count ?? 0;
            await FetchAllRepos();
            int newRepoCount = allRepos?.Count ?? 0;
            int maxPage = (int)Math.Ceiling((double)newRepoCount / reposPerPage);

            // Clamp currentPage to be in valid range
            if (currentPage > maxPage)
                currentPage = maxPage > 0 ? maxPage : 1;
            if (currentPage < 1)
                currentPage = 1;

            ShowRepoPage(currentPage);

            if (newRepoCount < prevRepoCount)
            {
                MessageBox.Show("A repository was deleted. List updated.", "Repo Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (newRepoCount > prevRepoCount)
            {
                MessageBox.Show("New repository detected!", "New Repo Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

    }
}
