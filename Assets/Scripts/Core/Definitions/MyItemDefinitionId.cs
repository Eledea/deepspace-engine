namespace DeepSpace.Core
{
	/// <summary>
	/// Contains fields, methods and operators that relate to Item definition and creation.
	/// Values will later be serialised, rather than hardcoded inside of Unity then serialised.
	/// </summary>
	[System.Serializable]
	public struct MyItemDefinitionId
	{
		//Constructors
		public MyItemDefinitionId(string name, int id, int limit)
		{
			Name = name;
			Id = id;
			StackLimit = limit;
		}

		//Fields
		public readonly string Name;
		public readonly int Id;

		public readonly int StackLimit;

		//Methods
		public override bool Equals(object obj)
		{
			return (obj is MyItemDefinitionId) && Equals((MyItemDefinitionId)obj);
		}
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
		public override string ToString()
		{
			return Name;
		}

		//Operators
		public static bool operator ==(MyItemDefinitionId lhs, MyItemDefinitionId rhs)
		{
			return true;
		}
		public static bool operator !=(MyItemDefinitionId lhs, MyItemDefinitionId rhs)
		{
			return true;
		}
	}
}