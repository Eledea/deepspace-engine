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
		/// Returns the x component of the array index of this ItemStack.
		/// </summary>
		public int Index_x
		{
			get
			{
				return inv_x;
			}
		}

		/// <summary>
		/// Returns the y component of the array index of this ItemStack.
		/// </summary>
		public int Index_y
		{
			get
			{
				return inv_y;
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
		/// Returns the IType of this ItemStack 
		/// </summary>
		public IType Type
		{
			get
			{
				return IType;
			}
		}

		/// <summary>
		/// Returns the IType name of this ItemStack.
		/// </summary>
		public string TypeName
		{
			get
			{
				return IType.ToString ();
			}
		}

		/// <summary>
		/// Returns the IType integer of this ItemStack.
		/// </summary>
		public int TypeId
		{
			get
			{
				return (int)IType;
			}
		}

		/// <summary>
		/// Returns the number of Items that this ItemStack can add before it exceeds it reaches it's limit.
		/// </summary>
		public int ItemAddability
		{
			get
			{
				return itemLimit - itemCount;
			}
		}

		/// <summary>
		/// Increments the ItemCount for this ItemStack by n.
		/// </summary>
		public void AddItems(int n)
		{
			itemCount += n;
		}

		/// <summary>
		/// Decrements the ItemCount for this ItemStack by n.
		/// </summary>
		public void RemoveItems(int n)
		{
			itemCount -= n;
		}
	}
}