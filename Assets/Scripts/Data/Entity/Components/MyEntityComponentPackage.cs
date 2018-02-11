using System.Collections.Generic;

namespace DeepSpace
{
	/// <summary>
	/// The MyEntityComponentPacakage class stores the components of an Entity.
	/// </summary>
	public class MyEntityComponentPacakage
	{
		//TODO: Use this for Serialisation later.

		public MyEntityComponentPacakage(Entity e)
		{
			Entity = e;
		}

		public Entity Entity { get; private set; }

		private HashSet<MyEntityComponentBase> m_components;

		public void AddComponentToPackage(MyEntityComponentBase c)
		{
			if (m_components == null)
				m_components = new HashSet<MyEntityComponentBase>();

			m_components.Add(c);
		}

		public void RemoveComponentFromPackage(MyEntityComponentBase c)
		{
			if (m_components == null)
				return;

			m_components.Remove(c);
		}
	}
}