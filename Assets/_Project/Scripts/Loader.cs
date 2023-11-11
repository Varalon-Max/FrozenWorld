﻿using System.Collections.Generic;
using Eflatun.SceneReference;
using MEC;
using UnityEngine.SceneManagement;

namespace _Project.Scripts
{
    public static class Loader
    {
        private static SceneReference _targetSceneReference;
        private static Scene _targetScene;

        public static void Load(SceneReference scene)
        {
            _targetSceneReference = scene;
            SceneManager.LoadScene("Loading");
            Timing.RunCoroutine(LoadTargetSceneReference());
        }
        
        public static void RestartScene()
        {
            SceneReference selfScene = SceneReference.FromScenePath(SceneManager.GetActiveScene().path);
            Load(selfScene);
        }

        private static IEnumerator<float> LoadTargetSceneReference()
        {
            yield return Timing.WaitForOneFrame;
            SceneManager.LoadScene(_targetSceneReference.Name);
        }
    }
}