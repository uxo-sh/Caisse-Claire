# Windows Application — Project Blueprint

> **How to use this file:** Hand this README to any AI chatbot (Claude, ChatGPT, Copilot, etc.) and ask it to initialize the project. The AI will use this document as its source of truth for architecture decisions, folder structure, and coding rules.

---

## 🎯 Project Goal

Build a **Windows desktop application** following a strict 3-layer architecture. Every feature, module, and file must respect the layer boundaries described below.

---

## 🏗️ 3-Layer Architecture

The application is organized into 3 layers. Each layer has one responsibility. Layers only communicate downward — upper layers call lower layers, never the reverse.

```
┌─────────────────────────────────────────────────────┐
│                     USER INPUT                       │
└──────────────────────┬──────────────────────────────┘
                       │
          ┌────────────▼────────────┐
          │   LAYER 1 — DIRECTIVE   │
          │      "What to do"       │
          └────────────┬────────────┘
                       │
          ┌────────────▼────────────┐
          │ LAYER 2 — ORCHESTRATION │
          │       "Decisions"       │
          └────────────┬────────────┘
                       │
          ┌────────────▼────────────┐
          │   LAYER 3 — EXECUTION   │
          │    "Doing the work"     │
          └─────────────────────────┘
```

---

### Layer 1 — Directive *(What to do)*

**Role:** Capture the user's intent and translate it into a clear, structured instruction.

- Receives raw input from the UI (button clicks, form submissions, commands)
- Validates and interprets what the user wants
- Produces a structured goal/command object
- Passes it down to the Orchestration layer
- **Does NOT make decisions. Does NOT execute anything.**

**Lives in:** `src/Directive/`

---

### Layer 2 — Orchestration *(Decisions)*

**Role:** Receive the structured goal and decide how to achieve it.

- Breaks the goal into steps
- Chooses which services, tools, or handlers to call
- Manages the sequence and conditions (if/else, retries, branching)
- Handles errors and fallback strategies
- **Does NOT touch the UI. Does NOT run low-level operations directly.**

**Lives in:** `src/Orchestration/`

---

### Layer 3 — Execution *(Doing the work)*

**Role:** Carry out the actual operations — file system, database, APIs, Windows system calls.

- Performs all I/O operations (read/write files, query DB, call REST APIs)
- Interacts with Windows APIs (Win32, .NET runtime, registry, processes)
- Returns results back up to the Orchestration layer
- **Does NOT interpret intent. Does NOT make decisions.**

**Lives in:** `src/Execution/`

---

## ⚙️ Operating Principles

The AI building this project must follow these two rules at all times.

### 1. ✅ Check Existing Code First

Before generating any new class, service, or utility, the AI must check if one already exists in the project.

- Reuse existing components whenever possible
- Extend existing classes rather than duplicating logic
- Only create something new when nothing fits

### 2. 🔁 Self-Correct When Something Breaks

If a generated piece of code causes an error, the AI must:

1. Identify the root cause
2. Fix the issue without discarding working code
3. Verify the fix before moving on
4. Never leave broken code in place and move on silently

---

## 📁 Expected Project Structure

When initializing the project, the AI must create the following folder structure:

```
MyWindowsApp/
├── src/
│   ├── Directive/          # Layer 1 — Intent & input handling
│   ├── Orchestration/      # Layer 2 — Planning & decision logic
│   └── Execution/          # Layer 3 — Tools, APIs, system calls
├── tests/
│   ├── Directive.Tests/
│   ├── Orchestration.Tests/
│   └── Execution.Tests/
├── docs/
└── README.md               # This file
```

---

## 🛠️ Tech Stack

| Concern                  | Technology                  |
|--------------------------|-----------------------------|
| UI Framework             | WPF / WinUI 3 / WinForms    |
| Language                 | C# / .NET 8                 |
| Database                 | SQL Server / SQLite          |
| Logging                  | Serilog                      |
| Dependency Injection     | Microsoft.Extensions.DI     |
| Testing                  | xUnit + Moq                 |

> The AI may suggest alternatives if a better fit exists, but must explain why.

---

## 📋 Instructions for the AI

When given this README, the AI must:

1. **Read this entire file first** before writing any code
2. **Scaffold the project structure** as defined above
3. **Respect layer boundaries** — no cross-layer logic shortcuts
4. **Follow the two Operating Principles** throughout the entire build
5. **Ask for clarification** if the feature request is ambiguous before implementing it

---

## 📜 License

Free to use — no restrictions, no cost, no attribution required.
Use, modify, and distribute freely for any purpose, personal or commercial.