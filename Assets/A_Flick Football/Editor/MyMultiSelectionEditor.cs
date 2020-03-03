using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(MyMultiSelection))]
[CanEditMultipleObjects]

public class MyMultiSelectionEditor : Editor {
	
	MyMultiSelection _target;
	
	void OnEnable(){
		_target = (MyMultiSelection)target;
		
	}
	
	public override void OnInspectorGUI() {
//		EditorGUILayout.BeginHorizontal();
//        _target.source = (GameObject) EditorGUILayout.ObjectField(_target.source, typeof(GameObject), true);
//        EditorGUILayout.EndHorizontal();
		
		_target.count = EditorGUILayout.IntField("Count" , _target.count);
		
		if(_target.count > _target._gos.Count)
			_target._gos.Add(null);
		
		if(_target.count < _target._gos.Count)
			_target._gos.RemoveAt(_target._gos.Count - 1);
		
		for(int i = 0; i < _target._gos.Count; ++i) {
			_target._gos[i] = (GameObject) EditorGUILayout.ObjectField(_target._gos[i], typeof(GameObject), true);
		}
	}
	
	void OnSceneGUI(){ 
		
		for(int i = 0; i < _target._gos.Count; ++i) {
			GameObject go = (GameObject) _target._gos[i];
//			go.transform.LookAt(_target.gameObject.transform);
			Handles.PositionHandle(go.transform.position, go.transform.rotation);
		}
	}
}
