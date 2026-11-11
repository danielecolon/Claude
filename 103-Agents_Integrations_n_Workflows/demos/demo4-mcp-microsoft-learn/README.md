# Demo 4: Microsoft Learn MCP (Claude reads the official Microsoft docs)

**What you'll learn:** a remote MCP server that needs **no sign-in at all**.
One URL in `.mcp.json` and Claude can search, read, and pull code samples
from learn.microsoft.com.

**The use case in one sentence:** "Before Claude answers an Azure or .NET
question, it checks what Microsoft's docs say today, and tells you where."

**Server:** Microsoft's official Learn MCP server
([MicrosoftDocs/mcp](https://github.com/MicrosoftDocs/mcp)), hosted at
`https://learn.microsoft.com/api/mcp`. Free, no account, no API key.

**Time:** about 2 minutes live, roughly 45 to 60 seconds for the prompt.
⚠️ Not tested end to end yet: my test machine can't reach
learn.microsoft.com. **Do a full dry run before the talk.**

## What's in this folder

```
demo4-mcp-microsoft-learn/
├── README.md    (this file)
└── .mcp.json    (points Claude Code at Microsoft's hosted server)
```

## The idea in one picture

```
 You ──prompt──▶ Claude Code ──HTTPS (no token)──▶ learn.microsoft.com/api/mcp
                     ▲                                       │
                     └──── doc excerpts, full pages, code ◀──┘
```

The server has three tools:

| Tool | What it does |
|---|---|
| `microsoft_docs_search` | Searches Microsoft Learn and returns the best short excerpts with links |
| `microsoft_docs_fetch` | Reads one whole Learn page (as Markdown) when an excerpt isn't enough |
| `microsoft_code_sample_search` | Finds official code samples, optionally for one language (e.g. `csharp`) |

`.mcp.json`:

```json
{
  "mcpServers": {
    "microsoft-learn": {
      "type": "http",
      "url": "https://learn.microsoft.com/api/mcp"
    }
  }
}
```

Compare with the GitHub demo: same `"type": "http"`, but **no `headers`**.
The docs are public, so there's nothing to sign in to.

## Before you start

- Claude Code installed and signed in. No Node.js needed for this one.
- One dry run before the talk, so you've approved the server once.
- Nothing to create, nothing to delete afterwards.

## Run the demo

1. Start Claude Code in this folder:

   ```bash
   cd 5-microsoft-learn
   claude
   ```

2. Approve the `microsoft-learn` server from this project when asked.

3. Type `/mcp`, open `microsoft-learn`: it should say connected and list the
   three tools above. **Esc**.

4. Paste:

   ```text
   Using the Microsoft Learn docs: what's the recommended way for an ASP.NET Core app on Azure App Service to connect to Azure SQL without a password in the connection string? Give me the steps in 5 bullets, a short C# sample, and the Learn links you used.
   ```

5. Approve the tools when asked. If Claude asks to write files, say **No**:
   we only want the answer in the chat.

## What you should see

1. `microsoft-learn - microsoft_docs_search` (one or more), maybe a
   `microsoft_docs_fetch` for a full page, and a
   `microsoft_code_sample_search` with language `csharp`.
   **Ctrl+O** shows the doc excerpts that came back.
2. Five bullets about **managed identity** (turn it on for the App Service,
   make it a user in the database, use `Authentication=Active Directory
   Default` in the connection string), a short C# snippet, and real
   `learn.microsoft.com` links you can click.

**Say:** "This is the .NET user group's favourite library: the Microsoft docs.
No install, no token, just a URL. Claude stops guessing from training data and
quotes the page, with the link, so you can check it. Same MCP plug again;
this server's job is *official knowledge*."

## Try next

- Something recent, where training data goes stale:
  `Search Microsoft Learn: what's new in the latest .NET release? 3 bullets with links.`
- A CLI command:
  `Using Microsoft Learn, give me the az CLI command to create a Linux App Service plan on the free tier.`
- Context7 vs. Learn: Context7 covers thousands of libraries (any vendor);
  Learn is Microsoft's own docs, including Azure portal steps and concepts,
  not just API reference.
- Make it automatic: add the line `Always check Microsoft Learn for Azure and
  .NET questions.` to a `CLAUDE.md` file in this folder.

## Troubleshooting

- **`/mcp` shows `microsoft-learn` failed:** the network can't reach
  `learn.microsoft.com` on port 443 (that's what happened on my test machine).
  Try another network or a hotspot.
- **Opening the URL in a browser gives an error page:** that's normal. It
  only speaks MCP, not web pages.
- **Claude answers without calling the server:** keep "Using the Microsoft
  Learn docs" in the prompt.
- **Older Claude Code says it doesn't know `"type": "http"`:** update Claude
  Code (`claude update`).
