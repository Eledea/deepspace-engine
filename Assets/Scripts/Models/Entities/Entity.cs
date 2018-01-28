using UnityEngine;

/// <summary>
/// The Entity class defines an Entity in a Galaxy.
/// </summary>
public class Entity
{
	/// <summary>
	/// The Name of this Entity.
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// The SolarSystem that this Entity is currently in.
	/// </summary>
	public SolarSystem SolarSystem { get; set; }

	/// <summary>
	/// The Position of this Entity from the map center in meters on each axis.
	/// </summary>
	public Vector3 Position;

	/// <summary>
	/// The Velocity of this Entity in meters per second on each axis.
	/// </summary>
	public Vector3 Velocity { get; set;}

	/// <summary>
	/// The Rotation of this Entity as a Quaternion.
	/// </summary>
	public Quaternion Rotation { get; set; }

	/// <summary>
	/// The Angular velocity of this Entity.
	/// </summary>
	public Vector3 AngularVelocity { get; set; }

	/// <summary>
	/// Updates the Position for this Entity.
	/// </summary>
	public void UpdatePosition()
	{
		Position += (Velocity * Time.deltaTime);
	}

	/// <summary>
	/// Updates the Rotation for this Entity.
	/// </summary>
	public void UpdateRotation()
	{
		Rotation = Quaternion.Euler (AngularVelocity * Time.deltaTime + Rotation.eulerAngles);
	}
}