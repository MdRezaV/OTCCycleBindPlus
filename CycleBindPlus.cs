using System;
using System.Collections.Generic;
using System.Linq;
using OpenTabletDriver.Plugin;
using OpenTabletDriver.Plugin.Attributes;
using OpenTabletDriver.Plugin.DependencyInjection;
using OpenTabletDriver.Plugin.Platform.Keyboard;
using OpenTabletDriver.Plugin.Tablet;

namespace CycleBindPlus
{
    [PluginName("Cycle Bind Plus")]
    public class CycleBindPlus : IStateBinding, IBinding
    {
        private IList<string[]> _keyCombinations = Array.Empty<string[]>();
        private string _keysString = string.Empty;
        private int _currentKeyIndex = 0;
        private int _lastPressedIndex = -1;

        [Property("Keys")]
        public string Keys
        {
            set
            {
                _keysString = value;
                _keyCombinations = ParseKeys(value);
                
                // Reset cycle state when configuration changes
                _currentKeyIndex = 0;
                _lastPressedIndex = -1;
            }
            get => _keysString;
        }

        [Resolved]
        public IVirtualKeyboard Keyboard { set; get; }

        public void Press(TabletReference tablet, IDeviceReport report)
        {
            if (_keyCombinations.Count == 0) return;

            if (_currentKeyIndex >= _keyCombinations.Count || _currentKeyIndex < 0)
                _currentKeyIndex = 0;

            _lastPressedIndex = _currentKeyIndex;
            var currentCombo = _keyCombinations[_currentKeyIndex];

            // Press all keys in the current combination
            foreach (var key in currentCombo)
            {
                Keyboard.Press(key);
            }

            Log.Debug("CycleBindPlus", $"Pressed {string.Join("+", currentCombo)}");
            _currentKeyIndex++; // Advance to next combination for the next press
        }

        public void Release(TabletReference tablet, IDeviceReport report)
        {
            if (_keyCombinations.Count == 0 || _lastPressedIndex < 0) return;

            var lastCombo = _keyCombinations[_lastPressedIndex];
            
            // Release the exact combination that was pressed
            foreach (var key in lastCombo)
            {
                Keyboard.Release(key);
            }

            Log.Debug("CycleBindPlus", $"Released {string.Join("+", lastCombo)}");
            _lastPressedIndex = -1;
        }

        private IList<string[]> ParseKeys(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return Array.Empty<string[]>();

            // Split combinations by comma, then split each combination by '+'
            var combinations = str.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            var result = new List<string[]>();

            foreach (var combo in combinations)
            {
                var keys = combo.Split('+', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                
                // Validate that every key in the combination is supported
                if (!keys.All(k => Keyboard.SupportedKeys.Contains(k)))
                {
                    throw new NotSupportedException($"The keybinding combination ({combo}) is not supported.");
                }
                
                result.Add(keys);
            }

            return result;
        }
    }
}