using DeepSpace.Core;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The SolarSystenView class manages the spawning, rendering and removing of GameObjects representing Orbitals
/// and Entities for the SolarSystem that the Player is in.
/// </summary>
public class SolarSystemView : MonoBehaviour
{
	//TODO: Replace hardcoded Prefabs with asset streaming later on.

	public GameObject player;
	public GameObject sphere;
	public GameObject cube;

	public static SolarSystemView Instance;

	void OnEnable()
	{
		Instance = this;

		//TODO: Make this not hardcoded.
		loadRange = 5000;
	}

	/// <summary>
	/// The Player data class that this controller is linked to.
	/// </summary>
	public Player Player { get; set;}

	SolarSystem mySolarSystem;

	GameObject playerGO;

	Dictionary<Entity, GameObject> entityToGameObject;
	Dictionary<GameObject, Entity> gameObjectToEntity;

	Vector3D floatingOrigin;
	double loadRange;

	/// <summary>
	/// Sets a new SolarSystem.
	/// </summary>
	public void SetSolarSystem()
	{
		while (transform.childCount > 0)
		{
			Transform child = transform.GetChild (0);
			child.SetParent (null);
			Destroy (child.gameObject);
		}

		entityToGameObject = new Dictionary<Entity, GameObject>();
		gameObjectToEntity = new Dictionary<GameObject, Entity>();

		mySolarSystem = GameController.Instance.Galaxy.CurrentSolarSystem;

		floatingOrigin = Player.Position;

		Debug.Log(string.Format("There are {0} Entity(s) in this SolarSystem.", mySolarSystem.EntitiesInSystem.Length));
	}

	void Update()
	{
		//TODO: Replace with an Event System so that we are not updating every frame.

		UpdateGameObjectForPlayer ();

		foreach (Entity e in mySolarSystem.EntitiesInSystem)
			UpdateGameObjectForEntity (e);
	}

	/// <summary>
	/// Determines if this Entity has a GameObject spawned.
	/// </summary>
	public bool GameObjectForEntity(Entity e)
	{
		return entityToGameObject.ContainsKey (e);
	}

	/// <summary>
	/// Returns an Entity from a GameObject.
	/// </summary>
	public Entity GameObjectToEntity(GameObject go)
	{
		return gameObjectToEntity [go];
	}

	/// <summary>
	/// Returns a GameObject from an Entity.
	/// </summary>
	public GameObject EntityToGameObject(Entity e)
	{
		return entityToGameObject [e];
	}

	/// <summary>
	/// Updates the GameObject for this Player.
	/// </summary>
	void UpdateGameObjectForPlayer()
	{
		if (playerGO == null)
			SpawnGameObjectForPlayer ();

		playerGO.transform.position = (Player.Position - floatingOrigin).ToVector3();
		playerGO.transform.rotation = Player.Rotation;
	}

	/// <summary>
	/// Spawns the GameObject for this Player.
	/// </summary>
	void SpawnGameObjectForPlayer()
	{
		playerGO = Instantiate (player);
		playerGO.GetComponentInChildren<MovementController>().Player = Player;
		playerGO.GetComponentInChildren<InventoryController>().Player = Player;
	}

	/// <summary>
	/// Destroys the GameObject for a Player using a GameObject reference.
	/// </summary>
	void DestroyGameObjectForPlayer()
	{
		Destroy (playerGO);
		playerGO = null;
	}

	/// <summary>
	/// Updates the GameObject for this Entity.
	/// </summary>
	void UpdateGameObjectForEntity(Entity e)
	{
		//TODO: Consider whether it is more effecient to just disable the Meshes for Object or to merely remove and spawn new GameObjects.

		if (Utility.Abs((Vector3D.Distance(e.Position, Player.Position))) < loadRange)
		{
			if (GameObjectForEntity(e) == false)
				SpawnGameObjectForEntity(e);
				
			GameObject myGO = EntityToGameObject(e);
			myGO.transform.position = (e.Position - floatingOrigin).ToVector3();
		}
		else
		{
			if (GameObjectForEntity(e))
				DestroyGameObjectForEntity(e);
		}
	}

	/// <summary>
	/// Spawns a GameObject for this Entity.
	/// </summary>
	void SpawnGameObjectForEntity(Entity e)
	{
		GameObject myGO;

		if (e is Orbital)
			myGO = Instantiate(sphere);
		else
			myGO = Instantiate(cube);

		myGO.transform.parent = this.transform;

		entityToGameObject [e] = myGO;
		gameObjectToEntity [myGO] = e;
	}

	/// <summary>
	/// Destroys the GameObject for an Entity using a GameObject reference.
	/// </summary>
	void DestroyGameObjectForEntity(Entity e)
	{
		GameObject go = EntityToGameObject(e);
		Destroy(go);

		gameObjectToEntity.Remove (go);
		entityToGameObject.Remove(e);
	}
}