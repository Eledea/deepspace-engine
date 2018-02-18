﻿namespace DeepSpace
{
	/// <summary>
	/// The Wood class defines an ItemStack of Wood.
	/// </summary>
	public class Wood : Stack
	{
		public Wood(int n)
		{
			m_type = IType.Wood;

			m_itemLimit = 50;

			if (n > m_itemLimit)
				m_itemCount = m_itemLimit;
			else
				m_itemCount = n;
		}
	}
}