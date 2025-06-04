using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace git_hub_app
{
    public partial class User_Login : Form
    {
        private string clientId = "Ov23liohF6RYuO1rpxnC"; // Keep only the client ID (it's safe to share)
        private string redirectUri = "http://localhost:5000/callback";
        private string backendTokenExchangeUrl = "https://git-hub-app-93qb.onrender.com/auth/github";

        public User_Login()
        {
            InitializeComponent();

            DrawCustomNameGrid();
        }

        private async Task<string> LoginAsync()
        {
            string state = Guid.NewGuid().ToString("N");
            string authUrl = $"https://github.com/login/oauth/authorize?client_id={clientId}&redirect_uri={redirectUri}&scope=read:user&state={state}";

            // Open GitHub OAuth login in browser
            Process.Start(new ProcessStartInfo(authUrl) { UseShellExecute = true });

            // Start localhost listener for GitHub redirect
            var http = new HttpListener();
            http.Prefixes.Add("http://localhost:5000/callback/");
            http.Start();

            var context = await http.GetContextAsync();
            var query = context.Request.QueryString;
            string code = query["code"];
            string receivedState = query["state"];

            // Respond to browser
            string html = "<html><body><h2>You may now close this window.</h2></body></html>";
            byte[] buffer = Encoding.UTF8.GetBytes(html);
            context.Response.ContentLength64 = buffer.Length;
            await context.Response.OutputStream.WriteAsync(buffer, 0, buffer.Length);
            http.Stop();

            if (state != receivedState) return null;

            // Call your backend to exchange code for token
            using (HttpClient client = new HttpClient())
            {
                string requestUrl = $"{backendTokenExchangeUrl}?code={code}";
                var response = await client.GetAsync(requestUrl);
                string content = await response.Content.ReadAsStringAsync();
                var tokenData = JObject.Parse(content);

                return tokenData["access_token"]?.ToString();
            }
        }

        private async void loginButton_Click(object sender, EventArgs e)
        {
            string token = await LoginAsync();

            if (token != null)
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization =
                        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                    client.DefaultRequestHeaders.UserAgent.ParseAdd("DevConnectDesktop");

                    string json = await client.GetStringAsync("https://api.github.com/user");
                    JObject user = JObject.Parse(json);

                    string username = user["login"]?.ToString();
                    string name = user["name"]?.ToString();
                    string email = user["email"]?.ToString();
                    string avatarUrl = user["avatar_url"]?.ToString();
                    string bio = user["bio"]?.ToString();

                    // Show profile
                    User_Profile profileForm = new User_Profile(username, name, email, avatarUrl, bio);
                    profileForm.Show();
                    this.Hide();
                }
            }
            else
            {
                MessageBox.Show("Login failed.");
            }
        }



        private Color GetLevelColor(int level)
        {
            if (level == 1)
                return ColorTranslator.FromHtml("#9be9a8");
            else if (level == 2)
                return ColorTranslator.FromHtml("#40c463");
            else if (level == 3)
                return ColorTranslator.FromHtml("#30a14e");
            else if (level == 4)
                return ColorTranslator.FromHtml("#216e39");
            else
                return ColorTranslator.FromHtml("#ebedf0"); // level 0 or unknown
        }

        private async void DrawCustomNameGrid()
        {
            tableLayoutContrib.Controls.Clear();
            tableLayoutContrib.ColumnStyles.Clear();
            tableLayoutContrib.RowStyles.Clear();
            tableLayoutContrib.AutoSize = false;
            tableLayoutContrib.RowCount = 7;

            int boxSize = 12;
            int boxMargin = 2;

            int paddingCols = 2;
            int spacing = 2; // space between letters
            int letterWidth = 6; // width of each letter (max)
            int[][,] letters = new int[][,]
            {
        // D
        new int[,]
        {
            {1,1,1},
            {1,0,1},
            {1,0,1},
            {1,0,1},
            {1,0,1},
            {1,0,1},
            {1,1,1}
        },
        // U
        new int[,]
        {
            {1,0,1},
            {1,0,1},
            {1,0,1},
            {1,0,1},
            {1,0,1},
            {1,0,1},
            {1,1,1}
        },
        // C
        new int[,]
        {
            {1,1,1},
            {1,0,0},
            {1,0,0},
            {1,0,0},
            {1,0,0},
            {1,0,0},
            {1,1,1}
        },
        // L
        new int[,]
        {
            {1,0,0},
            {1,0,0},
            {1,0,0},
            {1,0,0},
            {1,0,0},
            {1,0,0},
            {1,1,1}
        },
        // E
        new int[,]
        {
            {1,1,1},
            {1,0,0},
            {1,0,0},
            {1,1,0},
            {1,0,0},
            {1,0,0},
            {1,1,1}
        }
            };

            int totalCols = paddingCols + (letters.Length * (letterWidth + spacing));
            tableLayoutContrib.ColumnCount = totalCols;

            for (int col = 0; col < totalCols; col++)
                tableLayoutContrib.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, boxSize + boxMargin));
            for (int row = 0; row < 7; row++)
                tableLayoutContrib.RowStyles.Add(new RowStyle(SizeType.Absolute, boxSize + boxMargin));

            int colOffset = paddingCols;

            Random rnd = new Random();

            foreach (var letter in letters)
            {
                for (int row = 0; row < letter.GetLength(0); row++)
                {
                    for (int col = 0; col < letter.GetLength(1); col++)
                    {
                        int val = letter[row, col];

                        Panel square = new Panel
                        {
                            Width = boxSize,
                            Height = boxSize,
                            Margin = new Padding(1),
                            BackColor = val == 1
                                ? GetLevelColor(rnd.Next(1, 5))  // ✅ 1 to 4 only
                                : Color.WhiteSmoke
                        };

                        tableLayoutContrib.Controls.Add(square, colOffset + col, row);
                    }
                }

                colOffset += letter.GetLength(1) + spacing;
                await Task.Delay(450);
            }
        }


    }
}
