using UnityEngine;

namespace DeepSpace.Core
{
	/// <summary>
	/// The KeyBinding struct defines a KeyBinding with a positive and negative magnitude.
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
		/// Return the current magnitude of this Axis this frame.
		/// </summary>
		public float magnitude
		{
			get
			{
				return Utility.BoolToInt (Input.GetKey (positive)) - Utility.BoolToInt (Input.GetKey (negative));
			}
		}
	}
}