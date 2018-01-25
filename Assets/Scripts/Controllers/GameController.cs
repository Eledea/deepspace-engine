using UnityEngine;

/// <summary>
/// The central core of the game. Responsible for managing server side operations that
/// are not done within individual data classes.
/// </summary>
public class GameController : MonoBehaviour
{
	public static GameController Instance { get; protected set; }

	void OnEnable()
	{
		Instance = this;

		NewGalaxy ("Testworld");
	}

	/// <summary>
	/// The Galaxy we currently have loaded.
	/// </summary>
	public Galaxy Galaxy { get; set; }

	/// <summary>
	/// Generates a new galaxy with filename.
	/// </summary>
	public void NewGalaxy(string fileName)
	{
		//TODO: Move these functions to a seperate class for managing the world.

		this.Galaxy = new Galaxy ();

		Galaxy.GenerateGalaxy (1, 4, 3);
	}

	/// <summary>
	/// Loads a galaxy from file with filename.
	/// </summary>
	public void LoadGalaxy(string fileName)
	{
		if (Galaxy != null)
		{
			//Uh-Oh! We shouldn't have a Galaxy loaded if we are on our title screen!
			Debug.LogError("ERROR: A galaxy is loaded while on the title screen.");
			return;
		}
	}

	void Update()
	{
		if (Galaxy != null)
			Galaxy.UpdateGalaxy();
	}
}