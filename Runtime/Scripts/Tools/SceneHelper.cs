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

        public static int GetActiveSceneBuildIndex()
        {
            return SceneManager.GetActiveScene().buildIndex;
        }

        public static void CreateSceneOperations(IEnumerable<int> sceneBuildIndexes, List<AsyncOperation> operations)
        {
            operations.Clear();

            for (int i = 0; i < sceneBuildIndexes.Count(); i++)
            {
                operations.Add(i == 0
                    ? LoadSingle(sceneBuildIndexes.ElementAt(i))
                    : LoadAdditive(sceneBuildIndexes.ElementAt(i))
                );

                operations[i].allowSceneActivation = false;
            }
        }
    }
}
