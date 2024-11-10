using Kaynir.Inspector;
using Kaynir.SceneExtension.Loaders;
using System.Collections;
using UnityEngine;

namespace Kaynir.SceneExtension.Transitions
{
    public class TransitionSequence : MonoBehaviour, ITransitionModule
    {
        private enum ContextType { Persistant, Scene }

        [SerializeField]
        private ContextType context = ContextType.Persistant;

        [SerializeReference, DropdownOptions(false)]
        private DropdownList<TransitionModule> modules = new();

        public void Initialize(ISceneLoader sceneLoader)
        {
            if (context == ContextType.Scene)
            {
                DontDestroyOnLoad(gameObject);
            }
        }

        public void Clear(ISceneLoader sceneLoader)
        {
            if (context == ContextType.Scene)
            {
                Destroy(gameObject);
            }
        }

        public IEnumerator FadeInRoutine(ISceneLoader sceneLoader)
        {
            for (int i = 0; i < modules.List.Count; i++)
            {
                modules.List[i].Initialize(sceneLoader);
                yield return modules.List[i].FadeInRoutine(sceneLoader);
            }
        }

        public IEnumerator FadeOutRoutine(ISceneLoader sceneLoader)
        {
            for (int i = modules.List.Count - 1; i >= 0; i--)
            {
                yield return modules.List[i].FadeOutRoutine(sceneLoader);
                modules.List[i].Clear(sceneLoader);
            }
        }
    }
}
