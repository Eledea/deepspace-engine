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
	/// The angle in radians around it's parent Orbital that this Orbital starts at.
	/// </summary>
	public float InitAngle { get; set; }

	/// <summary>
	/// The angle in radians around it's parent Orbital that this Orbital is offset by from it's starting angle.
	/// </summary>
	public float OffsetAngle { get; set; }

	/// <summary>
	/// The Standard Gravitational Parameter of this Orbital.
	/// </summary>
	public UInt64 Mu { get; set; }

	/// <summary>
	/// The distance of this Orbital from it's parent Orbital. 
	/// </summary>
	public UInt64 OrbitalDistance { get; set; }

	/// <summary>
	/// The orbital period of this orbital in seconds.
	/// </summary>
	public UInt64 OrbitalPeriod { get; set; }

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
				Mathf.Sin (InitAngle + OffsetAngle) * (OrbitalDistance / zoomLevel),
				0,
				-Mathf.Cos (InitAngle + OffsetAngle) * (OrbitalDistance / zoomLevel)
			);

			if (Parent != null)
				Position += Parent.Position;

			return Position;
		}
	}

	/// <summary>
	/// Updates the angles for this Orbital and it's child Orbitals.
	/// </summary>
	public void UpdateAngles(UInt64 timeSinceStart)
	{
		//TODO: Reset angles every revolution to avoid floating point drift late game?

		OffsetAngle = ((float)timeSinceStart / (float)OrbitalPeriod) * 2 * Mathf.PI;

		for (int i = 0; i < Children.Count; i++)
			Children[i].UpdateAngles(timeSinceStart);
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