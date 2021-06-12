using UnityEngine;
using UnityEngine.SceneManagement;

public class Startup : MonoBehaviour {
	private const string rootScene = "Root";
	private const string firstLevel = "Level01";

    void Start() {
		if(SceneManager.sceneCount == 1) {
			// This is the normal game startup path. Only the root scene is
			// loaded, so start loading the first level.
			StartLoad(firstLevel);
		} else if(SceneManager.GetActiveScene().name == rootScene) {
			// This is a debug path where the active scene was set to root, but
			// another scene is also loaded. Activate the other scene; if there
			// are more than one, activate one and unload the others.
			bool anyActivated = false;
			for(int i = 0; i != SceneManager.sceneCount; ++i) {
				Scene scene = SceneManager.GetSceneAt(i);
				if(scene.name != rootScene) {
					if(!anyActivated) {
						SceneManager.SetActiveScene(scene);
						anyActivated = true;
					} else {
						SceneManager.UnloadSceneAsync(scene);
					}
				}
			}
		}
	}

	public void ResetLevel() {
		StartLoad(SceneManager.GetActiveScene().name);
	}

	private void StartLoad(string level) {
		// Unload any loaded scenes other than the root.
		for(int i = 0; i != SceneManager.sceneCount; ++i) {
			Scene scene = SceneManager.GetSceneAt(i);
			if(scene.name != rootScene) {
				// A loaded scene was found. Unload it and, when that is done,
				// go back to StartLoad again.
				AsyncOperation unloadOp = SceneManager.UnloadSceneAsync(scene);
				unloadOp.completed += (_) => StartLoad(level);
				return;
			}
		}

		// Load the target scene.
		AsyncOperation loadOp = SceneManager.LoadSceneAsync(level, LoadSceneMode.Additive);
		loadOp.completed += (_) => {
			SceneManager.SetActiveScene(SceneManager.GetSceneByName(level));
		};
    }
}
