using Cysharp.Threading.Tasks;
using System.Threading;

namespace Kaynir.SceneExtension.Transitions
{
    public abstract class SceneTransition : ISceneTransition
    {
        public abstract UniTask FadeInTask(CancellationToken cancellationToken = default);
        public abstract UniTask FadeOutTask(CancellationToken cancellationToken = default);
    }
}
