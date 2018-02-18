using UnityEngine;
using UnityEngine.Networking;

namespace DeepSpace.Networking
{
	/// <summary>
	/// Server side class that manages data based operations for the Galaxy we have loaded.
	/// </summary>
	public class GalaxyManager : NetworkBehaviour
	{
		void Start()
		{
			OnNewGalaxyCreated("Testworld");
		}

		/// <summary>
		/// The Galaxy we currently have loaded.
		/// </summary>
		public static Galaxy Galaxy { get; set; }

		/// <summary>
		/// Generates a new galaxy with filename.
		/// </summary>
		public static void OnNewGalaxyCreated(string fileName)
		{
			Galaxy = new Galaxy();

			Galaxy.GenerateGalaxy(1, 4, 3);
		}

		/// <summary>
		/// Loads a galaxy from file with filename.
		/// </summary>
		public static void OnGalaxyLoaded(string fileName)
		{
			if (Galaxy != null)
			{
				Debug.LogError("ERROR: A galaxy is loaded while on the title screen.");
				return;
			}
		}

		float m_advanceTimeTimer = 1f;

		void Update()
		{
			if (Galaxy != null)
			{
				m_advanceTimeTimer -= Time.deltaTime;

				if (m_advanceTimeTimer < 0)
				{
					Galaxy.AdvanceTime();
					m_advanceTimeTimer = 0f;
				}
			}
		}
	}
}