# Nutrition Database

A lightweight cross-platform CLI application for managing a local nutrition database using SQLite.

This tool allows you to store, search, update, and manage food nutrition information directly from the terminal without requiring any external services.

## Features

- Local SQLite database
- Fast food search
- Add, update, and remove entries
- Database status information
- File/database utilities
- Simple command-line interface
- Cross-platform support
- No internet connection required

## Commands

| Command | Description |
|---|---|
| `add` | Add a new food entry |
| `find` | Search food entries |
| `update` | Update existing entries |
| `remove` | Remove entries |
| `status` | Show database statistics |
| `info` | Show application/database information |
| `file` | File and database utilities |
| `todo` | Show planned features/tasks |
| `help` | Display help information |

---

# Requirements

- .NET SDK 8.0 or newer
- SQLite support (included via NuGet packages)

You can download the .NET SDK from:

- https://dotnet.microsoft.com/download

---
# Building the Project

Clone the repository:

```bash
git clone https://github.com/leomovskii/nutrition-database.git
cd nutrition-database
```

Build the project:

```bash
dotnet build -c Release
```

The compiled binaries will be located in:

```text
bin/Release/net8.0/
```

---
# Running the Application

Run directly using .NET:

```bash
dotnet run
```

Or run the compiled executable:

## Windows

```bash
NutritionDatabase.exe
```

## Linux

```bash
./NutritionDatabase
```

## macOS

```bash
./NutritionDatabase
```

---
# Installation

## Windows

### Option 1: Run Manually

Build the release version and launch the executable directly:

```text
bin/Release/net8.0/NutritionDatabase.exe
```

### Option 2: Add to PATH Automatically

You can use the included batch script to automatically add the application directory to the system PATH.

Example:

```bat
AddToPath.bat
```

After that, the application can be launched globally from any terminal.

Example:

```bash
ndb
```

### Option 3: Add to PATH Manually

1. Open Windows Environment Variables
2. Edit the `PATH` variable
3. Add the directory containing the executable
4. Restart the terminal

---
## Linux

Build the project:

```bash
dotnet build -c Release
```

Make the executable runnable:

```bash
chmod +x NutritionDatabase
```

Optional: move it to a global location:

```bash
sudo mv NutritionDatabase /usr/local/bin/nutrition-database
```

Then run:

```bash
nutrition-database
```

---
## macOS

Build the project:

```bash
dotnet build -c Release
```

Make the executable runnable:

```bash
chmod +x NutritionDatabase
```

Optional: move it to a global location:

```bash
sudo mv NutritionDatabase /usr/local/bin/nutrition-database
```

Then run:

```bash
nutrition-database
```

---
# Usage Examples

### Add a Food Entry

```bash
ndb add -t:Banana -c:22.8 -p:1.1 -f:0.3 -cc:89
```

Result (ndb info id):
```text
Title: Banana
Protein: 1.1 g
Fat: 0.3 g
Carbohydrates: 22.8 g
Calories: 89 kcal (372.37 kj)
Salt: 0 g
Last Updated: 2026.05.12 23:57:42
```

---
### Find Food Entries

```bash
ndb find id
```

---
### Update an Entry

```bash
ndb update id -t:"Canned tomatoes"
```

---
### Remove an Entry

```bash
ndb remove id
```

---
### Database Status

```bash
ndb status
```

---
### Show Help

```bash
ndb help
```

---
# Roadmap
Planned improvements may include:

- Better filtering
- Nutrition calculations
- CSV import/export
- JSON support
- Interactive terminal UI
- Unit tests
- API integration

---
# Contributing

Pull requests, bug reports, and suggestions are welcome.

If you find a bug or want to improve the project, feel free to open an issue or submit a pull request.

