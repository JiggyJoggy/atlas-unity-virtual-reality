﻿using System;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace MaxyGames.UNode.Editors.Control {
	class BoundsFieldControl : FieldControl<Bounds> {
		public override void Draw(Rect position, GUIContent label, object value, Type type, Action<object> onChanged, uNodeUtility.EditValueSettings settings) {
			EditorGUI.BeginChangeCheck();
			ValidateValue(ref value);
			var oldValue = (Bounds)value;
			var newValue = EditorGUI.BoundsField(position, label, oldValue);
			if(EditorGUI.EndChangeCheck()) {
				onChanged(newValue);
			}
		}

		public override void DrawLayouted(object value, GUIContent label, Type type, Action<object> onChanged, uNodeUtility.EditValueSettings settings) {
			DrawDecorators(settings);
			EditorGUI.BeginChangeCheck();
			ValidateValue(ref value);
			var oldValue = (Bounds)value;
			var newValue = EditorGUILayout.BoundsField(label, oldValue);
			if(EditorGUI.EndChangeCheck()) {
				onChanged(newValue);
			}
		}
	}
}