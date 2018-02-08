﻿using UnityEngine;

namespace DeepSpace.InventorySystem
{
	/// <summary>
	/// The Stone class defines an ItemStack of Stone.
	/// </summary>
	public class Stone : ItemStack
	{
		public Stone(int n, Inventory inv, Vector2 index)
		{
			m_type = IType.Stone;

			m_itemLimit = 50;

			if (n > m_itemLimit)
				m_itemCount = m_itemLimit;
			else
				m_itemCount = n;

			inv.AddItemStackAt(this, index);
		}
	}
}