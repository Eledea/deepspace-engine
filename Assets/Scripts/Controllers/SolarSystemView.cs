using DeepSpace.Core;
using DeepSpace.Networking;
using System.Collections.Generic;
using UnityEngine;

namespace DeepSpace.Controllers
{
	/// <summary>
	/// Manages the drawing, updating and destruction of Entities as GameObjects.
	/// </summary>
	public class SolarSystemView : MonoBehaviour
	{
		public Character Character { get; set; }
		private BuildController BuildController { get; set; }

		Dictionary<Entity, GameObject> m_entityToGameObject;
		Dictionary<GameObject, Entity> m_gameObjectToEntity;

		public GameObject LocalCharacter { get; private set; }

		public Vector3D FloatingOrigin { get; private set; }

		double m_floatingRange = 500;
		double m_loadRange = 10000;

		void Update()
		{
			if (LocalCharacter == null)
				return;

			if (LocalCharacter.transform.position.magnitude > m_floatingRange)
			{
				FloatingOrigin = Character.Transform.Position;
				Debug.Log("Player exceeded floating point range. Setting a new floating origin...");

				LocalCharacter.transform.position = (Character.Transform.Position - FloatingOrigin).ToVector3();
				BuildController.OnFloatingOriginChange();
				UpdateAllEntitiesForSolarSystem();
			}
		}

		public bool GameObjectForEntity(Entity e)
		{
			return m_entityToGameObject.ContainsKey(e);
		}

		public Entity GameObjectToEntity(GameObject go)
		{
			return m_gameObjectToEntity[go];
		}

		public GameObject EntityToGameObject(Entity e)
		{
			return m_entityToGameObject[e];
		}

		public void OnLocalCharacterSpawned()
		{
			while (transform.childCount > 0)
			{
				Transform child = transform.GetChild(0);
				child.SetParent(null);
				Destroy(child.gameObject);
			}

			m_entityToGameObject = new Dictionary<Entity, GameObject>();
			m_gameObjectToEntity = new Dictionary<GameObject, Entity>();

			FloatingOrigin = Character.Transform.Position;

			LocalCharacter = Instantiate(EntityManager._LocalCharacterPrefab) as GameObject;
			LocalCharacter.transform.SetParent(this.transform);
			LocalCharacter.name = Character.Name;
			SetupControllerReferencesForCharacter(LocalCharacter, Character);

			UpdateAllEntitiesForSolarSystem();
			Debug.Log(string.Format("Loaded a new SolarSystem with {0} Entity(s).", Character.SolarSystem.EntitiesInSystem.Length));
		}

		private void SetupControllerReferencesForCharacter(GameObject go, Character c)
		{
			var entityController = go.GetComponentInChildren<EntityController>();
			var overlayController = go.GetComponentInChildren<OverlayController>();
			var buildController = go.GetComponentInChildren<BuildController>();
			var camera = go.GetComponentInChildren<Camera>();

			c.OverlayController = overlayController;
			entityController.Character = c;
			entityController.OverlayController = overlayController;
			overlayController.Character = c;
			overlayController.Camera = camera;
			buildController.Character = c;
			buildController.SelectedBuilable = EntityManager._EntityDefinitions[1];
		}

		private void UpdateAllEntitiesForSolarSystem()
		{
			foreach (Entity e in Character.SolarSystem.EntitiesInSystem)
				UpdateGameObjectForEntity(e);
		}

		public void UpdateGameObjectForEntity(Entity e)
		{
			if (e.EntityId == Character.EntityId)
				return;

			if ((Vector3D.Distance(Character.Transform.Position, e.Transform.Position)) < m_loadRange)
			{
				if (GameObjectForEntity(e) == false)
					DrawGameObjectForEntity(e);

				GameObject myGO = EntityToGameObject(e);
				myGO.transform.position = (e.Transform.Position - FloatingOrigin).ToVector3();
				myGO.transform.rotation = e.Transform.Rotation;
			}
			else
			{
				if (GameObjectForEntity(e))
					DestroyGameObjectForEntity(e);
			}
		}

		private GameObject DrawGameObjectForEntity(Entity e)
		{
			var go = Instantiate(EntityManager._EntityPrefabs[e.DefinitionId.Id]) as GameObject;
			go.transform.SetParent(this.transform);
			go.name = e.Name;

			m_entityToGameObject[e] = go;
			m_gameObjectToEntity[go] = e;

			return go;
		}

		private void DestroyGameObjectForEntity(Entity e)
		{
			if (GameObjectForEntity(e) == false)
				return;

			GameObject go;

			if (e.EntityId == Character.EntityId)
			{
				go = LocalCharacter;
				LocalCharacter = null;
			}
			else
			{
				go = EntityToGameObject(e);
			}

			Destroy(go);

			m_gameObjectToEntity.Remove(go);
			m_entityToGameObject.Remove(e);
		}
	}
}