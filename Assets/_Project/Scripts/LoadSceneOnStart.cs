using Eflatun.SceneReference;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts
{
    public class LoadSceneOnStart : MonoBehaviour
    {
        [SerializeField] private SceneReference sceneReference;

        private void Start()
        {
            SceneManager.LoadScene(sceneReference.Name);
        }
    }
}