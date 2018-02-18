using System;

namespace DeepSpace
{
	/// <summary>
	/// The MyEntityComponentBase class is a base class for Entity components.
	/// </summary>
	public abstract class MyEntityComponentBase
	{
		public Entity Entity { get; protected set; }

		public event Action<Entity> OnEntityComponentUpdate;

		protected virtual void UpdateComponent()
		{
			OnEntityComponentUpdate(Entity);
		}
	}
}