using UnityEngine;

/// <summary>
/// Sub-class of an Orbital for storing a Moon.
/// </summary>
public class Moon : Orbital
{
	public void GenerateMoon()
	{
		OrbitalAngle = Random.Range (0, Mathf.PI * 2);
		OrbitalDistance = (ulong)Random.Range(50000, 200000);
	}
}