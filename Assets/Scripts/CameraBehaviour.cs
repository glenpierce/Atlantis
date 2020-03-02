using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraBehaviour : MonoBehaviour {

	void OnEnable()
	{
		EventManager.OnClicked += Shake;
	}


	void OnDisable()
	{
		EventManager.OnClicked -= Shake;
	}


	void Shake()
	{
		gameObject.transform.DOShakePosition (2);
	}

}
