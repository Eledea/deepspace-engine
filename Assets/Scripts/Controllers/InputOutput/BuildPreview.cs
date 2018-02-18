using DeepSpace.Core;
using System;
using UnityEngine;

namespace DeepSpace.Controllers
{
	public class BuildPreview : MonoBehaviour
	{
		public event Action<BuildCheckResult> OnBuildCheckResultChanged;

		BuildCheckResult m_result;
		private BuildCheckResult Result
		{
			set
			{
				if (value == m_result)
					return;

				m_result = value;
				OnBuildCheckResultChanged(m_result);
			}
		}

		public void OnTriggerStay(Collider other)
		{
			Result = BuildCheckResult.FAIL;
		}

		public void OnTriggerExit(Collider other)
		{
			Result = BuildCheckResult.OK;
		}
	}
}