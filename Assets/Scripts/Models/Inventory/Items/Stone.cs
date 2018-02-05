namespace DeepSpace.InventorySystem
{
	/// <summary>
	/// The Stone class defines an ItemStack of Stone.
	/// </summary>
	public class Stone : ItemStack
	{
		public Stone(int n, Inventory inv, int x, int y)
		{
			IType = IType.Stone;

			itemLimit = 50;

			if (n > itemLimit)
				itemCount = itemLimit;
			else
				itemCount = n;

			inv.AddItemStackAt(this, x, y);
		}
	}
}