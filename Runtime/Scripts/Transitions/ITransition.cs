using Kaynir.SceneExtension.Loaders;
using System.Collections;

namespace Kaynir.SceneExtension.Transitions
{
    public interface ITransition
    {
        /// <summary>
        /// Initializes transition before fade in coroutine.
        /// </summary>
        void Initialize(ISceneLoader sceneLoader);
        
        /// <summary>
        /// Clears transition after fade out coroutine.
        /// </summary>
        void Clear(ISceneLoader sceneLoader);

        /// <summary>
        /// Transition coroutine to cover scene loading process.
        /// </summary>
        IEnumerator FadeInRoutine(ISceneLoader sceneLoader);
        
        /// <summary>
        /// Transition coroutine to complete scene loading process.
        /// </summary>
        IEnumerator FadeOutRoutine(ISceneLoader sceneLoader);
    }
}
