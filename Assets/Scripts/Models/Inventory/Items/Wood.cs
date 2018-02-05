namespace DeepSpace.InventorySystem
{
	/// <summary>
	/// The Wood class defines an ItemStack of Wood.
	/// </summary>
	public class Wood : ItemStack
	{
		public Wood(int n, Inventory inv, int x, int y)
		{
			IType = IType.Wood;

			itemLimit = 50;

			if (n > itemLimit)
				itemCount = itemLimit;
			else
				itemCount = n;

			inv.AddItemStackAt(this, x, y);
		}
	}
}