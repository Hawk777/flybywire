using UnityEngine;

public class Quitter : MonoBehaviour {
	public void Quit() {
		Debug.Log("Quit game");
		Application.Quit();
	}
}
