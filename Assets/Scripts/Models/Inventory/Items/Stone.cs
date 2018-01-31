namespace DeepSpace.InventorySystem
{
	/// <summary>
	/// The Stone class defines an ItemStack of Stone.
	/// </summary>
	public class Stone : ItemStack
	{
		public Stone(Inventory inv, int n)
		{
			//TODO: Serialise property assignment from encoded XML rather than hardcoding.

			Inv = inv;
			IType = IType.Stone;

			itemLimit = 50;

			//TODO: Check this before instantiating a new instance of this class.

			if (n > itemLimit)
				itemCount = itemLimit;
			else
				itemCount = n;
		}
	}
}