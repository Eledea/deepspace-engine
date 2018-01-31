using DeepSpace.Core;
using System;
using System.Collections.Generic;

/// <summary>
/// The Orbital class defines an Orbital in a SolarSystem.
/// </summary>
public class Orbital : Entity
{
	public Orbital Parent;

	public List<Orbital> Children;

	protected float OrbitalAngle;
	protected UInt64 OrbitalDistance;

	protected Vector3D OrbitalPosition
	{
		get
		{
			Vector3D OrbitalPosition = new Vector3D (
				Math.Sin (OrbitalAngle) * OrbitalDistance,
				0,
				-Math.Cos (OrbitalAngle) * OrbitalDistance
			);

			if (Parent != null)
				OrbitalPosition += Parent.OrbitalPosition;

			return OrbitalPosition;
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