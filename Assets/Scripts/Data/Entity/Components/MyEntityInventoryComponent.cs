using DeepSpace.Core;

namespace DeepSpace
{
	/// <summary>
	/// The MyEntityInventoryComponent class defines an Inventory of an Entity.
	/// </summary>
	public class MyEntityInventoryComponent : MyEntityComponentBase
	{
		public MyEntityInventoryComponent(Entity e, int size_x, int size_y)
		{
			Entity = e;
			Inventory = new Stack[size_x, size_y];
		}

		private Stack[,] Inventory;

		public int InvSize_x
		{
			get { return Inventory.GetLength(0); }
		}

		public int InvSize_y
		{
			get { return Inventory.GetLength(1); }
		}

		public void AddItemStackAt(Stack s, Vector2I index)
		{
			Inventory [index.x, index.y] = s;

			s.Inventory = this;
			s.InventoryIndex = index;

			base.UpdateComponent();
		}

		public Stack RemoveItemStackFrom(Vector2I index)
		{
			Stack s = GetItemStackAt(index);

			if (s == null)
				return null;

			Inventory [index.x, index.y] = null;

			s.Inventory = null;
			s.InventoryIndex = Vector2I.zero;

			base.UpdateComponent();
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

		public Stack GetItemStackAt(int x, int y)
		{
			if (Inventory[x, y] != null)
				return Inventory[x, y];
			else
				return null;
		}

		public Stack GetItemStackAt(Vector2I index)
		{
			if (Inventory[index.x, index.y] != null)
				return Inventory[index.x, index.y];
			else
				return null;
		}
	}
}