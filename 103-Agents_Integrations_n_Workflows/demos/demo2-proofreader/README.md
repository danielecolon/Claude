# Demo 2: The proofreader

**What you'll learn:** what a custom sub-agent is made of. It's one Markdown
file with a name, a description, a short list of tools, a model, and plain
instructions.

**The use case:** a specialist whose only job is spotting typos. It can
**read** files but can't **edit** them, and it runs on Haiku, the small, fast model.

**Time:** about 1 minute.

## What's in this folder

```
demo2-proofreader/
├── README.md                     (this file)
├── welcome.md                    (a meetup welcome note with a few mistakes)
└── .claude/
    └── agents/
        └── proofreader.md        (the sub-agent)
```

> The `.claude` folder starts with a dot, so it may be hidden. Use `ls -a`
> (macOS/Linux) or `dir /a` (Windows) to see it.

## The sub-agent file

`.claude/agents/proofreader.md`:

```markdown
---
name: proofreader
description: Checks text files for spelling, grammar, and wording mistakes. Use when asked to proofread or check writing. Reports problems only; never edits.
tools: Read
model: haiku
---
You are a careful proofreader.

1. Read the file you are given.
2. List every spelling, grammar, or wording mistake as a numbered list:
   line number, the wrong text, and the fix.
3. Do not rewrite the whole document and do not edit any file.
4. End with one line: "Found N problems."
```

| Line | What it does |
|---|---|
| `name` | What you call it, and what Claude calls it |
| `description` | Tells the main agent **when** to use this sub-agent |
| `tools: Read` | The only tool it gets, so it can't edit or run commands |
| `model: haiku` | Uses the smaller, faster, cheaper model |
| Text below `---` | The sub-agent's instructions |

Claude Code loads any file in `.claude/agents/` as a project sub-agent.

## Before you start

- Install Claude Code and sign in. See https://code.claude.com/docs
- Check it works: `claude --version`

## Run the demo

1. Open a terminal in this folder:

   ```bash
   cd demo2-proofreader
   ```

2. Start Claude Code:

   ```bash
   claude
   ```

3. Check that the sub-agent is loaded. Type:

   ```text
   /agents
   ```

   You should see `proofreader` under project agents. Open it to see its tools
   and model, then press **Esc** to close the menu.

4. Paste this prompt and press Enter:

   ```text
   Use the proofreader sub-agent to check welcome.md. Don't fix anything yet.
   ```

5. Optional: ask Claude to apply the fixes:

   ```text
   Now fix them.
   ```

   Approve the edit when Claude asks. The **main** agent makes the edit,
   because the proofreader has no edit tool.

## What you should see

1. A `proofreader` sub-agent task starts.
2. It returns a numbered list of mistakes with line numbers and fixes, ending
   with "Found 7 problems" (give or take one).
3. `welcome.md` is unchanged until you ask for the fixes in step 5.

<details>
<summary>Answer key (open after you run it)</summary>

joinning → joining, sesion → session, have learn → have learned,
Their are → There are, you has → you have, tommorow → tomorrow,
mounth → month.

</details>

## Why it matters

- A whole specialist fits in about ten lines.
- **Least privilege:** with `tools: Read`, it can only report problems.
- **Right-sized model:** proofreading is simple work, so it runs on Haiku.

## Try next

- Change `model: haiku` to `model: sonnet`, restart `claude`, and compare the results.
- Run `/agents` and choose **Create new agent** to have Claude write a new
  sub-agent for you, for example "a tone checker that flags anything rude".

## Troubleshooting

- **`proofreader` isn't listed in `/agents`:** make sure you started `claude`
  inside `demo2-proofreader`. If you added the file while Claude was running,
  restart it.
- **Claude proofreads the file itself:** name the sub-agent in your prompt, as
  the prompt above does.
- **To run it again:** if you applied the fixes, restore `welcome.md` with
  `git checkout welcome.md`, or copy the original back from where you downloaded it.
