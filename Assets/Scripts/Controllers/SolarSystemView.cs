using DeepSpace.Core;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The SolarSystenView class manages the spawning, rendering and removing of GameObjects representing Orbitals
/// and Entities for the SolarSystem that the Player is in.
/// </summary>
public class SolarSystemView : MonoBehaviour
{
	public GameObject player;
	public GameObject sphere;
	public GameObject cube;

	void OnEnable()
	{
		floatingRange = 50;
		loadRange = 100;
	}

	/// <summary>
	/// The Player data class that this controller is linked to.
	/// </summary>
	public Player Player { get; set;}

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
		if (entityToGameObject != null)
		{
			foreach (Entity e in Player.SolarSystem.EntitiesInSystem)
				DestroyGameObjectForEntity(e);
		}

		entityToGameObject = new Dictionary<Entity, GameObject>();
		gameObjectToEntity = new Dictionary<GameObject, Entity>();

		floatingOrigin = Player.Position;

		UpdateAllEntities();

		Debug.Log(string.Format("Loaded a new SolarSystem containing {0} Entity(s)", Player.SolarSystem.EntitiesInSystem.Length));
	}

	void LateUpdate()
	{
		if (playerGO != null)
		{
			if (playerGO.transform.position.magnitude > floatingRange)
			{
				floatingOrigin = Player.Position;
				Debug.Log("Player exceeded floating point range. Setting a new floating origin...");
			}
		}
	}

	/// <summary>
	/// Update for all the Entities in the SolarSystem this player is in.
	/// </summary>
	public void UpdateAllEntities()
	{
		if (Player.SolarSystem == null)
			return;

		foreach (Entity e in Player.SolarSystem.EntitiesInSystem)
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
		if ((Vector3D.Distance(e.Position, Player.Position)) < loadRange)
		{
			if (GameObjectForEntity(e) == false)
				SpawnGameObjectForEntity(e);
				
			GameObject myGO = EntityToGameObject(e);
			myGO.transform.position = (e.Position - floatingOrigin).ToVector3();
			myGO.transform.rotation = e.Rotation;
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
		else if (e is Player)
			myGO = SpawnGameObjectForPlayer();
		else
			myGO = Instantiate(cube);

		myGO.transform.parent = this.transform;
		myGO.name = e.Name;

		entityToGameObject [e] = myGO;
		gameObjectToEntity [myGO] = e;
	}

	/// <summary>
	/// Spawns the GameObject for this Player.
	/// </summary>
	GameObject SpawnGameObjectForPlayer()
	{
		playerGO = Instantiate(player);
		playerGO.GetComponentInChildren<MovementController>().Player = Player;
		playerGO.GetComponentInChildren<InventoryController>().Player = Player;

		Player.movementController = playerGO.GetComponentInChildren<MovementController>();
		Player.inventoryController = playerGO.GetComponentInChildren<InventoryController>();

		return playerGO;
	}

	/// <summary>
	/// Destroys the GameObject for an Entity using a GameObject reference.
	/// </summary>
	void DestroyGameObjectForEntity(Entity e)
	{
		if (GameObjectForEntity(e) == false)
			return;

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
	}
}