using Kaynir.SceneExtension.Transitions;
using System.Threading;

namespace Kaynir.SceneExtension.Loaders
{
    public interface ISceneLoader
    {
        delegate void SceneAction(int buildIndex, float progress);
        
        event SceneAction LoadStartEvent;
        event SceneAction LoadProgressEvent;
        event SceneAction LoadEndEvent;

        void LoadScene(ISceneTransition transition, CancellationToken token = default, params int[] buildIndexes);
        void LoadScene(int buildIndex, ISceneTransition transition, CancellationToken token = default);
    }
}
