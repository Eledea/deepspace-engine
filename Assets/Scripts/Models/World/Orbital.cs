using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Independent data class for storing an Orbital.
/// </summary>
public class Orbital
{
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
	/// The distance of this Orbital from it's parent Orbital in meters. 
	/// </summary>
	public UInt64 OrbitalDistance { get; set; }

	/// <summary>
	/// Returns the world-space position of this Orbital.
	/// </summary>
	public Vector3 Position
	{
		get
		{
			Vector3 Position = new Vector3 (
				(float)(Math.Sin (OrbitalAngle) * OrbitalDistance),
				0,
				(float)(-Math.Cos (OrbitalAngle) * OrbitalDistance)
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
		if (Children == null)
			Children = new List<Orbital> ();
		
		Children.Add (child);
		child.Parent = this;
	}

	/// <summary>
	/// Removes an Orbital from this Orbital's list of children.
	/// </summary>
	public void RemoveChild(Orbital child)
	{
		if (Children != null)
			Children.Remove (child);

		child.Parent = null;
	}
}