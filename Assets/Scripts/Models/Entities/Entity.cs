using DeepSpace.Core;
using System;
using UnityEngine;

/// <summary>
/// The Entity class defines an Entity in a Galaxy.
/// </summary>
public class Entity
{
	string Name;

	/// <summary>
	/// Returns or sets the DisplayName of this Entity.
	/// </summary>
	/// <value>The display name.</value>
	public string DisplayName
	{
		get
		{
			return Name;
		}

		set
		{
			Name = value;
		}
	}

	/// <summary>
	/// The SolarSystem that this Entity is currently in.
	/// </summary>
	public SolarSystem SolarSystem { get; set; }

	/// <summary>
	/// The Position of this Entity from the map center in meters on each axis.
	/// </summary>
	public Vector3D Position { get; set; }

	/// <summary>
	/// The Velocity of this Entity in meters per second on each axis.
	/// </summary>
	public Vector3D Velocity { get; set;}

	/// <summary>
	/// The Rotation of this Entity as a Quaternion.
	/// </summary>
	public Quaternion Rotation { get; set; }

	/// <summary>
	/// Updates the Position for this Entity.
	/// </summary>
	public void UpdatePosition()
	{ 
		Position += Velocity * Time.deltaTime;
	}
}