using DeepSpace.Core;

namespace DeepSpace.Orbitals
{
	/// <summary>
	/// The Moon class defines a Moon in a SolarSystem.
	/// </summary>
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
}