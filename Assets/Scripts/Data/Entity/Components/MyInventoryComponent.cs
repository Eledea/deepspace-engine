using DeepSpace.Core;

namespace DeepSpace
{
	/// <summary>
	/// The MyInventoryComponent class defines an MyInventoryComponent of an Entity.
	/// </summary>
	public class MyInventoryComponent
	{
		public MyInventoryComponent(int size_x, int size_y)
		{
			Inv = new ItemStack[size_x, size_y];
		}

		ItemStack[,] Inv;

		public int InvSize_x
		{
			get
			{
				return Inv.GetLength(0);
			}
		}

		public int InvSize_y
		{
			get
			{
				return Inv.GetLength(1);
			}
		}

		public void AddItemStackAt(ItemStack s, Vector2I index)
		{
			Inv [index.x, index.y] = s;

			s.Inv = this;
			s.InventoryIndex = new Vector2I(index.x, index.y);
		}

		public ItemStack RemoveItemStackFrom(Vector2I index)
		{
			ItemStack s = GetItemStackAt(index.x, index.y);

			if (s == null)
				return null;

			Inv [index.x, index.y] = null;

			s.Inv = null;
			s.InventoryIndex = Vector2I.zero;

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
			if (Inv[x, y] != null)
				return Inv[x, y];
			else
				return null;
		}

		public ItemStack GetItemStackAt(Vector2I index)
		{
			if (Inv[index.x, index.y] != null)
				return Inv[index.x, index.y];
			else
				return null;
		}
	}
}