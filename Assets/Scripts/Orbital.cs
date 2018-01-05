using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Standalone Data class for defining the parameters of an Orbital.
/// </summary>
public class Orbital
{
	public Orbital()
	{
		Children = new List<Orbital>();
	}

	/// <summary> The parent Orbital of this Orbital. </summary>
	public Orbital Parent { get; private set; }

	/// <summary> The child Orbitals of this Orbital. </summary>
	public List<Orbital> Children { get; private set; }

	/// <summary> The Standard Gravitational Parameter of this Orbital. </summary>
	public UInt64 Mu { get; set;}

	/// <summary> The angle in radians that this Orbital will start at around it's parent. </summary>
	public float InitAngle { get; set; }

	/// <summary> The angle in radians that this Orbital is offset at around it's parent. </summary>
	public float OffsetAngle { get; set; }

	/// <summary> The distance of this Orbital from it's parent Orbital. </summary>
	public UInt64 OrbitalDistance { get; set; }

	/// <summary> The orbital period of this Orbital. </summary>
	public Double OrbitalPeriod { get; set; } //Use Kepler's 3rd Law?

	/// <summary> What texture does the Orbital have on the Graphical side? </summary>
	public int GraphicID { get; set; }

	/// <summary> The zoomLevel in our SolarSystemView class. </summary>
	private ulong zoomLevel { get; set; }

	/// <summary>
	/// Returns the world-space position of this Orbital.
	/// </summary>
	public Vector3 Position
	{
		get
		{
			Vector3 offset = new Vector3 (
				Mathf.Sin (InitAngle + OffsetAngle) * (OrbitalDistance / 14959787070),
				0,
				-Mathf.Cos (InitAngle + OffsetAngle) * (OrbitalDistance / 14959787070)
			);

			if (Parent != null)
			{
				offset += Parent.Position;
			}

			return offset;
		}
	}

	/// <summary>
	/// Updates the angles of this Orbital and it's child Orbitals by timeSinceStart.
	/// </summary>
	public void Update(UInt64 timeSinceStart)
	{
		//TODO: Reset angle each revolution to save memory?

		OffsetAngle = ((float)timeSinceStart / (float)OrbitalPeriod) * 2 * Mathf.PI;

		for (int i = 0; i < Children.Count; i++)
		{
			Children[i].Update(timeSinceStart);
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

	public void UpdateZoomLevel(ulong zoomLevel)
	{
		this.zoomLevel = zoomLevel;
	}
}