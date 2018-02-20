namespace DeepSpace.Console
{
	/// <summary>
	/// Contains static methods to for Console interaction ingame for testing and debugging in application.
	/// </summary>
	public class Console : UnityEngine.MonoBehaviour
	{
		public static void ReadConsoleInput(string input)
		{
			if (input == null)
				return;

			UnityEngine.Debug.Log(input);

			//Pass the input through a Command system and check for keywords.
		}
	}
}