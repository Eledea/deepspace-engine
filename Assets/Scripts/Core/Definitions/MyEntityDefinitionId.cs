namespace DeepSpace.Core
{
	/// <summary>
	/// Contains fields, properties and methods that relate to Entity definition and creation.
	/// </summary>
	[System.Serializable]
	public struct MyEntityDefinitionId
	{
		//Fields
		public string Name;
		public int Id;

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
			return Name;
		}

		//Operators
		public static bool operator == (MyEntityDefinitionId lhs, MyEntityDefinitionId rhs)
		{
			return lhs.Id == rhs.Id;
		}
		public static bool operator != (MyEntityDefinitionId lhs, MyEntityDefinitionId rhs)
		{
			return !(lhs == rhs);
		}
	}
}