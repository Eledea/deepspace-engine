using DeepSpace.Core;
using DeepSpace.Networking;
using System;
using UnityEngine;

namespace DeepSpace.Controllers
{
	[System.Serializable]
	public struct BuildCheckMaterialDefinition
	{
		public string Name;
		public Material Material;
	}

	/// <summary>
	/// The BuildController allows Players to use the DeepSpace Building system.
	/// </summary>
	public class BuildController : MonoBehaviour
	{
		//TODO: Needs to be serialised later.
		public GameObject PreviewObject;
		public BuildCheckMaterialDefinition[] PreviewMaterials;

		public Character Character { get; set; }
		public bool IsBuilding { get; private set; }
		public MyEntityDefinitionId? SelectedBuilable { get; set; }

		private Direction m_previewOrientation = Direction.Forward;
		private float m_previewDistance = 3F;
		private Quaternion m_previewRotation;

		GameObject m_preview;
		BuildCheckResult m_result;

		void Update()
		{
			Update_ModeController();
			Update_BuildingPreview();

			if (IsBuilding)
				Update_BuildingController();
		}

		void Update_ModeController()
		{
			if (Input.GetKeyDown(KeyCode.B))
			{
				if (Character.IsUsingInventorySystem == false)
				{
					if (IsBuilding)
						IsBuilding = false;
					else
						IsBuilding = true;
				}
			}
		}

		void Update_BuildingPreview()
		{
			if (IsBuilding)
			{
				m_previewDistance += Input.GetAxis("Mouse ScrollWheel") * 5F;
				m_previewDistance = Mathf.Clamp(m_previewDistance, 2F, 8F);

				if (Input.GetKeyDown(KeyCode.RightBracket))
				{
					m_previewOrientation = BaseDirections.GetNextDirection(m_previewOrientation);
					m_previewRotation = BaseDirections.GetRotation(m_previewOrientation);
				}
				if (Input.GetKeyDown(KeyCode.LeftBracket))
				{
					m_previewOrientation = BaseDirections.GetPrevDirection(m_previewOrientation);
					m_previewRotation = BaseDirections.GetRotation(m_previewOrientation);
				}

				Ray fromCamera = new Ray(transform.position, transform.forward);
				ShowPreviewAt(fromCamera.GetPoint(m_previewDistance), m_previewRotation);
			}
			else
			{
				Destroy(m_preview);
				m_previewDistance = 3F;
			}
		}

		private void ShowPreviewAt(Vector3 position, Quaternion orientation)
		{
			//TODO: Fix preview "jumping" on Player exceeding floating range.

			if (m_preview == null)
			{
				m_preview = Instantiate(PreviewObject, position, orientation);
				m_preview.name = "Building Preview";
				m_preview.GetComponent<Collider>().isTrigger = true;
				m_preview.GetComponent<Renderer>().material = PreviewMaterials[0].Material;
				m_preview.AddComponent<Rigidbody>().isKinematic = true;
				m_preview.layer = 9;
				m_preview.AddComponent<BuildPreview>().OnBuildCheckResultChanged += UpdateBuildCheckResult;
			}

			//Um Sam...why are you using Linear Interpolation like this? xD
			//TODO: Create a centralised struct/static class/etc. for Interpolation calculations?
			m_preview.transform.position = Vector3.Lerp(m_preview.transform.position, position, Time.deltaTime * 10F);
			m_preview.transform.rotation = Quaternion.Lerp(m_preview.transform.rotation, orientation, Time.deltaTime * 10F);
		}

		public void OnFloatingOriginChange()
		{
			//TODO: Was being lazy when I made this :P We'll need a better solution later ig. Intergrate in the Interpolation manager?

			Ray fromCamera = new Ray(transform.position, transform.forward);
			m_preview.transform.position = fromCamera.GetPoint(m_previewDistance);
		}

		private void UpdateBuildCheckResult(BuildCheckResult result)
		{
			m_result = result;

			switch (result)
			{
				case BuildCheckResult.OK:
					m_preview.GetComponent<Renderer>().material = PreviewMaterials[0].Material;
					break;
				case BuildCheckResult.FAIL:
					m_preview.GetComponent<Renderer>().material = PreviewMaterials[1].Material;
					break;
			}
		}

		void Update_BuildingController()
		{
			if (Input.GetMouseButtonDown(0))
			{
				var request = new BuildRequest(SelectedBuilable, Character.SolarSystem, Character.Player.View.FloatingOrigin + m_preview.transform.position, m_previewRotation, m_result);
				EntityManager.InstantiateBuildable(request);
			}
		}
	}
}