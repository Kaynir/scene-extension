using Kaynir.SceneExtension.Tools;
using UnityEngine;

namespace Kaynir.SceneExtension.UI
{
    public class UI_LoadingScreen : MonoBehaviour
    {
        [SerializeField] private UI_ProgressBar progressBar = null;

        private void OnEnable()
        {
            OnLoadingProgressed(SceneConsts.MIN_PROGRESS);
            SceneLoader.LoadingProgressed += OnLoadingProgressed;
        }

        private void OnDisable()
        {
            SceneLoader.LoadingProgressed -= OnLoadingProgressed;
        }

        private void OnLoadingProgressed(float progress)
        {
            progressBar.SetProgress(progress);
        }
    }
}