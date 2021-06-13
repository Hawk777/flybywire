using UnityEngine;
using UnityEngine.UI;

public class EndingBox : MonoBehaviour {
	public bool isOpen;

	public void OnButtonClicked() {
		isOpen = true;
		GetComponent<Button>().interactable = false;
	}
}
