using System;
using UnityEngine;

namespace DeepSpace.Core
{
	/// <summary>
	/// Defines the Mouse button of an interaction with a Mouse.
	/// </summary>
	public enum MouseButton
	{
		Left,
		Right,
		Middle,
		Unknown,
		None
	}

	/// <summary>
	/// Defines a Drag with the mouse.
	/// </summary>
	public struct MouseDrag
	{
		//Constructors
		public MouseDrag(Vector2 start, Vector2 end, MouseButton button) { m_startIndex = start; m_endIndex = end; m_button = button; }
		public MouseDrag(int start_x, int start_y, int end_x, int end_y, MouseButton button) { m_startIndex = new Vector2(start_x, start_y); m_endIndex = new Vector2(end_x, end_y); m_button = button; }

		//Fields
		Vector2 m_startIndex;
		Vector2 m_endIndex;
		MouseButton m_button;

		//Properties
		public Vector2 Start
		{
			get { return m_startIndex; }
		}

		public Vector2 End
		{
			get { return m_endIndex; }
		}

		internal MouseButton Button
		{
			get { return m_button; }
		}
	}
}