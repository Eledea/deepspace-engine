using DeepSpace.Core;

namespace DeepSpace
{
	/// <summary>
	/// Defines a Storage and it's components.
	/// </summary>
	public class Storage : Deployable, IStorage
	{
		public override void OnBuildableCreated(BuildData data)
		{
			base.OnBuildableCreated(data);

			Inventory = new MyEntityInventoryComponent(this, 8, 4);
		}

		public override void OnBuildableDestroyed()
		{
			base.OnBuildableDestroyed();
		}
	}
}