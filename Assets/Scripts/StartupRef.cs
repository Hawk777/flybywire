using UnityEngine;

public class StartupRef : MonoBehaviour {
	private Startup Instance {
		get {
			return GameObject.Find("Startup").GetComponent<Startup>();
		}
	}

	public void ResetLevel() {
		Instance.ResetLevel();
	}

	public void StartLoad(string level) {
		Instance.StartLoad(level);
	}
}
