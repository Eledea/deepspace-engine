using DeepSpace.Core;
using UnityEngine;

namespace DeepSpace
{
	/// <summary>
	/// The Storage class defines a Storage on a Grid.
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