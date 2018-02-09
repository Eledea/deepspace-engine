using DeepSpace.World;
using System.Collections.Generic;

namespace DeepSpace.Building
{
	/// <summary>
	/// The Grid class defines a Grid in a SolarSystem.
	/// </summary>
	public class Grid : Entity
	{
		//TODO: Consider better ways to store this data.
		public List<Buildable> m_buildables;
	}
}