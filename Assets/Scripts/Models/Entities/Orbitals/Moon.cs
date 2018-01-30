using DeepSpace.Core;

public class Moon : Orbital
{
	public void GenerateMoon()
	{
		Name = "Moon";
		OrbitalAngle = Utility.RandomizedPointAngle;
		OrbitalDistance = 10000;
		Position = OrbitalPosition;
	}
}