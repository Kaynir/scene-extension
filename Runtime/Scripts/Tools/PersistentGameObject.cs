using UnityEngine;

namespace Kaynir.SceneExtension.Tools
{
    public class PersistentGameObject : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
