using UnityEngine;

namespace Kaynir.SceneExtension.Tools
{
    public class SceneLoaderDebug : MonoBehaviour
    {
        private void OnEnable()
        {
            SceneLoader.LoadingStarted += OnLoadingStarted;
            SceneLoader.LoadingProgressed += OnLoadingProgressed;
            SceneLoader.LoadingCompleted += OnLoadingCompleted;
        }

        private void OnDisable()
        {
            SceneLoader.LoadingStarted -= OnLoadingStarted;
            SceneLoader.LoadingProgressed -= OnLoadingProgressed;
            SceneLoader.LoadingCompleted -= OnLoadingCompleted;
        }

        private void OnLoadingStarted()
        {
            Debug.Log("Scene loading started.");
        }

        private void OnLoadingProgressed(float progress)
        {
            Debug.Log($"Scene loading progress: {progress * 100 : 0}%.");
        }

        private void OnLoadingCompleted()
        {
            Debug.Log("Scene loading completed.");
        }
    }
}