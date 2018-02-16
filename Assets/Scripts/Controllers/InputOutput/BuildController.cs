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

		float m_previewDistance = 3F;
		GameObject m_preview;

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
				m_previewDistance += Input.GetAxis("Mouse ScrollWheel") * 5F;
				m_previewDistance = Mathf.Clamp(m_previewDistance, 1F, 8F);

				Ray fromCamera = new Ray(Character.Controllers.Camera.transform.position, Character.Controllers.Camera.transform.forward);
				ShowPreviewAt(fromCamera.GetPoint(m_previewDistance));
			}
			else
			{
				Destroy(m_preview);
				m_previewDistance = 3F;
			}
		}

		private void ShowPreviewAt(Vector3 position)
		{
			if (m_preview == null)
			{
				m_preview = Instantiate(PreviewObject, position, Quaternion.identity);
				m_preview.name = "Building Preview";
			}

			m_preview.transform.position = Vector3.Lerp(m_preview.transform.position, position, Time.deltaTime * 10F);
			m_preview.GetComponent<MeshRenderer>().material = PreviewMaterial;
		}
	}
}