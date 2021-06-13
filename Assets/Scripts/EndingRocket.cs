using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingRocket : MonoBehaviour
{
	public bool isActive;

	public void OnStart() {

	}

	public void OnButtonClicked() {
		isActive = true;
		GetComponent<Button>().interactable = false;
	}
}
