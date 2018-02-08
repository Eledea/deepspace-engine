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
		public MouseDrag(Vector2I start, Vector2I end, MouseButton button) { m_startIndex = start; m_endIndex = end; m_button = button; }
		public MouseDrag(int start_x, int start_y, int end_x, int end_y, MouseButton button) { m_startIndex = new Vector2I(start_x, start_y); m_endIndex = new Vector2I(end_x, end_y); m_button = button; }

		//Fields
		Vector2I m_startIndex;
		Vector2I m_endIndex;
		MouseButton m_button;

		//Properties
		public Vector2I Start
		{
			get { return m_startIndex; }
		}

		public Vector2I End
		{
			get { return m_endIndex; }
		}

		internal MouseButton Button
		{
			get { return m_button; }
		}
	}
}