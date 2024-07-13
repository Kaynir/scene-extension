using Cysharp.Threading.Tasks;
using System.Threading;

namespace Kaynir.SceneExtension.Transitions
{
    public interface ISceneTransition
    {
        UniTask FadeInTask(CancellationToken cancellationToken = default);
        UniTask FadeOutTask(CancellationToken cancellationToken = default);
    }
}
