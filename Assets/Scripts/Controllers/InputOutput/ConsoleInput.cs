using System;
using UnityEngine;
using UnityEngine.UI;

namespace DeepSpace.Controllers
{
	public class ConsoleInput : MonoBehaviour
	{
		public event Action<string, ConsoleOutput> OnConsoleInput;
		[NonSerialized] public ConsoleOutput Output;

		void OnEnable()
		{
			m_text = GetComponent<Text>();
		}

		private Text m_text;

		void Update()
		{
			if (Input.GetKeyDown(KeyCode.Return))
			{
				var input = m_text.text;
				OnConsoleInput(m_text.text, Output);
				m_text.text = string.Empty;
			}
		}
	}
}