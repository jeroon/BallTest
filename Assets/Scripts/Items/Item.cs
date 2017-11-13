using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{

	#region "Variables"
	public enum State { Idle, Move, NeedFood }
	public State CurrentState { get; set; }

	abstract public float MaxHealth { get; }
	abstract public float MaxFood { get;  }

	public float Health { get; set; }
	public float Food { get; set; }
	public float Speed { get; set; }

	public InfoPanel InfoPanel { get; set; }
	public Item Target { get; set; }

	private List<State> stateChanges = new List<State>();
	#endregion

	#region "Unity built-in methods"
	protected virtual void Awake()
	{

	}
	protected virtual void Start()
	{
		Health = MaxHealth;
		Food = MaxFood;
	}
	protected virtual void Update()
	{
		Live();
		DetermineState();
		CurrentStateAction();
		ChangePanelPosition();
	}
	protected virtual void LateUpdate()
	{
		CurrentStateAction();
	}
	protected virtual void OnDestroy()
	{

	}
	#endregion

	public abstract void DetermineState();

	public abstract void ChangePosition();
	public abstract void Live();
	public abstract void CheckFood();
	public abstract void CheckHealth();
	public abstract void Eat();
	public abstract void TakeDamage(float ammount);

	public void ChangePanelPosition()
	{
		Vector3 screenPos = Manager.StaticManager.ActiveCamera.WorldToScreenPoint(transform.position);
		Vector3 rect = new Vector3(InfoPanel.GetComponent<RectTransform>().rect.width, InfoPanel.GetComponent<RectTransform>().rect.height, 0);
		Manager.PrintToPanel("pos", screenPos);
		Manager.PrintToPanel("rect", rect);

		InfoPanel.GetComponent<RectTransform>().position = new Vector2(screenPos.x + rect.x / 2, screenPos.y + rect.y / 2);
	}

	public void AddToStates(State state)
	{
		stateChanges.Add(state);
	}
	public void RemoveFromStates(State state)
	{
		stateChanges.Remove(state);
	}

	public bool GetClosestTarget()
	{
		Box target = null;
		float closest = float.MaxValue;

		foreach (Box _target in Manager.StaticManager.targetList)
		{
			float dist = Vector3.Distance(transform.position, _target.transform.position);
			if (dist < closest)
			{
				target = _target;
				closest = dist;
			}
		}
		if (target == null)
		{
			print("Find closest target: no target found");
			Target = null;
			return false;
		}
		else
		{
			Target = target;
			return true;
		}
	}

	public bool IsInRange()
	{
		if (Target == null)
			return false;
		float dist = Vector3.Distance(transform.position, Target.transform.position);
		if (dist < 3)
			return true;
		else
			return false;
	}

	private void CurrentStateAction()
	{
		if (stateChanges.Contains(State.NeedFood))
		{
			if (IsInRange())
			{
				Eat();
				CurrentState = State.NeedFood;
			}
			else if (GetClosestTarget())
			{
				ChangePosition();
				CurrentState = State.Move;
			}
			else
			{
				print("no target found");
				CurrentState = State.Idle;
			}
		}
		//else if()
		
		
	}
}
