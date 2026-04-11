# Build and Execute Caisse Claire

This document provides instructions for an AI or developer to generate a standalone executable for the Caisse Claire project.

## Prerequisites
- .NET 10 SDK installed.
- Windows PowerShell (for icon generation).

## Step 1: Generate the Application Icon
Before building, ensure the application icon is generated from the font glyph. This script requires the `Segoe MDL2 Assets` font (standard on Windows).

Run the following command from the project root:
```powershell
powershell -ExecutionPolicy Bypass -File src\Directive\generate_icon.ps1
```
*Note: This generates `src\Directive\Resources\app_icon.ico`.*

## Step 2: Build the Standalone Executable
We use the `dotnet publish` command to create a self-contained, single-file executable.

Run the following command from the project root:
```powershell
dotnet publish src\Directive\CaisseClaire.Directive.csproj `
    -c Release `
    -r win-x64 `
    --self-contained true `
    -p:PublishSingleFile=true `
    -p:IncludeNativeLibrariesForSelfExtract=true `
    -p:PublishReadyToRun=true `
    -o out
```

## Step 3: Run the Executable
The final executable and its required data for runtime will be located in the `out/` directory.

```powershell
.\out\CaisseClaire.Directive.exe
```

## Important Notes
- **Do not push the `out/` directory**: It is already included in `.gitignore`.
- **Data persistence**: The application expects a `data/` directory relative to the executable for storing `products.csv` and `settings.json`. The build command output folder (`out`) should already contain a copy of the `data` folder if the project is configured correctly.
