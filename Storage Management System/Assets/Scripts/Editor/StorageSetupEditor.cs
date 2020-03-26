using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(StorageSetup))]
public class StorageSetupEditor : Editor
{
    bool _collectableCheckBox = true;
    bool _depositableCheckBox = false;
    bool _collectableCheckBoxClicked;
    bool _depositableCheckBoxClicked;

    int _maximumStorageSlots = 0;

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        StorageSetup storageSetup = (StorageSetup)target;

        GUILayout.Label("Storage type:", GUIStyle.none);
        GUILayout.BeginHorizontal(GUIStyle.none);
        _collectableCheckBoxClicked = EditorGUILayout.Toggle("Collectable", _collectableCheckBox);
        _depositableCheckBoxClicked = EditorGUILayout.Toggle("Depositable", _depositableCheckBox);

        // Toggle between the two selection boxes, only one can be true at a time
        if (_collectableCheckBox != _collectableCheckBoxClicked) {
            _collectableCheckBox = _collectableCheckBoxClicked;
            _depositableCheckBox = !_collectableCheckBox;
        } else if (_depositableCheckBox != _depositableCheckBoxClicked) {
            _depositableCheckBox = _depositableCheckBoxClicked;
            _collectableCheckBox = !_depositableCheckBox;
        }

        GUILayout.EndHorizontal();

        GUILayout.Label("Storage size:", GUIStyle.none);
        _maximumStorageSlots = (int)EditorGUILayout.Slider(_maximumStorageSlots, 0f, 50f);


        if (GUILayout.Button("Generate Storage Dependancies")) {
            storageSetup.SetupStorage(_depositableCheckBox, _maximumStorageSlots);
        }
    }
}
