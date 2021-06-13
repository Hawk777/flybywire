using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Startup : MonoBehaviour {
	private const string rootScene = "Root";
	private const string firstLevel = "Title";
	private GameObject rootCamera;
	private bool loadInProgress = false;

    void Start() {
		rootCamera = GameObject.Find("Root Camera");
		if(SceneManager.sceneCount == 1) {
			// This is the normal game startup path. Only the root scene is
			// loaded, so start loading the first level.
			StartLoad(firstLevel);
		} else {
			// Another scene is loaded in addition to the root scene.
			// Deactivate the root camera and let the other scene’s camera take
			// over.
			rootCamera.SetActive(false);
			if(SceneManager.GetActiveScene().name == rootScene) {
				// This is a debug path where the active scene was set to root,
				// but another scene is also loaded. Activate the other scene;
				// if there are more than one, activate one and unload the
				// others.
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
	}

	public void ResetLevel() {
		StartLoad(SceneManager.GetActiveScene().name);
	}

	public void StartLoad(string level) {
		if(!loadInProgress) {
			loadInProgress = true;
			StartCoroutine(Load(level));
		}
	}

	private IEnumerator Load(string level) {
		// Unload any loaded scenes other than the root.
		for(int i = SceneManager.sceneCount - 1; i >= 0; --i) {
			Scene scene = SceneManager.GetSceneAt(i);
			if(scene.name != rootScene) {
				AsyncOperation unloadOp = SceneManager.UnloadSceneAsync(scene);
				yield return unloadOp;
			}
		}

		// Enable the root camera, because the scene camera has just been
		// unloaded.
		rootCamera.SetActive(true);

		// Load the new scene.
		AsyncOperation loadOp = SceneManager.LoadSceneAsync(level, LoadSceneMode.Additive);
		yield return loadOp;

		// Set the new scene as the active scene.
		SceneManager.SetActiveScene(SceneManager.GetSceneByName(level));

		// Disable the root camera because the new scene has its own.
		rootCamera.SetActive(false);

		// Done loading!
		loadInProgress = false;
		Time.timeScale = 1f;
    }
}
