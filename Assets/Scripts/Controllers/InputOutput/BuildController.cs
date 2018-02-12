using DeepSpace.Core;
using UnityEngine;

namespace DeepSpace.Controllers
{
	/// <summary>
	/// The BuildController allows Players to use the DeepSpace Building system.
	/// </summary>
	public class BuildController : MonoBehaviour
	{
		//TODO: Needs to be serialised later.
		public GameObject PreviewObject;
		public Material PreviewMaterial;

		public Character Character { get; set; }
		public bool IsBuilding { get; private set; }

		private GameObject m_preview;

		void Update()
		{
			Update_ModeController();
			Update_BuildingPreview();
		}

		void Update_ModeController()
		{
			if (Input.GetKeyDown(KeyCode.B))
			{
				if (IsBuilding)
					IsBuilding = false;
				else
					IsBuilding = true;
			}
		}

		void Update_BuildingPreview()
		{
			if (IsBuilding)
			{
				Ray fromCamera = new Ray(Character.Controllers.Camera.transform.position, Character.Controllers.Camera.transform.forward);
				ShowPreviewAt(fromCamera.GetPoint(5));
			}
			else
			{
				Destroy(m_preview);
			}
		}

		private void ShowPreviewAt(Vector3 position)
		{
			if (m_preview == null)
			{
				m_preview = Instantiate(PreviewObject);
				m_preview.name = "Building Preview";
			}

			m_preview.transform.position = position;
			m_preview.GetComponent<MeshRenderer>().material = PreviewMaterial;
		}
	}
}