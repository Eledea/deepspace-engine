using UnityEngine;

namespace DeepSpace.InventorySystem
{
	/// <summary>
	/// The Inventory class defines an Inventory of an Entity.
	/// </summary>
	public class Inventory
	{
		/// <summary>
		/// Constructs a new Inventory.
		/// </summary>
		public Inventory(Entity e, int x, int y)
		{
			Inv = new ItemStack[x, y];
			m_entity = e;
		}

		ItemStack[,] Inv;

		/// <summary>
		/// Returns the length of the x axis of this Inventory.
		/// </summary>
		public int InvSize_x
		{
			get
			{
				return Inv.GetLength(0);
			}
		}

		/// <summary>
		/// Returns the length of the y axis of this Inventory.
		/// </summary>
		public int InvSize_y
		{
			get
			{
				return Inv.GetLength(1);
			}
		}

		/// <summary>
		/// Adds an ItemStack at this array position.
		/// </summary>
		public void AddItemStackAt(ItemStack s, Vector2 index)
		{
			Inv [Mathf.FloorToInt(index.x), Mathf.FloorToInt(index.y)] = s;

			s.Inv = this;
			s.InventoryIndex = new Vector2(Mathf.FloorToInt(index.x), Mathf.FloorToInt(index.y));
		}

		/// <summary>
		/// Removes an ItemStack from this array position.
		/// </summary>
		public ItemStack RemoveItemStackFrom(Vector2 index)
		{
			ItemStack s = GetItemStackAt(Mathf.FloorToInt(index.x), Mathf.FloorToInt(index.y));

			if (s == null)
				return null;

			Inv [Mathf.FloorToInt(index.x), Mathf.FloorToInt(index.y)] = null;

			s.Inv = null;
			s.InventoryIndex = Vector2.zero;

			return s;
		}

		/// <summary>
		/// Determines whether this instance contains an ItemStack at the specified array index.
		/// </summary>
		public bool IsItemStackAt(int x, int y)
		{
			return GetItemStackAt(x, y) != null;
		}

		/// <summary>
		/// Determines whether this instance contains an ItemStack at the specified array index.
		/// </summary>
		public bool IsItemStackAt(Vector2 index)
		{
			return GetItemStackAt(Mathf.FloorToInt(index.x), Mathf.FloorToInt(index.y)) != null;
		}

		/// <summary>
		/// Gets an ItemStack at this array index.
		/// </summary>
		public ItemStack GetItemStackAt(int x, int y)
		{
			if (Inv[x, y] != null)
				return Inv[x, y];
			else
				return null;
		}

		/// <summary>
		/// Gets an ItemStack at this array index.
		/// </summary>
		public ItemStack GetItemStackAt(Vector2 index)
		{
			if (Inv[Mathf.FloorToInt(index.x), Mathf.FloorToInt(index.y)] != null)
				return Inv[Mathf.FloorToInt(index.x), Mathf.FloorToInt(index.y)];
			else
				return null;
		}

		Entity m_entity;

		/// <summary>
		/// Returns the Entity that this Invenntory is attached to.
		/// </summary>
		public Entity Entity
		{
			get
			{
				return m_entity;
			}
		}
	}
}