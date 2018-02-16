using DeepSpace.Networking;

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

		public string Name;
		public SolarSystem SolarSystem;

		//TODO: Use this for Serialisation later.
		public MyEntityComponentPacakage Components { get; private set; }

		long m_entityId;
		public long EntityId
		{
			get { return m_entityId; }
			protected set
			{
				//Why are we trying to assign a new ID to an Entity that already has one?
				if (m_entityId != 0)
					return;

				//Assign a new ID here.
				m_entityId = value;
			}
		}

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
				m_transformComponent.OnTransformComponentUpdate += ((e) => { PlayerManager.Instance.OnEntityTransformComponentUpdate(this); });
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
				m_inventoryComponent.OnInventoryComponentUpdate += ((e) => { InventoryManager.Instance.OnEntityInventoryComponentUpdated(this); });
			}
		}
	}
}