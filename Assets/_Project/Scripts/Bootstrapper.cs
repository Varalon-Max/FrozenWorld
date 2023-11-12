using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts
{
    public class Bootstrapper : MonoBehaviour
    {
        private const string BOOTSTRAPPER_SCENE_NAME = "Bootstrapper";

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Init()
        {
#if UNITY_EDITOR
            var currentlyLoadedEditorScene = SceneManager.GetActiveScene();
#endif

            if (SceneManager.GetSceneByName(BOOTSTRAPPER_SCENE_NAME).isLoaded != true)
                SceneManager.LoadScene(BOOTSTRAPPER_SCENE_NAME);

#if UNITY_EDITOR
            if (currentlyLoadedEditorScene.IsValid())
                SceneManager.LoadSceneAsync(currentlyLoadedEditorScene.name, LoadSceneMode.Additive);
#endif
        }
    }
}