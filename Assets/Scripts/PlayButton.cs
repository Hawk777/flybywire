using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour {
	public void OnButtonClicked() {
		GetComponent<Button>().targetGraphic.GetComponent<Image>().sprite = GetComponent<Button>().spriteState.pressedSprite;
	}
}
