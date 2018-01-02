using System;
using System.Collections.Generic;
using UnityEngine;

public class Orbital
{
	public Orbital()
	{
		TimeToOrbit = 1;
	}

	/// <summary>
	/// The parent Orbital of this Orbital.
	/// </summary>
	public Orbital Parent;

	public List<Orbital> Children;

	/// <summary>
	/// The angle of in radians of our orbit around our parent Orbital.
	/// </summary>
	public float Angle;

	/// <summary>
	/// The distance in meters from our parent Orbital.
	/// </summary>
	public UInt64 OrbitalDistance;

	/// <summary>
	/// The time this Orbital takes to orbit our parent Orbital.
	/// </summary>
	public UInt64 TimeToOrbit;

	/// <summary>
	/// What Graphic do we represent ingame?
	/// </summary>
	public int GraphicID;

	/// <summary>
	/// Returns the world-space position of this Orbital.
	/// </summary>
	public Vector3 Position
	{
		get
		{
			//TODO: Convert our orbit into a Vector3 that we can use 
			//to render something as a Unity GameObject.
			return new Vector3(
				Mathf.Sin(Angle),
				Mathf.Cos(Angle),
				0 //hardcoded to zero.
			);
		}
	}

	public void Update(UInt64 timeSinceStart)
	{
		//Advance our angle by the current amount of time.

		Angle = (timeSinceStart / TimeToOrbit) * 2 * Mathf.PI;

		//Update for all our children.
		for (int i = 0; i < Children.Count; i++)
		{
			Children [i].Update (timeSinceStart);
		}
	}

	public void MakePlanet()
	{
		Angle = 0; //"North" of our parent.
		OrbitalDistance = 150000000000; //150 million Km.

		TimeToOrbit = 365 * 24 * 60 * 60;
	}

	public void AddChild(Orbital c)
	{
		c.Parent = this;
		Children.Add (c);
	}

	public void RemoveChild(Orbital c)
	{
		c.Parent = null;
		Children.Remove (c);
	}
}