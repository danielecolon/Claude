# Demo 1: Three stores, three readers

**What you'll learn:** how Claude Code can hand work to several sub-agents that
run **at the same time**, each with its own clean context, and then combine
their short answers.

**The use case:** you have customer comments from three stores. Instead of one
Claude reading everything, three helpers each read one store's file in
parallel. The main Claude only sees their two-line summaries.

**Time:** about 1 minute.

## What's in this folder

```
demo1-parallel-readers/
├── README.md          (this file)
└── feedback/
    ├── nashua.txt     (6 customer comments)
    ├── boston.txt     (6 customer comments)
    └── portland.txt   (6 customer comments)
```

There is no agent file in this demo. Claude Code uses its built-in
general-purpose sub-agent.

## Before you start

- Install Claude Code and sign in. See https://code.claude.com/docs
- Check it works: `claude --version`

## Run the demo

1. Open a terminal in this folder:

   ```bash
   cd demo1-parallel-readers
   ```

2. Start Claude Code:

   ```bash
   claude
   ```

3. Paste this prompt and press Enter:

   ```text
   Use three sub-agents in parallel, one per file in the feedback folder. Each one returns the top complaint and the top compliment for its store, in two lines. Then give me one table comparing the three stores.
   ```

4. If Claude asks for permission to read files, choose **Yes**.

## What you should see

1. Three sub-agent tasks start at almost the same moment, with names like
   "Analyze Nashua feedback", "Analyze Boston feedback", and
   "Analyze Portland feedback".
2. Press **Ctrl+O** if you want to expand the output and see each sub-agent
   reading only its own file.
3. A table comes back, roughly like this (the wording will vary):

   | Store | Top complaint | Top compliment |
   |---|---|---|
   | Nashua | Long Saturday checkout lines | New coffee bar |
   | Boston | Self-checkout freezes | Fresh produce |
   | Portland | Oat milk out of stock | Friendly deli staff |

## Why it matters

- **Parallel:** three files were read at the same time, not one after another.
- **Clean context:** the main agent never read the raw comments. It got three
  short summaries. Picture the same prompt with 300 stores.

## Try next

- Add a fourth file, such as `feedback/concord.txt`, and run the prompt again.
  Claude starts four sub-agents.
- Ask: `Which store needs help first, and why?` The main agent answers from the
  summaries it already has.

## Troubleshooting

- **Claude reads the files itself instead of using sub-agents:** make sure your
  prompt starts with "Use three sub-agents in parallel".
- **It can't find the files:** check that you started `claude` inside
  `demo1-parallel-readers`, not one folder above it.
