using Kaynir.SceneExtension.Loaders;
using Kaynir.SceneExtension.Transitions;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Kaynir.SceneExtension.Examples
{
    public class ExampleServiceManager : MonoBehaviour
    {
        private static ExampleServiceManager _instance;

        private static ExampleServiceManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    CreateInstance();
                }

                return _instance;
            }
        }

        private static void CreateInstance()
        {
            GameObject gameObject = new(nameof(ExampleServiceManager));
            DontDestroyOnLoad(gameObject);
            
            _instance = gameObject.AddComponent<ExampleServiceManager>();
            _instance.BindServices(gameObject.transform);
        }

        public static bool TryGetService<T>(out T service)
        {
            return Instance.TryGetServiceInternal(out service);
        }

        [SerializeField]
        private TransitionSequence defaultTransitionPrefab;

        private Dictionary<Type, object> _services;

        private void BindServices(Transform parent)
        {
            _services = new();

            ISceneLoader sceneLoader = new SceneLoader(this);
            ITransition sceneTransition = Instantiate(defaultTransitionPrefab, parent);

            BindService(sceneLoader);
            BindService(sceneTransition);
        }

        private void BindService<T>(T instance)
        {
            if (TryGetServiceInternal<T>(out _))
            {
                Debug.Log($"Service of type {typeof(T)} is already exists.");
                return;
            }

            _services[typeof(T)] = instance;
        }

        private bool TryGetServiceInternal<T>(out T service)
        {
            _services.TryGetValue(typeof(T), out object currentService);

            if (currentService == null)
            {
                service = default;
                return false;
            }

            service = (T)currentService;
            return true;
        }

        private void OnDestroy()
        {
            _instance = null;
        }
    }
}
