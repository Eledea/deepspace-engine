using DeepSpace.Core;
using UnityEngine;

/// <summary>
/// The Entity class defines an Entity in a SolarSystem.
/// </summary>
public class Entity
{
	public string Name;

	public SolarSystem SolarSystem;
	public Vector3D Position;

	public Vector3D Velocity;
	public Quaternion Rotation;

	/// <summary>
	/// Updates the Position for this Entity.
	/// </summary>
	public void UpdatePosition()
	{ 
		Position += Velocity * Time.deltaTime;
	}
}