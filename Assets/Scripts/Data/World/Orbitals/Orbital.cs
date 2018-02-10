using DeepSpace.Core;
using System;
using System.Collections.Generic;

namespace DeepSpace
{
	/// <summary>
	/// The Orbital class defines an Orbital in a SolarSystem.
	/// </summary>
	public class Orbital : Entity
	{
		Orbital m_parent;
		List<Orbital> m_children;

		protected float OrbitalAngle;
		protected UInt64 OrbitalDistance;

		/// <summary>
		/// Returns the Child Orbitals for this Orbital as an array.
		/// </summary>
		public Orbital[] ChildOrbitals
		{
			get
			{
				List<Orbital> childOrbitals = new List<Orbital>();

				if (m_children != null)
				{
					foreach (Orbital o in m_children)
					{
						childOrbitals.Add(o);
						childOrbitals.AddRange(o.ChildOrbitals);
					}
				}

				return childOrbitals.ToArray();
			}
		}

		/// <summary>
		/// Returns a calculated Position of this Orbital as a Vector3D.
		/// </summary>
		protected Vector3D OrbitalPosition
		{
			get
			{
				Vector3D OrbitalPosition = new Vector3D(
					Math.Sin(OrbitalAngle) * OrbitalDistance,
					0,
					-Math.Cos(OrbitalAngle) * OrbitalDistance
				);

				if (m_parent != null)
					OrbitalPosition += m_parent.OrbitalPosition;

				return OrbitalPosition;
			}
		}

		/// <summary>
		/// Adds an Orbital to this Orbital's list of children.
		/// </summary>
		public void AddChild(Orbital child)
		{
			if (m_children == null)
				m_children = new List<Orbital>();

			m_children.Add(child);
			child.m_parent = this;
		}

		/// <summary>
		/// Removes an Orbital from this Orbital's list of children.
		/// </summary>
		public void RemoveChild(Orbital child)
		{
			if (m_children != null)
				m_children.Remove(child);

			child.m_parent = null;
		}
	}
}