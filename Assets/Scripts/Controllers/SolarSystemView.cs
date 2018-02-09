using DeepSpace.Core;
using DeepSpace.Characters;
using DeepSpace.Orbitals;
using DeepSpace.World;
using System.Collections.Generic;
using UnityEngine;

namespace DeepSpace.Controllers
{
	/// <summary>
	/// The SolarSystenView class manages the spawning, rendering and removing of GameObjects representing Orbitals
	/// and Entities for the SolarSystem that the Player is in.
	/// </summary>
	public class SolarSystemView : MonoBehaviour
	{
		public GameObject character;
		public GameObject sphere;
		public GameObject cube;

		/// <summary>
		/// The Player data class that this controller is linked to.
		/// </summary>
		public Character Character { get; set; }

		GameObject characterGO;

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
				foreach (Entity e in Character.SolarSystem.EntitiesInSystem)
					DestroyGameObjectForEntity(e);
			}

			entityToGameObject = new Dictionary<Entity, GameObject>();
			gameObjectToEntity = new Dictionary<GameObject, Entity>();

			floatingOrigin = Character.Position;

			UpdateAllEntities(50, 100);

			Debug.Log(string.Format("Loaded a new SolarSystem containing {0} Entity(s).", Character.SolarSystem.EntitiesInSystem.Length));
		}

		void LateUpdate()
		{
			if (characterGO != null)
			{
				if (characterGO.transform.position.magnitude > floatingRange)
				{
					floatingOrigin = Character.Position;
					Debug.Log("Player exceeded floating point range. Setting a new floating origin...");
				}
			}
		}

		/// <summary>
		/// Update for all the Entities in the SolarSystem this player is in.
		/// </summary>
		public void UpdateAllEntities(double _floatingRange, double _loadRange)
		{
			floatingRange = _floatingRange;
			loadRange = _loadRange;

			if (Character.SolarSystem == null)
				return;

			foreach (Entity e in Character.SolarSystem.EntitiesInSystem)
				UpdateGameObjectForEntity(e);
		}

		/// <summary>
		/// Determines if this Entity has a GameObject spawned.
		/// </summary>
		public bool GameObjectForEntity(Entity e)
		{
			return entityToGameObject.ContainsKey(e);
		}

		/// <summary>
		/// Returns an Entity from a GameObject.
		/// </summary>
		public Entity GameObjectToEntity(GameObject go)
		{
			return gameObjectToEntity[go];
		}

		/// <summary>
		/// Returns a GameObject from an Entity.
		/// </summary>
		public GameObject EntityToGameObject(Entity e)
		{
			return entityToGameObject[e];
		}

		/// <summary>
		/// Updates the GameObject for this Entity.
		/// </summary>
		public void UpdateGameObjectForEntity(Entity e)
		{
			if ((Vector3D.Distance(e.Position, Character.Position)) < loadRange)
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
			GameObject go;

			if (e.EntityId == Character.EntityId)
			{
				go = SpawnLocalCharacter((Character)e);
			}
			else if (e is Orbital)
			{
				go = Instantiate(sphere);
			}
			else if (e is Character)
			{
				go = SpawnLocalCharacter((Character)e);
			}
			else
				go = Instantiate(cube);

			go.transform.parent = this.transform;
			go.name = e.Name;

			entityToGameObject[e] = go;
			gameObjectToEntity[go] = e;
		}

		private GameObject SpawnLocalCharacter(Character c)
		{
			characterGO = Instantiate(character);
			characterGO.GetComponentInChildren<EntityController>().Player = Character;
			characterGO.GetComponentInChildren<OverlayController>().Character = Character;

			Character.entityController = characterGO.GetComponentInChildren<EntityController>();
			Character.overlayController = characterGO.GetComponentInChildren<OverlayController>();
			Character.RegisterInventoryUpdateCallback(() => { Character.overlayController.OnInventoryUpdate(); });

			return characterGO;
		}

		/// <summary>
		/// Destroys the GameObject for an Entity using a GameObject reference.
		/// </summary>
		void DestroyGameObjectForEntity(Entity e)
		{
			if (GameObjectForEntity(e) == false)
				return;

			GameObject go;

			if (e is Character)
			{
				go = characterGO;
				characterGO = null;
			}
			else
			{
				go = EntityToGameObject(e);
			}

			Destroy(go);

			gameObjectToEntity.Remove(go);
			entityToGameObject.Remove(e);
		}
	}
}