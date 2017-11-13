using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : Item
{
	#region "Variables"

	override public float MaxHealth { get { return 10; } }
	override public float MaxFood { get { return 10; } }
	private float EatAmmount = .01f;
	private float HealAmmount = .05f;
	private float ViewDistance = 30;
	private bool eating;
	
	#endregion

	#region "Unity built-in methods"
	protected override void Start()
	{
		base.Start();
		
	}
	protected override void Update()
	{
		base.Update();

		InfoPanel.SetTexts(Health, MaxHealth, Food, MaxFood, CurrentState);
	}
	protected override void OnDestroy()
	{
		base.OnDestroy();

	}
	#endregion


	public override void Live()
	{

			Food -= .01f;
		if (Food <= 0)
		{
			Food = 0;
			Health -= HealAmmount;
		}
	}

	public override void DetermineState()
	{
		CheckFood();
		CheckHealth();
	}

	public override void ChangePosition()
	{
		Vector3 targetPos = Target.transform.position;
		float step = .3f * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, targetPos, step);
		AddToStates(State.NeedFood);
	}

	public override void CheckFood()
	{
		if (Food <= MaxFood * 80 / 100 && !eating)
		{
			AddToStates(State.NeedFood);
		}
	}

	public override void CheckHealth()
	{
		if (Health <= 0)
		{
			Destroy(InfoPanel.gameObject);
			Destroy(this.gameObject);
		}
	}

	public override void Eat()
	{
		Target.TakeDamage(EatAmmount);
		Food += EatAmmount;
		eating = true;

		if (Food >= MaxFood)
		{
			Food = MaxFood;
			if (Health == MaxHealth)
			{
				AddToStates(State.Idle);
				RemoveFromStates(State.NeedFood);
				eating = false;
			}
			else
			{
				Health += HealAmmount;
			}
		}
	}

	public override void TakeDamage(float ammount)
	{
		Health -= ammount;
		CheckHealth();
	}
}
