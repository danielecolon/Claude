# Demo 3: Fresh-eyes code review

**What you'll learn:** the **reviewer** pattern. A sub-agent that has never
seen the code reviews it with a fresh context. It reports problems, and the
main agent fixes them.

**The use case:** a small C# class has some bugs. A read-only `code-reviewer`
sub-agent finds them, and the main Claude makes the edits, the same way a pull
request works.

**Time:** about 1 minute.

## What's in this folder

```
demo3-code-reviewer/
├── README.md                     (this file)
├── PriceCalculator.cs            (about 20 lines of C# with a few bugs)
└── .claude/
    └── agents/
        └── code-reviewer.md      (the sub-agent)
```

> The `.claude` folder starts with a dot, so it may be hidden. Use `ls -a`
> (macOS/Linux) or `dir /a` (Windows) to see it.

You don't need .NET installed. The reviewer only reads the code.

## The sub-agent file

`.claude/agents/code-reviewer.md`:

```markdown
---
name: code-reviewer
description: Reviews C# code for bugs and edge cases with fresh eyes. Use after code is written or changed. Read-only; reports findings, never edits.
tools: Read, Grep, Glob
model: sonnet
---
You are a senior C# reviewer seeing this code for the first time.

1. Read the files you are given.
2. Find real bugs first: wrong math, wrong conditions, crashes, missed edge cases.
3. For each finding give: file and line, what goes wrong, a sample input
   that shows it, and the fix in one line.
4. Skip style nitpicks.
5. Reply in under 150 words. Never edit files.
```

`tools: Read, Grep, Glob` lets it read and search, but not edit or run
commands. `model: sonnet` gives it a stronger model for reasoning about code.

## Before you start

- Install Claude Code and sign in. See https://code.claude.com/docs
- Check it works: `claude --version`
- **Make a backup**, because this demo changes `PriceCalculator.cs`:

  ```bash
  cp PriceCalculator.cs ../PriceCalculator.cs.bak      # macOS/Linux
  copy PriceCalculator.cs ..\PriceCalculator.cs.bak    # Windows
  ```

  The backup goes one folder up so the reviewer doesn't read it.

## Run the demo

1. Open a terminal in this folder:

   ```bash
   cd demo3-code-reviewer
   ```

2. Start Claude Code:

   ```bash
   claude
   ```

3. Optional: type `/agents` to check that `code-reviewer` is listed, then press **Esc**.

4. Paste this prompt and press Enter:

   ```text
   Ask the code-reviewer sub-agent to review PriceCalculator.cs. Then fix every bug it finds and show me the diff.
   ```

5. When Claude asks to edit `PriceCalculator.cs`, choose **Yes**. (Or press
   **Shift+Tab** before step 4 to switch to accept-edits mode.)

## What you should see

1. A `code-reviewer` sub-agent task starts and reads `PriceCalculator.cs`.
2. It returns a short list of findings, each with a line, a sample input, and a fix.
3. The main agent edits the file and shows you the diff.

<details>
<summary>Answer key (open after you run it)</summary>

The three planted bugs:

1. The member discount is `0.01m` (1%), but the comment says 10%.
   `Total(200, true)` returns 198 instead of 180.
2. Free shipping uses `> 100m`, so an order of exactly $100 still pays $5.
   It should be `>= 100m`.
3. `AveragePrice` divides by zero when the cart is empty.

The reviewer often finds extras too, such as a `null` array or a negative
subtotal. That's the point of fresh eyes.

</details>

## Why it matters

- **Fresh context:** the reviewer hasn't seen how the code was written, so it
  isn't blind to the author's assumptions.
- **Least privilege:** the reviewer can't change anything. It reports, and the
  main agent edits.
- **Separate roles:** writer and reviewer stay apart, like a pull request.

## Try next

- Ask Claude to write a new method, for example
  `Add a method that applies a coupon code`, then
  `Have the code-reviewer check your change`.
- Remove `model: sonnet` from the agent file so it uses the same model as your
  main session.

## Reset the demo

Put the original file back:

```bash
cp ../PriceCalculator.cs.bak PriceCalculator.cs      # macOS/Linux
copy ..\PriceCalculator.cs.bak PriceCalculator.cs    # Windows
```

If you cloned the folder with git, `git checkout PriceCalculator.cs` also works.

## Troubleshooting

- **`code-reviewer` isn't listed in `/agents`:** make sure you started `claude`
  inside `demo3-code-reviewer`. If you added the file while Claude was running,
  restart it.
- **Claude reviews the code itself:** name the sub-agent in your prompt, as the
  prompt above does.
- **No bugs found:** the file may already be fixed from an earlier run. Reset it
  (see above) and try again.
