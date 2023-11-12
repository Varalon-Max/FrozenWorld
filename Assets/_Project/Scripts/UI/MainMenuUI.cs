using System;
using _Project.Scripts.Tools;
using Eflatun.SceneReference;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI
{
    public class MainMenuUI : UI
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
            
            playButton.onClick.AddListener(InvokeOnAnyButtonClicked);
            aboutUsButton.onClick.AddListener(InvokeOnAnyButtonClicked);
            quitButton.onClick.AddListener(InvokeOnAnyButtonClicked);
        }

        private void OnDestroy()
        {
            playButton.onClick.RemoveAllListeners();
            aboutUsButton.onClick.RemoveAllListeners();
            quitButton.onClick.RemoveAllListeners();
        }
    }
}