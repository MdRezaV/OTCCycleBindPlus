# Cycle Keybind Plus Plugin for OpenTabletDriver

This project is a fork of [Ashesh3/OTDCycleBind](https://github.com/Ashesh3/OTDCycleBind). While the original project only supports cycling through single keys (e.g., `A+B+C`), this repository introduces **CycleBindPlus**, an enhanced binding mode that supports modifiers and complex key combinations.

The Cycle Bind Plus plugin for OpenTabletDriver allows users to set multiple key combinations to be pressed in a sequential cycle each time a tablet button or pen tip is pressed.

## Features

- Cycles through full key combinations sequentially.
- Supports modifiers (e.g., `Ctrl`, `Shift`, `Alt`) and multi-key presses.
- Prevents "stuck" modifier keys by ensuring the exact pressed combination is released upon button release.

## Installation

1. Download the latest release from the [Releases section](../../releases).
2. Place the downloaded `.dll` file into your OpenTabletDriver `Plugins` folder.
3. Restart OpenTabletDriver or rescan for plugins.

## Usage

After installing the plugin, select `Cycle Bind Plus` from the binding dropdown menu in your OpenTabletDriver settings.

**Format:**
* Use `,` (comma) to separate different combinations in the cycle.
* Use `+` to combine modifiers and keys *within* a single combination.

**Example:** `Ctrl+C, Ctrl+V, Shift+Insert`
* **1st press:** Presses `Ctrl` + `C`
* **2nd press:** Presses `Ctrl` + `V`
* **3rd press:** Presses `Shift` + `Insert`
* **4th press:** Presses `Ctrl` + `C` (cycles back to the start)

*Note: Ensure the key names match the supported virtual keys in OpenTabletDriver. If a key is not recognized, the plugin will throw a `NotSupportedException`.*

## Troubleshooting

If you encounter any issues while using this plugin, please check the OpenTabletDriver debug log for error messages. If the problem persists, please open an issue on this GitHub repository with a detailed description of the problem, your input string, and any relevant error messages or screenshots.

## Contributing

Contributions are welcome. If you are interested in improving the plugin, please feel free to open a pull request. All contributions are greatly appreciated.

## Credits

* **Original Project Concept:** [Ashesh3/OTDCycleBind](https://github.com/Ashesh3/OTDCycleBind)
* **Core Software:** [OpenTabletDriver](https://github.com/OpenTabletDriver/OpenTabletDriver)

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
