using _Project.Scripts.Tools;
using Eflatun.SceneReference;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] private Button playButton;
        [SerializeField] private Button aboutUsButton;
        [SerializeField] private Button quitButton;
        [SerializeField] private SceneReference firstLevel;
        [SerializeField] private SceneReference aboutUsSceneReference;


        private void Awake()
        {
            playButton.onClick.AddListener(() => Loader.Load(firstLevel));
            aboutUsButton.onClick.AddListener(() => Loader.Load(aboutUsSceneReference));
            quitButton.onClick.AddListener(Helper.QuitApplication);
        }
    }
}