using System.Collections;
using System.Collections.Generic;
using Enemy;
using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects]
[CustomEditor(typeof(EnemyMovementSetting))]
public class EnemyMovementSettingEditor : Editor
{
    EnemyMovementSetting movementSettingInst;

    private SerializedProperty
        _movementType,
        _speed,
        _stoppingDistance,
        _xMin,
        _xMax,
        _yMin,
        _yMax,
        _zMin,
        _zMax,
        _selectedPFAlg,
        _heuristicFactor,
        _inEditorPathfindingTraverce,
        _trajectoryOptimization,
        _trajectorySmoothing,
        _moveVectorOrientation,
        _turnSpeed,
        _generateEventMessages,
        _generateCondMessages,
        _tracePath,
        _lineWidth,
        _lineMaterial,
        _showPathfindingZone,
        _pathfindingLevel,
        _lesion,
        _updateOffset;

    
    private void OnEnable()
    {
        _movementType = serializedObject.FindProperty("movementType");
        _speed = serializedObject.FindProperty("speed");
        _stoppingDistance = serializedObject.FindProperty("stoppingDistance");
        _xMin = serializedObject.FindProperty("xMin");
        _yMin = serializedObject.FindProperty("yMin");
        _zMin = serializedObject.FindProperty("zMin");
        _xMax = serializedObject.FindProperty("xMax");
        _yMax = serializedObject.FindProperty("yMax");
        _zMax = serializedObject.FindProperty("zMax");
        _selectedPFAlg = serializedObject.FindProperty("selectedPFAlg");
        _heuristicFactor = serializedObject.FindProperty("heuristicFactor");
        _inEditorPathfindingTraverce = serializedObject.FindProperty("inEditorPathfindingTraverce");
        _trajectoryOptimization = serializedObject.FindProperty("trajectoryOptimization");
        _trajectorySmoothing = serializedObject.FindProperty("trajectorySmoothing");
        _moveVectorOrientation = serializedObject.FindProperty("moveVectorOrientation");
        _turnSpeed = serializedObject.FindProperty("turnSpeed");
        _generateEventMessages = serializedObject.FindProperty("generateEventMessages");
        _generateCondMessages = serializedObject.FindProperty("generateCondMessages");
        _tracePath = serializedObject.FindProperty("tracePath");
        _lineWidth = serializedObject.FindProperty("lineWidth");
        _lineMaterial = serializedObject.FindProperty("lineMaterial");
        _showPathfindingZone = serializedObject.FindProperty("showPathfindingZone");
        _pathfindingLevel = serializedObject.FindProperty("pathfindingLevel");
    }


