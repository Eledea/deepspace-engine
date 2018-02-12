using DeepSpace.Core;
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
		//TODO: Serialise this.
		public GameObject character;
		public GameObject sphere;
		public GameObject cube;

		public Character Character { get; set; }

		GameObject characterGO;

		Dictionary<Entity, GameObject> entityToGameObject;
		Dictionary<GameObject, Entity> gameObjectToEntity;

		Vector3D floatingOrigin;

		double floatingRange;
		double loadRange;

		public void OnSolarSystemChange()
		{
			if (entityToGameObject != null)
			{
				foreach (Entity e in Character.SolarSystem.EntitiesInSystem)
					DestroyGameObjectForEntity(e);
			}

			entityToGameObject = new Dictionary<Entity, GameObject>();
			gameObjectToEntity = new Dictionary<GameObject, Entity>();

			floatingOrigin = Character.Transform.Position;

			UpdateAllEntities(50, 100);

			Debug.Log(string.Format("Loaded a new SolarSystem containing {0} Entity(s).", Character.SolarSystem.EntitiesInSystem.Length));
		}

		void LateUpdate()
		{
			if (characterGO != null)
			{
				if (characterGO.transform.position.magnitude > floatingRange)
				{
					floatingOrigin = Character.Transform.Position;
					Debug.Log("Player exceeded floating point range. Setting a new floating origin...");
				}
			}
		}

		public void UpdateAllEntities(double _floatingRange, double _loadRange)
		{
			floatingRange = _floatingRange;
			loadRange = _loadRange;

			if (Character.SolarSystem == null)
				return;

			foreach (Entity e in Character.SolarSystem.EntitiesInSystem)
				UpdateGameObjectForEntity(e);
		}

		public bool GameObjectForEntity(Entity e)
		{
			return entityToGameObject.ContainsKey(e);
		}

		public Entity GameObjectToEntity(GameObject go)
		{
			return gameObjectToEntity[go];
		}

		public GameObject EntityToGameObject(Entity e)
		{
			return entityToGameObject[e];
		}

		public void UpdateGameObjectForEntity(Entity e)
		{
			if ((Vector3D.Distance(e.Transform.Position, e.Transform.Position)) < loadRange)
			{
				if (GameObjectForEntity(e) == false)
					SpawnGameObjectForEntity(e);

				GameObject myGO = EntityToGameObject(e);
				myGO.transform.position = (e.Transform.Position - floatingOrigin).ToVector3();
				myGO.transform.rotation = e.Transform.Rotation;
			}
			else
			{
				if (GameObjectForEntity(e))
					DestroyGameObjectForEntity(e);
			}
		}

		void SpawnGameObjectForEntity(Entity e)
		{
			GameObject go;

			if (e.EntityId == Character.EntityId)
				go = SpawnLocalCharacter((Character)e);
			else if (e is Orbital)
				go = Instantiate(sphere);
			else if (e is Character)
				go = SpawnLocalCharacter((Character)e);
			else
				go = Instantiate(cube);

			go.transform.parent = this.transform;
			go.name = e.Name;

			entityToGameObject[e] = go;
			gameObjectToEntity[go] = e;
		}

		private GameObject SpawnLocalCharacter(Character c)
		{
			var go = Instantiate(character) as GameObject;
			c.Controllers = new InputOutput(go, c);

			return go;
		}

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