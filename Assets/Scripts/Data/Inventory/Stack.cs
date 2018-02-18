using DeepSpace.Core;

namespace DeepSpace
{
	/// <summary>
	/// Defines a stack of Items.
	/// </summary>
	public class Stack
	{
		public MyItemDefinitionId DefinitionId { get; private set; }

		public MyEntityInventoryComponent Inventory { get; set; }

		public Stack(MyItemDefinitionId id, int n)
		{
			DefinitionId = id;
			m_itemCount = n;
		}

		private int m_inv_x;
		private int m_inv_y;
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

		protected int m_itemCount;
		public int NumItems
		{
			get
			{
				return m_itemCount;
			}
		}
		public int MaxItems
		{
			get
			{
				return DefinitionId.StackLimit;
			}
		}

		public int ItemAddability
		{
			get
			{
				return DefinitionId.StackLimit - m_itemCount;
			}
		}

		public void AddItems(int n)
		{
			if (n > DefinitionId.StackLimit - m_itemCount)
				return;

			m_itemCount += n;
		}

		public void RemoveItems(int n)
		{
			if (n > m_itemCount)
				return;

			m_itemCount -= n;
		}
	}
}