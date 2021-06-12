using UnityEngine;
using UnityEngine.SceneManagement;

public class Startup : MonoBehaviour {
	private const string firstLevel = "Level01";

    void Start() {
		if(SceneManager.sceneCount == 1) {
			AsyncOperation op = SceneManager.LoadSceneAsync(firstLevel, LoadSceneMode.Additive);
			op.completed += LoadDone;
		}
    }

	private void LoadDone(AsyncOperation op) {
		SceneManager.SetActiveScene(SceneManager.GetSceneByName(firstLevel));
	}
}
