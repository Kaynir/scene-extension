using System.Collections.Generic;
using System.Linq;
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

        public static AsyncOperation LoadAdditive(int buildIndex)
        {
            return SceneManager.LoadSceneAsync(buildIndex, LoadSceneMode.Additive);
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

        public static List<AsyncOperation> GetLoadOperations(int[] buildIndexes)
        {
            return buildIndexes.Select((buildIndex, i) =>
            {
                AsyncOperation operation = i == 0
                ? LoadSingle(buildIndex)
                : LoadAdditive(buildIndex);

                operation.allowSceneActivation = false;
                return operation;
            }).ToList();
        }
    }
}
