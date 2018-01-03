using System;
using System.Collections.Generic;
using UnityEngine;

public class Orbital
{
	public Orbital()
	{
		Children = new List<Orbital>();

		InitAngle = UnityEngine.Random.Range(0, Mathf.PI*2);
		OrbitalDistance = (ulong)UnityEngine.Random.Range(5, 100);
	}

	/// <summary>
	/// The parent Orbital of this Orbital.
	/// </summary>
	public Orbital Parent;

	/// <summary>
	/// The child Orbitals of this Orbital.
	/// </summary>
	public List<Orbital> Children;

	/// <summary>
	/// The angle in radians that this Orbital will start at around it's parent.
	/// </summary>
	public float InitAngle;

	/// <summary>
	/// The distance of this Orbital from it's parent Orbital.
	/// </summary>
	public UInt64 OrbitalDistance;

	/// <summary>
	/// The orbital period of this Orbital.
	/// </summary>
	public UInt64 OrbitalPeriod; //Use Kepler's 3rd Law?

	/// <summary>
	/// What texture does the Orbital have on the Graphical side?
	/// </summary>
	public int GraphicID;

	/// <summary>
	/// Returns the world-space position of this Orbital.
	/// </summary>
	public Vector3 Position
	{
		get
		{
			Vector3 offset = new Vector3 (
				Mathf.Sin (InitAngle) * OrbitalDistance,
				0,
				-Mathf.Cos (InitAngle) * OrbitalDistance
			);

			if (Parent != null)
			{
				offset += Parent.Position;
			}

			return offset;
		}
	}

	/// <summary>
	/// Adds an Orbital as a child to this Orbital.
	/// </summary>
	public void AddChild(Orbital orbital)
	{
		orbital.Parent = this;
		Children.Add (orbital);
	}

	/// <summary>
	/// Remove an Orbital as a child from this Orbital.
	/// </summary>
	public void RemoveChild(Orbital orbital)
	{
		orbital.Parent = null;
		Children.Remove (orbital);
	}
}