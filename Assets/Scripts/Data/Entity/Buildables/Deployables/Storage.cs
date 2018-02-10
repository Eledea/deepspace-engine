using DeepSpace.Core;
using UnityEngine;

namespace DeepSpace
{
	/// <summary>
	/// The Storage class defines a Storage on a Grid.
	/// </summary>
	public class Storage : Deployable
	{
		public Storage(string name, long id, Vector3D position, Quaternion rotation)
		{
			Name = name;
			EntityId = id;
			Position = position;
			Rotation = rotation;

			Inventory = new MyInventoryComponent(8, 4);
		}
	}
}