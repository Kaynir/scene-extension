using Kaynir.SceneExtension.Loaders;
using Kaynir.SceneExtension.Transitions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Kaynir.SceneExtension.Examples
{
    public class ExampleMainMenu : MonoBehaviour
    {
        [SerializeField]
        private TransitionSequence reloadTransition;

        [SerializeField]
        private Button reloadMenuButton;

        [SerializeField]
        private Button loadSceneButton;

        [SerializeField]
        private TMP_InputField loadSceneInputField;

        private ISceneLoader _sceneLoader;
        private ITransition _defaultTransition;

        private void Awake()
        {
            ExampleServiceManager.TryGetService(out _sceneLoader);
            ExampleServiceManager.TryGetService(out _defaultTransition);

            reloadMenuButton.onClick.AddListener(OnReloadButtonClicked);
            loadSceneButton.onClick.AddListener(OnLoadSceneButtonClicked);
        }

        private void OnDestroy()
        {
            reloadMenuButton.onClick.RemoveListener(OnReloadButtonClicked);
            loadSceneButton.onClick.RemoveListener(OnLoadSceneButtonClicked);
        }

        private void OnReloadButtonClicked()
        {
            _sceneLoader.ReloadActiveScene(reloadTransition);
        }

        private void OnLoadSceneButtonClicked()
        {
            int.TryParse(loadSceneInputField.text, out int buildIndex);
            _sceneLoader.LoadScene(buildIndex, _defaultTransition);
        }
    }
}
