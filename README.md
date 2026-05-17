# <img src="SpaceHaven Save Editor/Assets/icon.ico" width="30" height="30"/> Space Haven Save Editor v2.5 (Modern Dark Bento Edition)

A powerful and modern save editor for the space tactical simulation game **[Space Haven](https://bugbyte.fi/spacehaven/)**. Developed in C# and Avalonia UI, this upgraded version brings a sleek Dark Bento interface, deep character attribute modifications, custom inventory safeguards, faction management, and more.

> [!NOTE]
> This project is a modernized, updated, and highly enhanced version built upon the original base repository: **[nuttycream/SH-Save-Editor](https://github.com/nuttycream/SH-Save-Editor)**.

---

## 🚀 Key Features

The editor supports complete, granular modification of your game saves:

### 🎭 1. Deep Character Editor
* **Identity & Role**: Easily rename characters, change their Faction, and instantly recruit prisoners/refugees by switching their status to **Crewman** (and setting their faction to Player) with a single click.
* **Survival Stats**: Edit both the **Current Value** (`v` XML attribute) and the **Max Value** (`ltv` XML attribute) for vital bodily stats (e.g., *Food, Oxygen, Energy, Health, etc.*).
* **Skills**: Modify the **Current Level** (`level` XML attribute, range 0-8) and the **Max Potential Level** (`mxn` XML attribute) for all professional skills (e.g., *Mining, Pilot, Gunner, Construction, etc.*).
* **Attributes**: Customize core physical and mental attribute points.
* **Instant "Max All" Buttons**: Smart commands to instantly maximize Stats, Skills, or Attributes of any crew member.
* **Traits Manager**: Add or remove character traits from a complete list with automatic ID lookups.
* **Conditions & Ailments (New)**: Add, edit, or remove all physical and mental conditions (e.g., *Starving, Hungry, Low Oxygen, Low Comfort, etc.*) with severity levels ranging from 0 to 5. Safely writes XML structures to ensure seamless in-game loads.
* **Clone Character**: Duplicate your favorite character to a new crew member with a unique Entity ID and customized name.

### 📦 2. Inventory & Storage Editor
* Edit item quantities in all storage facilities aboard your ships.
* **Smart Fallback Safeguard**: Automatically detects custom mod items or unrecognized resources and displays them safely as `Unknown Item (ID: {id})` to prevent application crashes.

### 🤝 3. Faction Manager
* Add bank credits to the player's account.
* Adjust relation scores and goodwill between all factions in the universe.

### 🔬 4. Research Modifier
* Edit research progress for each tech node or instantly unlock the entire tree.

---

## 📋 Prerequisites & Requirements

To run and use this editor, make sure your system meets the following:

* **Operating System**: Windows 10/11 (x64), Linux (x64), or macOS (via Wine).
* **Runtime Environment**: **[.NET 8.0 Runtime](https://dotnet.microsoft.com/download/dotnet/8.0)** (Desktop Runtime).
  * *Note: If using the Standalone/Portable release, the runtime is self-contained and no external installation is required.*
* **Game Save File**: A valid Space Haven save game (typically a file named `game` with no extension).

---

## 🛠️ How to Use

1. Download the latest release from the Releases page and extract the archive.
2. Launch the application `SpaceHaven Save Editor.exe` (or run `./SpaceHaven Save Editor` on Linux).
3. Click **File -> Open File** (or press `Ctrl + O`).
4. Navigate to your Space Haven save games folder. Default locations:
   * **Windows (Steam)**: `C:\Program Files (x86)\Steam\steamapps\common\SpaceHaven\savegames\[SAVE_NAME]\save`
   * **Linux**: `~/.config/unity3d/Bugbyte/Space Haven/savegames/[SAVE_NAME]/save`
5. Select the file named `game`.
6. Make your edits using the modern Bento Grid UI.
7. Click **File -> Save File** (or press `Ctrl + S`) to save your changes. Launch Space Haven, load your save, and enjoy!

---

## 💻 Developer & Build Instructions

If you want to build or customize the application from source:

1. Install **.NET 8.0 SDK** on your machine.
2. Clone this repository:
   ```bash
   git clone https://github.com/nuttycream/SH-Save-Editor.git
   ```
3. Navigate to the project folder:
   ```bash
   cd "SH-Save-Editor-2.5/SpaceHaven Save Editor"
   ```
4. Restore dependencies and compile:
   ```bash
   dotnet build
   ```
5. Run the application:
   ```bash
   dotnet run
   ```

---

## 📄 License

This project is open-source and licensed under the **[MIT License](LICENSE)**.

---
*Built on Avalonia UI v11 and .NET 8.*
