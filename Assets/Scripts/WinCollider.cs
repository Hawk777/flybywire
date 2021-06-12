using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCollider : MonoBehaviour {
	private void Start() {
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		Debug.Log("Win");
	}
}
