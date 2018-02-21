using UnityEngine;

namespace DeepSpace.Core
{
	/// <summary>
	/// Defines a KeyBinding with a positive and negative magnitude.
	/// </summary>
	public struct AxisBinding
	{
		//Constructors
		public AxisBinding(KeyCode Positive, KeyCode Negative)
		{
			m_positive = Positive;
			m_negative = Negative;
		}

		//Fields
		KeyCode m_positive;
		KeyCode m_negative;

		//Properties
		public float Magnitude
		{
			get
			{
				return Utility.BoolToInt(Input.GetKey(m_positive)) - Utility.BoolToInt(Input.GetKey(m_negative));
			}
		}

		//Methods
		public void UpdateKeyBinding(KeyCode Positive, KeyCode Negative)
		{
			m_positive = Positive; m_negative = Negative;
		}
	}
}