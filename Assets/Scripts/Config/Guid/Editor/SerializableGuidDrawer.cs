using System;

using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;

using UnityEditor;

using UnityEngine;

public class SerializableGuidDrawer : OdinValueDrawer<SerializableGuid>
{
    private int _newDebounceClickedCount;
    private const int c_newDebounceClickCount = 3;

    protected override void Initialize()
    {
        base.Initialize();
        _newDebounceClickedCount = c_newDebounceClickCount;
    }

    protected override void DrawPropertyLayout(GUIContent label)
    {
        using (new GUILayout.HorizontalScope())
        {
            EditorGUILayout.LabelField(label, new GUIContent(ValueEntry.SmartValue.ToString()));
            if (ValueEntry.ValueCount == 1)
            {
                GUIContent newButtonContent = new GUIContent($"New ({_newDebounceClickedCount})", $"To prevent accidental guid change, new button must be pressed {_newDebounceClickedCount} more times");
                if (GUILayout.Button(newButtonContent, GUILayout.MaxWidth(60)))
                {
                    _newDebounceClickedCount--;
                    if (_newDebounceClickedCount <= 0)
                    {
                        ValueEntry.SmartValue = SerializableGuid.New;
                        _newDebounceClickedCount = c_newDebounceClickCount;
                    }
                } 
            }
        }
    }
}