using DeepSpace.Core;
using UnityEngine;

namespace DeepSpace
{
	/// <summary>
	/// Provides definitions for a Buildable to implement.
	/// </summary>
	public interface IBuildable
	{
		void OnBuildableCreated(BuildData data);
		void OnBuildableDestroyed();
	}
}