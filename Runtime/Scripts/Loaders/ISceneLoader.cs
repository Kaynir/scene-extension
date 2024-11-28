using Kaynir.SceneExtension.Transitions;
using System;
using System.Collections.Generic;

namespace Kaynir.SceneExtension.Loaders
{
    public interface ISceneLoader
    {
        delegate void SceneAction(int sceneBuildIndex, float progress);
        
        event SceneAction LoadStarted;
        event SceneAction LoadTicked;
        event SceneAction LoadEnded;

        /// <summary>
        /// Loads the scene in single mode with transition.
        /// </summary>
        void LoadScene(int sceneBuildIndex, ITransition transition, Action onBeforeExit = null);

        /// <summary>
        /// Loads the scene collection with transition (first scene in single and others in additive mode).
        /// </summary>
        void LoadScenes(IEnumerable<int> sceneBuildIndexes, ITransition transition, Action onBeforeExit = null);

        /// <summary>
        /// Reloads the current active scene in single mode with transition.
        /// </summary>
        void ReloadActiveScene(ITransition transition, Action onBeforeExit = null);

        /// <summary>
        /// Set condition to delay scene activation and fade out sequence.
        /// </summary>
        void SetWaitActivation(Func<bool> waitActivation);
    }
}
