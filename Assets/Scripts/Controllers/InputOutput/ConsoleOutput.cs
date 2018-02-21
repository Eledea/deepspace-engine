using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace DeepSpace.Controllers
{
	public class ConsoleOutput : MonoBehaviour
	{
		void OnEnable()
		{
			m_text = GetComponent<Text>();

			m_outputList = new Queue<string>();
		}

		private Text m_text;

		private Queue<string> m_outputList;

		public void OnConsoleOutput(string output)
		{
			m_text.text = AddOutputToBuilder(output).ToString();
		}

		private StringBuilder AddOutputToBuilder(string output)
		{
			//TODO: Add a scrollable window instead of removing outputs when they go out of range.

			if (m_outputList.Count >= 24)
				m_outputList.Dequeue();

			m_outputList.Enqueue(output);

			StringBuilder sb = new StringBuilder();
			foreach (string s in m_outputList)
			{
				sb.AppendLine(s);
			}

			return sb;
		}
	}
}