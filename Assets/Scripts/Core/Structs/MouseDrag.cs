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
		public MouseDrag(Vector2I start, Vector2I end, MouseButton button) { Start = start; End = end; Button = button; }
		public MouseDrag(int start_x, int start_y, int end_x, int end_y, MouseButton button) { Start = new Vector2I(start_x, start_y); End = new Vector2I(end_x, end_y); Button = button; }

		//Properties
		public Vector2I Start { get; private set; }
		public Vector2I End { get; private set; }
		public MouseButton Button { get; private set; }
	}
}