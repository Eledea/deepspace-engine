using DeepSpace.Core;
using UnityEngine;

/// <summary>
/// Sub-class of an Orbital for storing a Moon.
/// </summary>
public class Moon : Orbital
{
	public void GenerateMoon()
	{
		OrbitalAngle = Utility.RandomizedPointAngle;
		OrbitalDistance = 10000;
	}
}