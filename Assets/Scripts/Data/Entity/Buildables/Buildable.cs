using DeepSpace.Core;

namespace DeepSpace
{
	/// <summary>
	/// The Buildable class defines a Buildable in a SolarSystem.
	/// </summary>
	public abstract class Buildable : Entity, IBuildable
	{
		public virtual void OnBuildableCreated(BuildData data)
		{
			DefinitionId = data.Definition; Name = data.Definition.Name; EntityId = data.Id;
			Transform = new MyEntityTransformComponent(this, data.Position, data.Orientation);
		}

		public virtual void OnBuildableDestroyed()
		{
		}
	}
}