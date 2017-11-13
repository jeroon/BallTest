//using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{

	#region "Variables"
	[SerializeField] private Text logPanelTextField = null;
	[SerializeField] private Canvas canvas = null;
	[SerializeField] private Camera activeCamera = null;
	[SerializeField] private Ball ballPrefab = null;
	[SerializeField] private Box targetPrefab = null;
	[SerializeField] private InfoPanel infoPanelPrefab = null;

	public static Manager StaticManager;
	public static Text LogPanelTextField;
	private static Dictionary<string, string> PrintToPanelDictionary = new Dictionary<string, string>();

	public Camera ActiveCamera { get { return activeCamera; } }
	public Canvas Canvas { get { return canvas; } }

	public List<Box> targetList = new List<Box>();

	#endregion

	#region "Unity built-in methods"
	private void Awake()
	{
		Application.runInBackground = true;
		if (StaticManager == null) StaticManager = this;
		if (LogPanelTextField == null) LogPanelTextField = logPanelTextField;
	}
	void Start()
	{
		CreateBall();
		CreateTarget();
	}
	void Update()
	{
		if (targetList.Count == 0)
			CreateTarget();
	}
	#endregion


	public void CreateBall()
	{
		Ball ball = Instantiate(ballPrefab);
		InfoPanel infoPanel = Instantiate(infoPanelPrefab);
		infoPanel.transform.parent = canvas.transform;
		ball.InfoPanel = infoPanel;
		ball.transform.position = new Vector3(Random.Range(-20, 10), 0, Random.Range(-20, 10));
	}

	public void CreateTarget()
	{
		Box target= Instantiate(targetPrefab);
		InfoPanel infoPanel = Instantiate(infoPanelPrefab);
		infoPanel.transform.parent = canvas.transform;
		infoPanel.gameObject.SetActive(false);
		target.InfoPanel = infoPanel;
		target.transform.position = new Vector3(Random.Range(-20,10), 0, Random.Range(-20, 10));
		targetList.Add(target);
	}

	public static void PrintToPanel<T>(string name, T msg)
	{
		if (!PrintToPanelDictionary.ContainsKey(name))
			PrintToPanelDictionary.Add(name, msg.ToString());
		else
			PrintToPanelDictionary[name] = msg.ToString();

		string output = "";
		foreach (KeyValuePair<string, string> item in PrintToPanelDictionary)
		{
			output += item.Key + ": " + item.Value + System.Environment.NewLine;
		}
		LogPanelTextField.text = output;
	}
}
