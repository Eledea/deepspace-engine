using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Graphical class responsible for visually displaying changes to data in our data classing.
/// </summary>
public class SolarSystemView : MonoBehaviour
{
	public GameObject[] gameObjects;

	void Start()
	{
		gc = FindObjectOfType<GameController>();
		SetSolarSystem ();
	}

	GameController gc;
	SolarSystem mySolarSystem;

	Dictionary<Orbital, GameObject> orbitalGameObjectMap;
	Dictionary<Player, GameObject> playerGameObjectMap;

	/// <summary>
	/// Sets a new SolarSystem to display.
	/// </summary>
	public void SetSolarSystem()
	{
		while (transform.childCount > 0)
		{
			Transform child = transform.GetChild (0);
			child.SetParent (null);
			Destroy (child.gameObject);
		}

		orbitalGameObjectMap = new Dictionary<Orbital, GameObject> ();
		playerGameObjectMap = new Dictionary<Player, GameObject> ();

		mySolarSystem = gc.Galaxy.CurrentSolarSystem;

		SpawnGameObjectForOrbital (mySolarSystem, this.transform);

		for (int p = 0; p < gc.Galaxy.Players.Count; p++)
			SpawnGameObjectForPlayer(gc.Galaxy.Players [p]);
	}

	/// <summary>
	/// Spawn a GameObject for this Orbital.
	/// </summary>
	void SpawnGameObjectForOrbital(Orbital orbital, Transform parent)
	{
		GameObject go = (GameObject)Instantiate (gameObjects[0]);
		go.transform.position = orbital.Position;
		go.transform.parent = parent;

		orbitalGameObjectMap [orbital] = go;

		if (orbital.Children != null)
			for (int i = 0; i < orbital.Children.Count; i++)
				SpawnGameObjectForOrbital (orbital.Children[i], go.transform);
	}

	/// <summary>
	/// Spawns a GameObject for this player.
	/// </summary>
	void SpawnGameObjectForPlayer(Player player)
	{
		GameObject go = (GameObject)Instantiate (gameObjects [1]);
		go.transform.position = player.WorldPosition;

		playerGameObjectMap [player] = go;
		go.GetComponent<MovementController>().Player = player;
		go.GetComponentInChildren<InventoryController>().Player = player;
	}

	void Update()
	{
		if (gc.Galaxy != null)
		{
			if (mySolarSystem != null)
				UpdateGameObjectForOrbital (mySolarSystem);
			else
				Debug.LogError ("We have a Galaxy loaded, but no SolarSystem!");

			for (int p = 0; p < gc.Galaxy.Players.Count; p++)
				UpdateGameObjectForPlayer(gc.Galaxy.Players [p]);
		}
	}

	/// <summary>
	/// Updates the GameObject for this Orbital.
	/// </summary>
	void UpdateGameObjectForOrbital (Orbital orbital)
	{
		//TODO: Right now this function does nothing. Later we'll use it for Dynamic Rendering.

		/*GameObject go = orbitalGameObjectMap [orbital];

		if (orbital.Children != null)
			for (int i = 0; i < orbital.Children.Count; i++)
				UpdateGameObjectForOrbital (orbital.Children[i]);*/
	}

	/// <summary>
	/// Updates the GameObject for this Player.
	/// </summary>
	void UpdateGameObjectForPlayer(Player player)
	{
		//TODO: Consider calling this function with an event if our Player position changes rather than every iteration?

		GameObject go = playerGameObjectMap [player];
		go.transform.position = player.WorldPosition;
		go.transform.rotation = player.Rotation;
	}
}