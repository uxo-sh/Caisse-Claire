# CaisseClaire — Architecture

## 3-Layer Cognitive Architecture

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

### Layer 1 — Directive (CaisseClaire.Core)
Captures and clarifies user intent. Translates raw input into structured, actionable goals.

### Layer 2 — Orchestration (CaisseClaire.Core/Services + CaisseClaire.Data)
The brain — decides how to achieve goals, which tools to use, handles failures and retries.

### Layer 3 — Execution (CaisseClaire.UI + CaisseClaire.Data)
Carries out operations: Windows APIs, databases, file systems, UI components.

## Tech Stack

| Component         | Technology                          |
|-------------------|-------------------------------------|
| UI Layer          | WPF (.NET 8)                        |
| AI Backbone       | Claude API (claude-sonnet-4)        |
| Orchestration     | Custom C# Agent Loop                |
| Execution Tools   | .NET 8, Win32 API, SQL Server, REST |
| Logging           | Serilog + Windows Event Log         |
| Config Management | appsettings.json + Environment Vars |
