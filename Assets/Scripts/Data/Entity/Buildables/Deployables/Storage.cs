using DeepSpace.Core;
using UnityEngine;

namespace DeepSpace
{
	/// <summary>
	/// Defines a Storage and it's components.
	/// </summary>
	public class Storage : Deployable
	{
		public Storage(string name, long id, Vector3D velocity, Vector3D position, Quaternion rotation)
		{
			Name = name;
			EntityId = id;

			Transform = new MyEntityTransformComponent(this, position, rotation);
			Inventory = new MyEntityInventoryComponent(this, 8, 4);
		}
	}
}