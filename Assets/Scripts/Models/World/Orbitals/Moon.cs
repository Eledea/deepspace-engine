using DeepSpace.Core;

/// <summary>
/// Sub-class of an Orbital for storing a Moon.
/// </summary>
public class Moon : Orbital
{
	public void GenerateMoon()
	{
		Name = "Moon";
		OrbitalAngle = Utility.RandomizedPointAngle;
		OrbitalDistance = 10000;
	}
}