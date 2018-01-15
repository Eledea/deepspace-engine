using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Independent data class for storing an Orbital.
/// </summary>
public class Orbital
{
	public Orbital()
	{
		Children = new List<Orbital> ();
	}

	/// <summary>
	/// The parent Orbital of this Orbital.
	/// </summary>
	public Orbital Parent { get; set; }

	/// <summary>
	/// The child Orbitals of this Orbital.
	/// </summary>
	public List<Orbital> Children { get; set; }

	/// <summary>
	/// The angle in radians around it's parent Orbital that this Orbital is positioned at.
	/// </summary>
	public float OrbitalAngle { get; set; }

	/// <summary>
	/// The distance of this Orbital from it's parent Orbital. 
	/// </summary>
	public UInt64 OrbitalDistance { get; set; }

	/// <summary>
	/// The zoom level to apply when returning the position of this Orbital.
	/// </summary>
	private ulong zoomLevel = 1000; //1 Unity unit = 1000m

	/// <summary>
	/// Returns the world-space position of this Orbital.
	/// </summary>
	public Vector3 Position
	{
		get
		{
			Vector3 Position = new Vector3 (
				Mathf.Sin (OrbitalAngle) * (OrbitalDistance / zoomLevel),
				0,
				-Mathf.Cos (OrbitalAngle) * (OrbitalDistance / zoomLevel)
			);

			if (Parent != null)
				Position += Parent.Position;

			return Position;
		}
	}

	/// <summary>
	/// Adds an Orbital to this Orbital's list of children.
	/// </summary>
	public void AddChild(Orbital child)
	{
		child.Parent = this;
		Children.Add (child);
	}

	/// <summary>
	/// Removes an Orbital from this Orbital's list of children.
	/// </summary>
	public void RemoveChild(Orbital child)
	{
		child.Parent = null;
		Children.Remove (child);
	}
}