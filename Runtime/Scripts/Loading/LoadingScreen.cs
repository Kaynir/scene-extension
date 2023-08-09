using System.Collections;
using Kaynir.SceneExtension.Tools;
using UnityEngine;

namespace Kaynir.SceneExtension.Loading
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField, Min(0)] private int sceneBuildIndex = 1; 

        public IEnumerator LoadRoutine(bool isAdditive)
        {
            if (SceneHelper.SetActive(sceneBuildIndex)) yield break;

            yield return isAdditive
            ? SceneHelper.LoadAdditive(sceneBuildIndex, true)
            : SceneHelper.LoadSingle(sceneBuildIndex);
        }

        public IEnumerator UnloadRoutine()
        {
            if (!SceneHelper.IsLoaded(sceneBuildIndex)) yield break;

            yield return SceneHelper.Unload(sceneBuildIndex);
        }
    }
}