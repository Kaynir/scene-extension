using UnityEngine;
using UnityEngine.SceneManagement;

namespace Kaynir.SceneExtension.Tools
{
    public static class SceneHelper
    {
        public static AsyncOperation LoadSingle(int buildIndex)
        {
            return SceneManager.LoadSceneAsync(buildIndex, LoadSceneMode.Single);
        }

        public static AsyncOperation LoadAdditive(int buildIndex, bool setActive = false)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(buildIndex, LoadSceneMode.Additive);
            if (setActive) operation.completed += (op) => SetActive(buildIndex);
            return operation;
        }

        public static AsyncOperation Unload(int buildIndex)
        {
            return SceneManager.UnloadSceneAsync(buildIndex);
        }

        public static Scene GetScene(int buildIndex)
        {
            return SceneManager.GetSceneByBuildIndex(buildIndex);
        }

        public static bool SetActive(int buildIndex)
        {
            if (!IsLoaded(buildIndex)) return false;
            return SceneManager.SetActiveScene(GetScene(buildIndex));
        }

        public static bool IsLoaded(int buildIndex)
        {
            return GetScene(buildIndex).isLoaded;
        }
    }
}