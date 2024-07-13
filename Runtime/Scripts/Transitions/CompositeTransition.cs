using Cysharp.Threading.Tasks;
using System.Threading;

namespace Kaynir.SceneExtension.Transitions
{
    public class CompositeTransition : SceneTransition
    {
        private readonly SceneTransition[] _sceneTransitions;

        public CompositeTransition(params SceneTransition[] sceneTransitions)
        {
            _sceneTransitions = sceneTransitions;
        }

        public override async UniTask FadeInTask(CancellationToken cancellationToken = default)
        {
            for (int i = 0; i < _sceneTransitions.Length; i++)
            {
                await _sceneTransitions[i].FadeInTask(cancellationToken);
            }
        }

        public override async UniTask FadeOutTask(CancellationToken cancellationToken = default)
        {
            for (int i = _sceneTransitions.Length - 1; i >= 0; i--)
            {
                await _sceneTransitions[i].FadeOutTask(cancellationToken);
            }
        }
    }
}
