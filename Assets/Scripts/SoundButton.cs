using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour
{
	public bool isOn;
	public Sprite onSprite;
	public Sprite offSprite;

	public void OnStart() {

	}

	public void OnButtonClicked() {
		isOn = !isOn;
		GetComponent<Image>().sprite = isOn ? onSprite : offSprite;
	}
}
