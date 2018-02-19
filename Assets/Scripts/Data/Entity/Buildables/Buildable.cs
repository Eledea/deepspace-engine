using DeepSpace.Core;
using UnityEngine;

namespace DeepSpace
{
	/// <summary>
	/// The Buildable class defines a Buildable in a SolarSystem.
	/// </summary>
	public abstract class Buildable : Entity, IBuildable
	{
		public virtual void OnBuildableCreated(BuildData data)
		{
			Name = data.Name; EntityId = data.Id;

			//All Buildables require a Transform component.
			Transform = new MyEntityTransformComponent(this, data.Position, data.Orientation);
		}

		public virtual void OnBuildableDestroyed()
		{
		}
	}
}