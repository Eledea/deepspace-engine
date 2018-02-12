using DeepSpace.Core;
using System;

namespace DeepSpace
{
	/// <summary>
	/// The MyEntityInventoryComponent class defines an Inventory of an Entity.
	/// </summary>
	public class MyEntityInventoryComponent : MyEntityComponentBase
	{
		public Entity Entity { get; private set; }

		public MyEntityInventoryComponent(Entity e, int size_x, int size_y)
		{
			Entity = e;
			Inventory = new ItemStack[size_x, size_y];
		}

		private ItemStack[,] Inventory;
		public event Action<Entity> OnInventoryComponentUpdate;

		public int InvSize_x
		{
			get { return Inventory.GetLength(0); }
		}

		public int InvSize_y
		{
			get { return Inventory.GetLength(1); }
		}

		public void AddItemStackAt(ItemStack s, Vector2I index)
		{
			Inventory [index.x, index.y] = s;

			s.Inventory = this;
			s.InventoryIndex = new Vector2I(index.x, index.y);

			OnInventoryComponentUpdate(Entity);
		}

		public ItemStack RemoveItemStackFrom(Vector2I index)
		{
			ItemStack s = GetItemStackAt(index.x, index.y);

			if (s == null)
				return null;

			Inventory [index.x, index.y] = null;

			s.Inventory = null;
			s.InventoryIndex = Vector2I.zero;

			OnInventoryComponentUpdate(Entity);
			return s;
		}

		public bool IsItemStackAt(int x, int y)
		{
			return GetItemStackAt(x, y) != null;
		}

		public bool IsItemStackAt(Vector2I index)
		{
			return GetItemStackAt(index.x, index.y) != null;
		}

		public ItemStack GetItemStackAt(int x, int y)
		{
			if (Inventory[x, y] != null)
				return Inventory[x, y];
			else
				return null;
		}

		public ItemStack GetItemStackAt(Vector2I index)
		{
			if (Inventory[index.x, index.y] != null)
				return Inventory[index.x, index.y];
			else
				return null;
		}
	}
}