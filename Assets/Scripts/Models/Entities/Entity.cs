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
	public Vector3 Position { get; set; }

	/// <summary>
	/// The Velocity of this Entity in meters per second on each axis.
	/// </summary>
	public Vector3 Velocity { get; set;}

	/// <summary>
	/// The Rotation of this Entity.
	/// </summary>
	public Quaternion Rotation { get; set; }

	/// <summary>
	/// Returns the world-space position of this Entity.
	/// </summary>
	public Vector3 WorldPosition
	{
		get
		{
			return Position / 1000f;
		}
	} 

	/// <summary>
	/// Updates the Position for this Entity.
	/// </summary>
	public void UpdatePosition()
	{
		Position += (Velocity * Time.deltaTime);
	}
}