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
		floatingRange = 50;

		//TODO: Make this not hardcoded. eg: Make controllable from settings UI.
		loadRange = 100;
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

	double floatingRange;
	double loadRange;

	/// <summary>
	/// Sets a new SolarSystem.
	/// </summary>
	public void OnSolarSystemChange()
	{
		if (mySolarSystem != null)
			foreach (Entity e in mySolarSystem.EntitiesInSystem)
				DestroyGameObjectForEntity(e);

		entityToGameObject = new Dictionary<Entity, GameObject>();
		gameObjectToEntity = new Dictionary<GameObject, Entity>();

		mySolarSystem = GameController.Instance.Galaxy.CurrentSolarSystem;

		floatingOrigin = Player.Position;

		Debug.Log(string.Format("There are {0} Entity(s) in this SolarSystem.", mySolarSystem.EntitiesInSystem.Length));
	}

	void Update()
	{
		if (playerGO != null)
		{
			if (playerGO.transform.position.magnitude > floatingRange)
			{
				floatingOrigin = Player.Position;
				Debug.Log("Player exceeded defined distance from floating origin. Setting a new floating origin...");
			}
		}

		//TODO: Replace with an Event System so that we are not updating every frame.

		if (mySolarSystem != null)
			foreach (Entity e in mySolarSystem.EntitiesInSystem)
				UpdateGameObjectForEntity(e);
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
	/// Updates the GameObject for this Entity.
	/// </summary>
	void UpdateGameObjectForEntity(Entity e)
	{
		//TODO: Consider whether it is more efficient to just disable the Meshes for Object or to merely remove and spawn new GameObjects.

		if ((Vector3D.Distance(e.Position, Player.Position)) < loadRange)
		{
			if (GameObjectForEntity(e) == false)
				SpawnGameObjectForEntity(e);
				
			GameObject myGO = EntityToGameObject(e);
			myGO.transform.position = (e.Position - floatingOrigin).ToVector3();
			myGO.transform.rotation = Player.Rotation;
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

		//TODO: This sucks. Make it better!

		if (e is Orbital)
			myGO = Instantiate(sphere);
		else if (e is Player)
			myGO = SpawnGameObjectForPlayer();
		else
			myGO = Instantiate(cube);

		myGO.transform.parent = this.transform;

		entityToGameObject [e] = myGO;
		gameObjectToEntity [myGO] = e;

		Debug.Log(string.Format("Loading a GameObject for {0}...", e.Name));
	}

	/// <summary>
	/// Spawns the GameObject for this Player.
	/// </summary>
	GameObject SpawnGameObjectForPlayer()
	{
		playerGO = Instantiate(player);
		playerGO.GetComponentInChildren<MovementController>().Player = Player;
		playerGO.GetComponentInChildren<InventoryController>().Player = Player;

		return playerGO;
	}

	/// <summary>
	/// Destroys the GameObject for an Entity using a GameObject reference.
	/// </summary>
	void DestroyGameObjectForEntity(Entity e)
	{
		GameObject myGO;

		if (e is Player)
		{
			myGO = playerGO;
			playerGO = null;
		}
		else
		{
			myGO = EntityToGameObject(e);
		}

		Destroy(myGO);

		gameObjectToEntity.Remove (myGO);
		entityToGameObject.Remove(e);

		Debug.Log(string.Format("Unloading a GameObject for {0}...", e.Name));
	}
}