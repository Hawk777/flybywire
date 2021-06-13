using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingBox : MonoBehaviour
{
	public bool isOpen;
	public Sprite openSprite;

	public void OnStart() {

	}

	public void OnButtonClicked() {
		isOpen = true;
		GetComponent<Button>().interactable = false;
		GetComponent<Image>().sprite = openSprite;
	}
}
