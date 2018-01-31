using UnityEngine;

namespace DeepSpace.InventorySystem
{
	/// <summary>
	/// The ItemStack class defines a stack of Items.
	/// </summary>
	public class ItemStack
	{
		public Inventory Inv;

		private int inv_x;
		private int inv_y;

		protected int itemCount;
		protected int itemLimit;

		protected IType IType;

		/// <summary>
		/// Returns or sets the array index of this ItemStack in it's Inventory.
		/// </summary>
		public Vector2 InventoryIndex
		{
			get
			{
				return new Vector2 (inv_x, inv_y);
			}

			set
			{
				inv_x = Mathf.FloorToInt (value.x); inv_y = Mathf.FloorToInt (value.y);
			}
		}

		/// <summary>
		/// Returns the number of Items in this ItemStack.
		/// </summary>
		public int NumItems
		{
			get
			{
				return itemCount;
			}
		}

		/// <summary>
		/// Returns the maximum number of Items this ItemStack can hold.
		/// </summary>
		public int MaxItems
		{
			get
			{
				return itemLimit;
			}
		}

		/// <summary>
		/// Returns the Type name of this ItemStack.
		/// </summary>
		public string ITypeName
		{
			get
			{
				return IType.ToString ();
			}
		}

		/// <summary>
		/// Returns the Type integer of this ItemStack.
		/// </summary>
		public int ITypeId
		{
			get
			{
				return (int)IType;
			}
		}

		/// <summary>
		/// Increments the ItemCount for this ItemStack.
		/// </summary>
		public void AddItem()
		{
			itemCount++;
		}

		/// <summary>
		/// Decrements the ItemCount for this ItemStack.
		/// </summary>
		public void RemoveItem()
		{
			itemCount--;
		}
	}
}