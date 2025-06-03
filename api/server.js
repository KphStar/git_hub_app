const express = require("express");
const axios = require("axios");
const cors = require("cors");

const app = express();
app.use(cors());
const PORT = process.env.PORT || 3000;

app.get("/auth/github", async (req, res) => {
  const code = req.query.code;
  const client_id = process.env.GITHUB_CLIENT_ID;
  const client_secret = process.env.GITHUB_CLIENT_SECRET;

  try {
    const response = await axios.post(
      "https://github.com/login/oauth/access_token",
      {
        client_id,
        client_secret,
        code,
      },
      {
        headers: {
          Accept: "application/json",
        },
      }
    );

    res.json(response.data); // includes access_token
  } catch (error) {
    res.status(500).json({ error: "Token exchange failed" });
  }
});

app.get("/", (req, res) => {
  res.send("OAuth Server Running!");
});

app.listen(PORT, () => {
  console.log(`Server listening on port ${PORT}`);
});
