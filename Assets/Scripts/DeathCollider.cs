using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCollider : MonoBehaviour {
	private void Start() {
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		Debug.Log("Death");
	}
}
