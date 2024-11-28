using System;
using System.Linq;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Kaynir.Inspector.Editor
{
    [CustomPropertyDrawer(typeof(DropdownList<>))]
    public class DropdownListDrawer : PropertyDrawer
    {
        private SerializedProperty _targetProperty;
        private SerializedProperty _listProperty;
        private IDropdownList _dropdownList;
        private ReorderableList _listDrawer;
        private Type[] _types;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            _targetProperty ??= property;
            _listProperty ??= property.FindPropertyRelative("List");
            _dropdownList = (IDropdownList)_targetProperty.managedReferenceValue;
            _types ??= GetElementTypes();
            _listDrawer ??= new ReorderableList(property.serializedObject, _listProperty)
            {
                displayAdd = true,
                displayRemove = true,
                draggable = true,
                showDefaultBackground = true,
                drawHeaderCallback = DrawHeader,
                onAddDropdownCallback = DrawDropdown,
                drawElementCallback = DrawElement,
                elementHeightCallback = GetElementHeight
            };

            return EditorGUI.GetPropertyHeight(_listProperty);
        }

        private void DrawHeader(Rect rect)
        {
            EditorGUI.LabelField(rect, _targetProperty.displayName);
        }

        private float GetElementHeight(int index)
        {
            return EditorGUI.GetPropertyHeight(_listProperty.GetArrayElementAtIndex(index));
        }

        private void DrawElement(Rect rect, int index, bool isActive, bool isFocused)
        {
            rect.xMin += 8f;
            SerializedProperty element = _listProperty.GetArrayElementAtIndex(index);
            EditorGUI.PropertyField(rect, element, new(element.managedReferenceValue.GetType().Name), true);
        }

        private void DrawDropdown(Rect buttonRect, ReorderableList list)
        {
            GenericMenu menu = new GenericMenu();
            var dropdownOptions = (DropdownOptionsAttribute)fieldInfo
                .GetCustomAttributes(typeof(DropdownOptionsAttribute), false)
                .First();

            var activeTypes = (dropdownOptions != null) && !dropdownOptions.AllowDuplicates
                ? _types.Where(type => !_dropdownList.HasElementOfType(type))
                : _types;

            if (activeTypes.Count() == 0)
            {
                menu.AddDisabledItem(new("No implementations found."));
                menu.ShowAsContext();
                return;
            }

            for (int i = 0; i < activeTypes.Count(); i++)
            {
                int typeIndex = i;
                menu.AddItem(new(activeTypes.ElementAt(i).Name), false, () =>
                {
                    Type selectedType = activeTypes.ElementAt(typeIndex);
                    _dropdownList.AddElement(selectedType);
                });
            }

            menu.ShowAsContext();
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            _listDrawer.DoList(position);
            
            EditorGUI.EndProperty();
        }

        private Type[] GetElementTypes()
        {
            Type elementType = _dropdownList.ElementType;

            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => elementType.IsAssignableFrom(type) && !type.IsAbstract)
                .ToArray();
        }
    }
}
