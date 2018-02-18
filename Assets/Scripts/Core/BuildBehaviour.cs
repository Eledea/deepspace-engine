namespace DeepSpace.Core
{
	public enum BuildCheckResult
	{
		OK		= 0,
		FAIL	= 1
	}

	/// <summary>
	/// Contains constants, properties and methods for managing the behaviour of Buildables.
	/// </summary>
	public sealed class BuildBehaviour
	{
		/*
		Currently, all Buildables inherit from the Buildable base class. There are 3 types of Buildables:
		Constructables, Deployables and Attachables. The rules for Buildables are as follows...

		Constructables:
		Can only be connected using attachment points on the Constructable.

		Deployables:
		Can only be built on the "top" facing surface of a Constructable.
		Can only be built with an upward facing orientation.

		Attachables:
		Can be built in any orientation on any surface of a Constructable.
		*/
	}
}