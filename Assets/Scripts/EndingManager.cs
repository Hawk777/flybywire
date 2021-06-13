using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingManager : MonoBehaviour {
	enum State { BOXES, BOXES_DISAPPEAR, ROCKET, LAUNCH }

	public ParticleSystem starParticleSystem;

	private EndingBox[] boxes;
	private EndingRocket rocket;
	private State state = State.BOXES;

	private void Start() {
		starParticleSystem.gameObject.SetActive(false);

		boxes = GetComponentsInChildren<EndingBox>();
		rocket = GetComponentInChildren<EndingRocket>();
		rocket.gameObject.SetActive(false);
	}

	private void Update() {
		switch (state) {
			case State.BOXES:
				bool allBoxesAreOpen = true;
				foreach (EndingBox box in boxes) {
					if (!box.isOpen) {
						allBoxesAreOpen = false;
						break;
					}
				}

				if (allBoxesAreOpen) {
					state = State.BOXES_DISAPPEAR;
				}
				break;
			case State.BOXES_DISAPPEAR:
				foreach(EndingBox box in boxes) {
					box.gameObject.SetActive(false);
				}
				rocket.gameObject.SetActive(true);
				state = State.ROCKET;
				break;
			case State.ROCKET:
				if (rocket.isActive) {
					if (starParticleSystem) {
						starParticleSystem.gameObject.SetActive(true);
					}
					state = State.LAUNCH;
				}
				break;
		}
	}
}