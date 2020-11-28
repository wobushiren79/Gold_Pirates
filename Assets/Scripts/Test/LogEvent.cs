using UnityEngine;
using UnityEditor;
using System.Text;
using UnityEngine.UI;

public class LogEvent : BaseMonoBehaviour
{
	public Text tvLog;
	StringBuilder logBuilder = new StringBuilder();

	void Awake()
	{
		Application.logMessageReceived += HandleLog;
	}

    private void Update()
    {
		tvLog .text = logBuilder.ToString();

	}

	void HandleLog(string condition, string stackTrace, LogType type)
	{
		if (type == LogType.Error || type == LogType.Exception)
		{
			string message = string.Format("condition = {0} \n stackTrace = {1} \n type = {2}", condition, stackTrace, type);
			SendLog(condition);
		}
	}

	void SendLog(string message)
	{
		logBuilder.Append(message+"\n");
	}

}