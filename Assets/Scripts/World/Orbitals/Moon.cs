using UnityEngine;

/// <summary>
/// Sub-class of an Orbital for storing a Moon.
/// </summary>
public class Moon : Orbital
{
	public void GenerateMoon()
	{
		InitAngle = Random.Range (0, Mathf.PI * 2);
		Mu = 116718;
		OrbitalDistance = (ulong)Random.Range(50000, 200000);
		OrbitalPeriod = (ulong)System.Math.Sqrt((39.4784176 * System.Math.Pow(OrbitalDistance, 3)) / Mu);
	}
}