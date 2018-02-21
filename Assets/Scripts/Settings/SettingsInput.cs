using DeepSpace.Core;
using System.Collections.Generic;
using UnityEngine;

namespace DeepSpace.Settings
{
	/// <summary>
	/// Serves as a Library object for storing data pertaining to Client controls and inputs.
	/// </summary>
	public class SettingsInput : SettingsBase
	{
		//TODO: Design an interface to set this all up. Remember to add it to the SettingsContainer!

		public Dictionary<Direction, AxisBinding> dirBindings = new Dictionary<Direction, AxisBinding>
		{
				{ Direction.Right, new AxisBinding(KeyCode.D, KeyCode.A) },
				{ Direction.Up, new AxisBinding(KeyCode.Space, KeyCode.LeftControl) },
				{ Direction.Forward, new AxisBinding(KeyCode.W, KeyCode.S) }
		};
		public AxisBinding worldRoll = new AxisBinding(KeyCode.Q, KeyCode.E);
		public KeyCode dampeners = KeyCode.Z;

		public KeyCode console = KeyCode.BackQuote;
		public KeyCode overlay = KeyCode.Tab;

		public KeyCode buildMode = KeyCode.B;
		public KeyCode nextOrientation = KeyCode.RightBracket;
		public KeyCode prevOrientation = KeyCode.LeftBracket;
	}
}