using UnityEngine;

namespace DeepSpace.Utility
{
	public struct KeyBinding
	{
		public KeyBinding (KeyCode positive) { Positive = positive; Negative = KeyCode.None;}

		public KeyBinding (KeyCode positive, KeyCode negative) { Positive = positive; Negative = negative; }

		KeyCode Positive;
		KeyCode Negative;
			
		/// <summary>
		/// Return the current magnitude of this Axis this frame.
		/// </summary>
		public float Magnitude
		{
			get
			{
				return Utility.BoolToInt (Input.GetKey (Positive)) - Utility.BoolToInt (Input.GetKey (Negative));
			}
		}
	}
}