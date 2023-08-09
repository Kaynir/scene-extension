using System;
using Kaynir.SceneExtension.Tools;
using UnityEngine;

namespace Kaynir.SceneExtension.Loading
{
    public struct SceneOperation
    {
        public Func<AsyncOperation> asyncFunc;

        public static SceneOperation LoadSingle(int buildIndex)
        {
            return new SceneOperation()
            {
                asyncFunc = () => SceneHelper.LoadSingle(buildIndex)
            };
        }

        public static SceneOperation LoadAdditive(int buildIndex, bool setActive = false)
        {
            return new SceneOperation()
            {
                asyncFunc = () => SceneHelper.LoadAdditive(buildIndex, setActive)
            };
        }

        public static SceneOperation Unload(int buildIndex)
        {
            return new SceneOperation()
            {
                asyncFunc = SceneHelper.IsLoaded(buildIndex) 
                ? () => SceneHelper.Unload(buildIndex)
                : null
            };
        }
    }
}