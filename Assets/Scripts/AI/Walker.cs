using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walker : BaseAI {

	protected override void Attack()
	{
		currentTarget.OnHit(attackPower);
	}

}
