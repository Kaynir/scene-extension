using Kaynir.Inspector;
using Kaynir.SceneExtension.Loaders;
using System.Collections;
using UnityEngine;

namespace Kaynir.SceneExtension.Transitions
{
    public class TransitionSequence : MonoBehaviour, ITransition
    {
        private enum ContextType { Persistant, Scene }

        [SerializeField]
        private ContextType context = ContextType.Persistant;

        [SerializeReference, DropdownOptions(false)]
        private DropdownList<Transition> transitions = new();

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
            for (int i = 0; i < transitions.List.Count; i++)
            {
                transitions.List[i].Initialize(sceneLoader);
                yield return transitions.List[i].FadeInRoutine(sceneLoader);
            }
        }

        public IEnumerator FadeOutRoutine(ISceneLoader sceneLoader)
        {
            for (int i = transitions.List.Count - 1; i >= 0; i--)
            {
                yield return transitions.List[i].FadeOutRoutine(sceneLoader);
                transitions.List[i].Clear(sceneLoader);
            }
        }
    }
}
