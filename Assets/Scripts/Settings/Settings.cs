using System.Collections.Generic;

namespace DeepSpace.Settings
{
	/// <summary>
	/// Contains the data for a Players's settings.
	/// </summary>
	public class Settings
	{
		private HashSet<SettingsBase> ClientSettings;

		public void AddSettingsDataToContainer(SettingsBase s)
		{
			if (ClientSettings == null)
				ClientSettings = new HashSet<SettingsBase>();

			ClientSettings.Add(s);
		}

		public void RemoveSettingsDataToContainer(SettingsBase s)
		{
			//We may be able to remove this method later down the line, however useful for debugging for now at least.

			if (ClientSettings == null)
				return;

			ClientSettings.Remove(s);
		}
	}
}