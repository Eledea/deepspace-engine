namespace DeepSpace.InventorySystem
{
	/// <summary>
	/// The Wood class defines an ItemStack of Wood.
	/// </summary>
	public class Wood : ItemStack
	{
		public Wood(Inventory inv, int n)
		{
			Inv = inv;
			IType = IType.Wood;

			itemLimit = 50;

			if (n > itemLimit)
				itemCount = itemLimit;
			else
				itemCount = n;
		}
	}
}