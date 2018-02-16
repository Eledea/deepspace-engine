using UnityEngine;

namespace DeepSpace.Core
{
	/// <summary>
	/// Defines a KeyBinding with a positive and negative magnitude.
	/// </summary>
	public struct KeyBinding
	{
		/// Constructors
		public KeyBinding (KeyCode Positive) {positive = Positive; negative = KeyCode.None;}
		public KeyBinding (KeyCode Positive, KeyCode Negative) { positive = Positive; negative = Negative; }

		/// Fields
		KeyCode positive;
		KeyCode negative;

		/// <summary>
		/// Returns true if either of the keys on this axis are down. False is not.
		/// </summary>
		public bool AxisDown
		{
			get
			{
				return (Input.GetKey(positive) || Input.GetKey(negative));
			}
		}

		/// <summary>
		/// Return the current magnitude of this Axis this frame.
		/// </summary>
		public float Magnitude
		{
			get
			{
				return Utility.BoolToInt (Input.GetKey (positive)) - Utility.BoolToInt (Input.GetKey (negative));
			}
		}
	}
}