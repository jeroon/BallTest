using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour
{

	#region "Variables"
	[SerializeField] private Text healthText = null;
	[SerializeField] private Text foodText = null;
	[SerializeField] private Text currentStateText= null;

	#endregion

	#region "Unity built-in methods"
	void Start() {  }
	void Update() { }
	#endregion

	public void SetTexts(float health, float maxHealth, float food, float maxFood, Item.State currentState)
	{
		healthText.text = System.Math.Round(health, 2) + "/ " + maxHealth;
		foodText.text = System.Math.Round(food, 2) + "/ " + maxFood;
		currentStateText.text = currentState.ToString();
	}

}
