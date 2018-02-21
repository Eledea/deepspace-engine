using DeepSpace.Controllers;

namespace DeepSpace.Console
{
	/// <summary>
	/// Contains static methods to be used for Console interaction ingame for testing and debugging in application.
	/// </summary>
	public class Console : UnityEngine.MonoBehaviour
	{
		public static void ReadConsoleInput(string input, ConsoleOutput callback)
		{
			if (input.Length < 1)
				return;

			//UnityEngine.Debug.Log(input);
			callback.OnConsoleOutput(input);

			//TODO: Pass the input through a Command system and check for keywords.
		}
	}
}