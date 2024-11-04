using Kaynir.SceneExtension.Transitions;
using System;
using System.Collections.Generic;

namespace Kaynir.SceneExtension.Loaders
{
    public interface ISceneLoader
    {
        delegate void SceneAction(int buildIndex, float progress);
        
        event SceneAction LoadStarted;
        event SceneAction LoadTicked;
        event SceneAction LoadEnded;

        /// <summary>
        /// Loads the scene in single mode with transition.
        /// </summary>
        void LoadScene(int sceneBuildIndex, ISceneTransition sceneTransition, Action onBeforeExit = null);

        /// <summary>
        /// Loads the scene collection with transition (first scene in single and others in additive mode).
        /// </summary>
        void LoadScenes(IEnumerable<int> sceneBuildIndexes, ISceneTransition sceneTransition, Action onBeforeExit = null);

        /// <summary>
        /// Reloads the current active scene in single mode with transition.
        /// </summary>
        void ReloadActiveScene(ISceneTransition sceneTransition, Action onBeforeExit = null);
    }
}
