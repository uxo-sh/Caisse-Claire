# 🤖 WinArchBot — AI Chatbot for Windows Application Architecture

> An intelligent assistant that understands, explains, and applies the **3-Layer Architecture** for building Windows applications — powered by a structured AI reasoning pipeline.

---

## 📐 Architecture Overview

WinArchBot is built on a **3-layer cognitive architecture** that mirrors how a professional software engineer thinks when designing Windows applications. Each layer has a clear responsibility and feeds into the next.

```
┌─────────────────────────────────────────────────────┐
│              USER / WINDOWS APPLICATION              │
└──────────────────────┬──────────────────────────────┘
                       │
          ┌────────────▼────────────┐
          │   LAYER 1 — DIRECTIVE   │  ← What to do
          │   (Intent & Goals)      │
          └────────────┬────────────┘
                       │
          ┌────────────▼────────────┐
          │ LAYER 2 — ORCHESTRATION │  ← Decision-making
          │   (Planning & Routing)  │
          └────────────┬────────────┘
                       │
          ┌────────────▼────────────┐
          │   LAYER 3 — EXECUTION   │  ← Doing the work
          │   (Tools & Actions)     │
          └─────────────────────────┘
```

---

## 🗂️ Layer Descriptions

### Layer 1 — Directive *(What to do)*

The **Directive Layer** is responsible for capturing and clarifying intent. It translates raw user input into structured, actionable goals.

**Responsibilities:**
- Parse natural language requests from the Windows UI
- Define the objective clearly (e.g., "Create a new window form", "Fetch user data", "Generate a report")
- Set constraints and expected outcomes
- Forward a structured goal to the Orchestration layer

**Key Concepts:**
- Intent extraction
- Goal decomposition
- Input validation and clarification
- Priority setting

```
User Input: "Show me the latest sales report filtered by region"
      ↓
Directive Output: {
  action: "generate_report",
  filters: { type: "sales", dimension: "region" },
  output_format: "table + chart"
}
```

---

### Layer 2 — Orchestration *(Decisions)*

The **Orchestration Layer** acts as the brain. It receives structured goals and decides *how* to achieve them — which tools to use, in what order, and how to handle failures.

**Responsibilities:**
- Select the right tools, APIs, or sub-agents for the task
- Plan multi-step workflows
- Handle conditional logic and branching decisions
- Monitor execution status and retry on failure
- Coordinate between parallel or sequential tasks

**Key Concepts:**
- Task planning and sequencing
- Tool routing and selection
- Error recovery and fallback strategies
- State management across steps

```
Goal: generate_report (sales, by region)
      ↓
Plan:
  Step 1 → Query database tool (sales data)
  Step 2 → Filter by region tool
  Step 3 → Render chart tool
  Step 4 → Format output tool
```

---

### Layer 3 — Execution *(Doing the work)*

The **Execution Layer** carries out the actual operations. It interfaces directly with Windows APIs, databases, file systems, UI components, and external services.

**Responsibilities:**
- Run system-level operations (file I/O, registry, processes)
- Interact with Windows UI components (WinForms, WPF, WinUI)
- Call external APIs and databases
- Return structured results back up to the Orchestration layer
- Log all actions for traceability

**Key Concepts:**
- Tool invocation and API calls
- Windows system integration (Win32, .NET, COM)
- Result formatting and error surfacing
- Atomic, idempotent operations where possible

```
Action: Query database tool
      ↓
Execution:
  → Connect to SQL Server
  → Run: SELECT * FROM Sales WHERE Region IS NOT NULL
  → Return: [{ region: "North", total: 42000 }, ...]
```

---

## ⚙️ Operating Principles

These principles govern how WinArchBot reasons and acts at every layer.

### 1. ✅ Check Existing Tools First

Before creating or invoking anything new, the bot always audits what is already available.

- **Why:** Avoids redundant work, reduces errors, and reuses proven components.
- **How it works:**
  - At the Orchestration layer, before planning a solution, the bot queries its tool registry.
  - If a capable tool already exists (e.g., a file reader, a DB connector, a report renderer), it is reused.
  - New tools are only created when no existing tool covers the need.

```
❌ Bad: "I'll write a new SQL query tool for this."
✅ Good: "A SQL query tool already exists → reusing it with new parameters."
```

---

### 2. 🔁 Self-Correct When Something Breaks

WinArchBot does not fail silently. When an error occurs at any layer, it diagnoses, adjusts, and retries autonomously.

- **Why:** Windows application environments are complex — APIs change, resources fail, inputs are unpredictable.
- **How it works:**
  - The Orchestration layer monitors execution results for error signals.
  - On failure, it identifies the root cause (wrong tool, bad input, unavailable resource).
  - It adjusts the plan and retries with a corrected approach.
  - After a configurable number of retries, it escalates to the user with a clear explanation.

```
Step 3 → Chart render tool FAILED (null data returned)
      ↓
Self-correction:
  → Re-check Step 2 output → empty result set detected
  → Retry Step 1 with broader date range
  → Proceed with corrected data
```

---

## 🛠️ Tech Stack (Windows Application)

| Component         | Technology                          |
|-------------------|-------------------------------------|
| UI Layer          | WPF / WinUI 3 / WinForms            |
| AI Backbone       | Claude API (claude-sonnet-4)        |
| Orchestration     | Custom C# Agent Loop                |
| Execution Tools   | .NET 8, Win32 API, SQL Server, REST |
| Logging           | Serilog + Windows Event Log         |
| Config Management | appsettings.json + Environment Vars |

---

## 🚀 Getting Started

### Prerequisites

- Windows 10/11
- .NET 8 SDK
- Visual Studio 2022+
- Anthropic API Key

### Installation

```bash
git clone https://github.com/your-org/winarchbot.git
cd winarchbot
dotnet restore
```

### Configuration

```json
// appsettings.json
{
  "Anthropic": {
    "ApiKey": "YOUR_API_KEY",
    "Model": "claude-sonnet-4-20250514"
  },
  "Bot": {
    "MaxRetries": 3,
    "ToolRegistryPath": "./tools"
  }
}
```

### Run

```bash
dotnet run --project WinArchBot.App
```

---

## 📁 Project Structure

```
WinArchBot/
├── Layer1_Directive/
│   ├── IntentParser.cs
│   ├── GoalDecomposer.cs
│   └── InputValidator.cs
├── Layer2_Orchestration/
│   ├── Planner.cs
│   ├── ToolRouter.cs
│   ├── ErrorRecovery.cs
│   └── StateManager.cs
├── Layer3_Execution/
│   ├── Tools/
│   │   ├── DatabaseTool.cs
│   │   ├── FileSystemTool.cs
│   │   └── ReportTool.cs
│   ├── Executor.cs
│   └── ResultFormatter.cs
├── Shared/
│   ├── Models/
│   └── Logging/
└── WinArchBot.App/
    └── MainWindow.xaml
```

---

## 📜 License

This project is **free to use** — no restrictions, no cost, no attribution required.
Feel free to use, modify, and distribute it for any purpose, personal or commercial.

---

## 🤝 Contributing

Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

---

*Built with the 3-Layer Architecture pattern for robust, maintainable Windows AI applications.*