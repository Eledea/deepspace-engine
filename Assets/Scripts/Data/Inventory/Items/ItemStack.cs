using DeepSpace.Core;

namespace DeepSpace
{
	/// <summary>
	/// The ItemStack class defines a stack of Items.
	/// </summary>
	public class ItemStack
	{
		public MyInventoryComponent Inv;

		private int m_inv_x;
		private int m_inv_y;

		protected int m_itemCount;
		protected int m_itemLimit;

		protected IType m_type;

		/// <summary>
		/// Returns or sets the array index of this ItemStack in it's Inventory.
		/// </summary>
		public Vector2I InventoryIndex
		{
			get
			{
				return new Vector2I(m_inv_x, m_inv_y);
			}

			set
			{
				m_inv_x = value.x; m_inv_y = value.y;
			}
		}

		/// <summary>
		/// Returns the x component of the array index of this ItemStack.
		/// </summary>
		public int Index_x
		{
			get { return m_inv_x; }
		}

		/// <summary>
		/// Returns the y component of the array index of this ItemStack.
		/// </summary>
		public int Index_y
		{
			get { return m_inv_y; }
		}

		/// <summary>
		/// Returns the number of Items in this ItemStack.
		/// </summary>
		public int NumItems
		{
			get { return m_itemCount; }
		}

		/// <summary>
		/// Returns the maximum number of Items this ItemStack can hold.
		/// </summary>
		public int MaxItems
		{
			get { return m_itemLimit; }
		}

		/// <summary>
		/// Returns the IType of this ItemStack 
		/// </summary>
		public IType Type
		{
			get { return m_type; }
		}

		/// <summary>
		/// Returns the IType name of this ItemStack.
		/// </summary>
		public string TypeName
		{
			get { return m_type.ToString (); }
		}

		/// <summary>
		/// Returns the IType integer of this ItemStack.
		/// </summary>
		public int TypeId
		{
			get { return (int)m_type; }
		}

		/// <summary>
		/// Returns the number of Items that this ItemStack can add before it exceeds it reaches it's limit.
		/// </summary>
		public int ItemAddability
		{
			get { return m_itemLimit - m_itemCount; }
		}

		/// <summary>
		/// Increments the ItemCount for this ItemStack by n.
		/// </summary>
		public void AddItems(int n)
		{
			m_itemCount += n;
		}

		/// <summary>
		/// Decrements the ItemCount for this ItemStack by n.
		/// </summary>
		public void RemoveItems(int n)
		{
			m_itemCount -= n;
		}
	}
}