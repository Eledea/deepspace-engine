using DeepSpace.Core;
using DeepSpace.Networking;
using UnityEngine;

namespace DeepSpace
{
	/// <summary>
	/// The Entity class defines an Entity in a SolarSystem.
	/// </summary>
	public class Entity
	{
		public Entity()
		{
			Components = new MyEntityComponentPacakage(this);
		}

		public SolarSystem SolarSystem;

		public MyEntityComponentPacakage Components { get; private set; }

		public string Name;
		//TODO: Actually implement the Entity ID system.
		public long EntityId;

		MyEntityTransformComponent m_transformComponent;
		public MyEntityTransformComponent Transform
		{
			get
			{
				return m_transformComponent;
			}

			set
			{
				Components.RemoveComponentFromPackage(m_transformComponent);
				Components.AddComponentToPackage(value);

				m_transformComponent = value;
			}
		}

		MyEntityInventoryComponent m_inventoryComponent;
		public MyEntityInventoryComponent Inventory
		{
			get
			{
				return m_inventoryComponent;
			}

			set
			{
				Components.RemoveComponentFromPackage(m_inventoryComponent);
				Components.AddComponentToPackage(value);

				m_inventoryComponent = value;
			}
		}
	}
}