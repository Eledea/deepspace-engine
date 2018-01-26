﻿using DeepSpace.Utility;
using UnityEngine;

/// <summary>
/// Sub-class of an Orbital for storing a Moon.
/// </summary>
public class Moon : Orbital
{
	public void GenerateMoon()
	{
		OrbitalAngle = Utility.RandomizePointAngle;
		OrbitalDistance = 10000;
	}
}