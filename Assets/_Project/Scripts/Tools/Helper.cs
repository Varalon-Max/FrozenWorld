using UnityEditor;
using UnityEngine;

namespace _Project.Scripts.Tools
{
    public static class Helper
    {
        public static void QuitApplication()
        {
# if (UNITY_EDITOR)
            EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }
    }
}