using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : Item
{

	#region "Variables"
	override public float MaxHealth { get { return 10; } }
	override public float MaxFood { get { return 10; } }

	#endregion

	#region "Unity built-in methods"
	protected override void Start()
	{
		base.Start();
		Speed = .1f;
	}
	protected override void Update() { base.Update(); }
	#endregion


	public override void ChangePosition()
	{
		// target is stationairy for now
		//transform.position += new Vector3(Random.Range(-Speed, Speed), 0, Random.Range(-Speed, Speed));
	}

	public override void CheckFood()
	{
	}

	public override void CheckHealth()
	{
		Manager.PrintToPanel("boxhealth", Health);
		if (Health <= 0)
		{
			Manager.StaticManager.targetList.Remove(this);
			Destroy(this.gameObject);
		}
	}
	public override void Eat()
	{

	}

	public override void TakeDamage(float ammount)
	{
		Health -= ammount;
		CheckHealth();
	}

	public override void DetermineState()
	{
	}

	public override void Live()
	{
	}
}
