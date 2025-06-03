using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace git_hub_app
{
    public partial class User_Login : Form
    {
        // Your GitHub OAuth credentials
        private string clientId = "Ov23liohF6RYuO1rpxnC"; //Change this is actual client ID
        private string clientSecret = "46f35ba7ffcee17ca9fa62d9bd379d6ce3bc69db"; //this is actual client secret, do not share it publicly
        private string redirectUri = "http://localhost:5000/callback";

        public User_Login()
        {
            InitializeComponent();
        }


        private async Task<string> LoginAsync()
        {
            string state = Guid.NewGuid().ToString("N");
            string authUrl = $"https://github.com/login/oauth/authorize?client_id={clientId}&redirect_uri={redirectUri}&scope=read:user&state={state}";

            // Open browser
            Process.Start(new ProcessStartInfo(authUrl) { UseShellExecute = true });

            // Start local server for redirect
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

            using (HttpClient client = new HttpClient())
            {
                var postData = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("client_id", clientId),
                    new KeyValuePair<string, string>("client_secret", clientSecret),
                    new KeyValuePair<string, string>("code", code),
                    new KeyValuePair<string, string>("redirect_uri", redirectUri)
                });

                client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
                var response = await client.PostAsync("https://github.com/login/oauth/access_token", postData);
                var content = await response.Content.ReadAsStringAsync();
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
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                    client.DefaultRequestHeaders.UserAgent.ParseAdd("DevConnectDesktop");

                    string json = await client.GetStringAsync("https://api.github.com/user");
                    JObject user = JObject.Parse(json);

                    string username = user["login"]?.ToString();
                    string name = user["name"]?.ToString();
                    string email = user["email"]?.ToString();
                    string avatarUrl = user["avatar_url"]?.ToString();
                    string bio = user["bio"]?.ToString();

                    // Open profile form
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
    }
}