    public override void OnInspectorGUI()
    {
//        base.OnInspectorGUI();
        movementSettingInst = (EnemyMovementSetting) target;

        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("General settings:", EditorStyles.boldLabel);

        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("Movement type:");
        EnemyMovementType newType = (EnemyMovementType) _movementType.enumValueIndex;
        newType = (EnemyMovementType) EditorGUILayout.EnumPopup(newType);
        _movementType.enumValueIndex = (int) newType;
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        _speed.floatValue = Mathf.Max(EditorGUILayout.FloatField(new GUIContent("Speed:", "For ground movement best average is 2, for flying is 0.05f"), _speed.floatValue), float.Epsilon);
        EditorGUILayout.EndHorizontal();

        if (movementSettingInst.movementType == EnemyMovementType.FLYING)
        {
            EditorGUILayout.LabelField("Game zone constraints:", EditorStyles.boldLabel);
            EditorGUILayout.BeginHorizontal();
            _xMin.floatValue = EditorGUILayout.FloatField("XMin & XMax: ", _xMin.floatValue);
            _xMax.floatValue = EditorGUILayout.FloatField(_xMax.floatValue);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            _yMin.floatValue = EditorGUILayout.FloatField("YMin & YMax: ", _yMin.floatValue);
            _yMax.floatValue = EditorGUILayout.FloatField(_yMax.floatValue);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            _zMin.floatValue = EditorGUILayout.FloatField("ZMin & ZMax: ", _zMin.floatValue);
            _zMax.floatValue = EditorGUILayout.FloatField(_zMax.floatValue);
            EditorGUILayout.EndHorizontal();



            EditorGUILayout.LabelField("Pathfinding settings:", EditorStyles.boldLabel);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Pathfinding algorithm:");
            Pursuer.PathfindingAlgorithm newEnum = (Pursuer.PathfindingAlgorithm) _selectedPFAlg.enumValueIndex;
            newEnum = (Pursuer.PathfindingAlgorithm) EditorGUILayout.EnumPopup(newEnum);
            _selectedPFAlg.enumValueIndex = (int) newEnum;
            EditorGUILayout.EndHorizontal();
            if (movementSettingInst.selectedPFAlg == Pursuer.PathfindingAlgorithm.AStar)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Heuristic factor:");
                _heuristicFactor.floatValue = EditorGUILayout.Slider(_heuristicFactor.floatValue, .5f, 1f);
                EditorGUILayout.EndHorizontal();
            }
            else
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("In-editor pathfinding traverce:");
                _inEditorPathfindingTraverce.boolValue = EditorGUILayout.Toggle(_inEditorPathfindingTraverce.boolValue);
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Number of pathfinding level: ", EditorStyles.wordWrappedLabel);
            SpaceManager sMInst = Component.FindObjectOfType<SpaceManager>();
            if (sMInst != null)
                _pathfindingLevel.intValue =
                    EditorGUILayout.IntSlider(movementSettingInst.pathfindingLevel, 0,
                        sMInst.gridDetailLevelsCount - 1);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Trajectory optimization:");
            _trajectoryOptimization.boolValue = EditorGUILayout.Toggle(_trajectoryOptimization.boolValue);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Trajectory smoothing:");
            _trajectorySmoothing.boolValue = EditorGUILayout.Toggle(_trajectorySmoothing.boolValue);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.LabelField("Movement settings:", EditorStyles.boldLabel);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Turn in the dir. of movement:");
            _moveVectorOrientation.boolValue = EditorGUILayout.Toggle(_moveVectorOrientation.boolValue);
            EditorGUILayout.EndHorizontal();
            if (_moveVectorOrientation.boolValue)
                _turnSpeed.floatValue = Mathf.Max(EditorGUILayout.FloatField("Turn speed:", _turnSpeed.floatValue),
                    float.Epsilon);

            EditorGUILayout.LabelField("Other settings:", EditorStyles.boldLabel);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Generate event messages:");
            _generateEventMessages.boolValue = EditorGUILayout.Toggle(_generateEventMessages.boolValue);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Generate condition messages:");
            _generateCondMessages.boolValue = EditorGUILayout.Toggle(_generateCondMessages.boolValue);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.LabelField("Debug settings:", EditorStyles.boldLabel);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("In-game trajectory trace:");
            _tracePath.boolValue = EditorGUILayout.Toggle(_tracePath.boolValue);
            EditorGUILayout.EndHorizontal();
            if (_tracePath.boolValue)
            {
                _lineWidth.floatValue =
                    Mathf.Max(EditorGUILayout.FloatField("Line width:", _lineWidth.floatValue), .1f);
                _lineMaterial.objectReferenceValue = (Material) EditorGUILayout.ObjectField("Line material:",
                    (Material) (_lineMaterial.objectReferenceValue), typeof(Material), true);
            }

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Show pathfinding area in the editor:");
            _showPathfindingZone.boolValue = EditorGUILayout.Toggle(_showPathfindingZone.boolValue);
            EditorGUILayout.EndHorizontal();

        }
        else
        {
            EditorGUILayout.LabelField("Movement settings:", EditorStyles.boldLabel);
            
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Stopping distance:");
            _stoppingDistance.floatValue = EditorGUILayout.Slider(_stoppingDistance.floatValue, 1f, 10f);
            EditorGUILayout.EndHorizontal();
        }
        
        EditorGUILayout.EndVertical();
        serializedObject.ApplyModifiedProperties();
    }
}
