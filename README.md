# Caisse Claire - Point of Sale (POS) Application

Welcome to **Caisse Claire**, a modern, lightweight, and robust Point of Sale (POS) system built on the .NET 10 WPF framework. This desktop application is specifically designed to manage store transactions, cart calculation, catalog configuration, and PDF reporting.

![Caisse Claire Architecture](docs/architecture.md) _(Conceptual)_

---

## 🌟 Key Features

### 🛒 High-Performance Cart Management
- Easily ring up products using quick 3-digit access codes.
- Add "Custom/Unknown Items" instantly to accommodate dynamic pricing on-the-fly.
- **Accurate Math:** Automatically calculates total prices and provides the exact "Change to Return" based on the cash given by the client.
- **Cart Editing:** Misclicked an item? Simply select it and remove it securely before checkout.

### 📦 Built-In Catalog Manager
- Launch the **Product Catalog** from the main UI to overview your entire inventory.
- **Add / Remove:** Update stock logic directly through the UI. It safely saves any changes to the `data/products.csv` database.
- **Soft Deletions:** Products marked for deletion turn red to visually warn you before permanent removal.
- **Intuitive Visual Reordering:** Easily organize and sequence your catalog the way you want using drag-and-drop mechanics. 

### 📄 Professional Exporting
- Utilize our lightning-fast 1-click **Export to PDF**! The catalog transforms perfectly into a well-formatted A4 `CatalogExport.pdf` file instantly.

### ⚙️ User Accessibility & Settings Configuration
- **Dark Mode Support:** Switch effortlessly between Light and Dark themes to match your ambient store lighting.
- **Bilingual Capabilities:** Application supports both `English` and `Français` via dynamic real-time resource dictionary swapping.
- **Visual Scaling:** Text too small? Use the font scaling slider to increase readability up to 150%.
- Preferences are strictly remembered across sessions within `data/settings.json`.

---

## 🛠️ Technology Stack
This project follows a strict **3-Layer Architecture** separating Intent, Decision Making, and System Calls:

| Concept | Technology |
|---|---|
| **UI Environment** | Windows Presentation Foundation (WPF) |
| **Language & Runtime** | C# 13 / .NET 10 |
| **Logic Paradigms** | Orchestrator & Repository Patterns |
| **Drag & Drop** | Gong-WPF-DragDrop Framework |
| **PDF Renderer** | QuestPDF Community |

---

## 🚀 How to Run

Running Caisse Claire from source is incredibly simple using the .NET CLI.

1. Ensure the `.NET 10 SDK` represents your default developer environment.
2. Open a terminal at the very root of this project (next to `CaisseClaire.slnx`).
3. Run the following command:
```powershell
dotnet run --project src\Directive\CaisseClaire.Directive.csproj
```

**Why from the root?**
Running strictly from the root properly informs the `Execution` layer to correctly resolve `data/products.csv` and `data/settings.json`.

_Happy selling with Caisse Claire! 💰🎊_