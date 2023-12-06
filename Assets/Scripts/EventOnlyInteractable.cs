using UnityEngine;

public class EventOnlyInteractable : Interactable
{

	public NPCController boss;

	protected override void Interact()
	{
		Debug.Log("interact with" + boss.name);
	}
}
