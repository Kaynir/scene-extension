using System;
using System.Collections.Generic;
using UnityEngine;

namespace Kaynir.Inspector
{
    public interface IDropdownList
    {
        Type ElementType { get; }
        void AddElement(Type type);
        bool HasElementOfType(Type type);
    }

    public class DropdownOptionsAttribute : PropertyAttribute
    {
        public bool AllowDuplicates;

        public DropdownOptionsAttribute(bool allowDuplicates)
        {
            AllowDuplicates = allowDuplicates;
        }
    }

    [Serializable]
    public class DropdownList<T> : IDropdownList
    {
        [SerializeReference]
        public List<T> List = new();

        public Type ElementType => typeof(T);

        public void AddElement(Type type)
        {
            if (!ElementType.IsAssignableFrom(type))
            {
                Debug.LogError($"Failed to add {type.Name} as {ElementType.Name} instance.");
                return;
            }

            List.Add((T)Activator.CreateInstance(type));
        }

        public bool HasElementOfType(Type type)
        {
            return List.Exists(element => element.GetType() == type);
        }
    }
}
