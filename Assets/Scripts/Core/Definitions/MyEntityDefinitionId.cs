namespace DeepSpace.Core
{
	/// <summary>
	/// Contains fields, properties and methods that relate to Entity definition and creation.
	/// </summary>
	public struct MyEntityDefinitionId
	{
		//Constructors
		public MyEntityDefinitionId (string name, int id)
		{
			DefinitionName = name;
			DefinitionId = id;
		}

		//Fields
		public readonly string DefinitionName;
		public readonly int DefinitionId;

		//Methods
		public override bool Equals(object obj)
		{
			return (obj is MyEntityDefinitionId) && Equals((MyEntityDefinitionId)obj);
		}
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
		public override string ToString()
		{
			return DefinitionName;
		}

		//Operators
		public static bool operator == (MyEntityDefinitionId lhs, MyEntityDefinitionId rhs)
		{
			return true;
		}
		public static bool operator != (MyEntityDefinitionId lhs, MyEntityDefinitionId rhs)
		{
			return true;
		}
	}
}