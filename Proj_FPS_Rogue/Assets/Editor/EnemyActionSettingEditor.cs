using System.Collections;
using System.Collections.Generic;
using Enemy;
using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects]
[CustomEditor(typeof(EnemyActionSetting))]
public class EnemyActionSettingEditor : Editor
{
    EnemyActionSetting _actionSetting;

    private SerializedProperty
        _enemyActionType,
        _distanceAttackType,
        /*_meleeAttackType,*/
        _rate,
        _damage,
        _maxAngle,
        _maxDistance,
        _attackCalculationType,
        _projectilePrefab,
        _projectileSpeed,
        _angleTriple,
        _offset,
        _machineGunRate,
        _magazineAmount;


    private void OnEnable()
    {
        _enemyActionType = serializedObject.FindProperty("actionType");
        _distanceAttackType = serializedObject.FindProperty("distanceAttackType");
        /*_meleeAttackType  = serializedObject.FindProperty("meleeAttackType");*/
        _rate = serializedObject.FindProperty("attackRate");
        _damage = serializedObject.FindProperty("damage");
        _maxAngle = serializedObject.FindProperty("maxAngle");
        _maxDistance = serializedObject.FindProperty("maxDistance");
        _attackCalculationType = serializedObject.FindProperty("attackCalculationType");
        _projectilePrefab = serializedObject.FindProperty("projectilePrefab");
        _projectileSpeed = serializedObject.FindProperty("projectileSpeed");
        _angleTriple = serializedObject.FindProperty("angleTripleAttack");
        _offset = serializedObject.FindProperty("offset");
        _machineGunRate = serializedObject.FindProperty("rate");
        _magazineAmount = serializedObject.FindProperty("magazineAmount");
    }
    
     public override void OnInspectorGUI()
    {
//        base.OnInspectorGUI();
        _actionSetting = (EnemyActionSetting) target;

        EditorGUILayout.BeginVertical();
       
        EditorGUILayout.LabelField("General settings:", EditorStyles.boldLabel);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Action type:");
        EnemyActionType newType = (EnemyActionType) _enemyActionType.enumValueIndex;
        newType = (EnemyActionType) EditorGUILayout.EnumPopup(newType);
        _enemyActionType.enumValueIndex = (int) newType;
        EditorGUILayout.EndHorizontal();
        
        EditorGUILayout.BeginHorizontal();
        _damage.intValue = Mathf.Max(EditorGUILayout.IntField(new GUIContent("Damage:"), _damage.intValue), 5);
        EditorGUILayout.EndHorizontal();
        
        EditorGUILayout.BeginHorizontal();
        _rate.floatValue = Mathf.Max(EditorGUILayout.FloatField(new GUIContent("Attack rate:"), _rate.floatValue), 3);
        EditorGUILayout.EndHorizontal();
        
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Max attack angle:");
        _maxAngle.intValue = EditorGUILayout.IntSlider(_maxAngle.intValue, 0, 40);
        EditorGUILayout.EndHorizontal();
        
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Max distance for attack:");
        _maxDistance.intValue = EditorGUILayout.IntSlider(_maxDistance.intValue, 0, 30);
        EditorGUILayout.EndHorizontal();
        
        if (_actionSetting.actionType == EnemyActionType.DISTANCE)
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Distance attack settings:", EditorStyles.boldLabel);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Distance attack type:");
            DistanceAttackType distance = (DistanceAttackType) _distanceAttackType.enumValueIndex;
            distance = (DistanceAttackType) EditorGUILayout.EnumPopup(distance);
            _distanceAttackType.enumValueIndex = (int) distance;
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Attack calculation type:");
            AttackCalculationType calculationType = (AttackCalculationType) _attackCalculationType.enumValueIndex;
            calculationType = (AttackCalculationType) EditorGUILayout.EnumPopup(calculationType);
            _attackCalculationType.enumValueIndex = (int) calculationType;
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(_projectilePrefab, new GUIContent("Projectile prefab:"));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.Slider(_projectileSpeed, 0, 50, new GUIContent("Projectile speed: "));
            EditorGUILayout.EndHorizontal();

            if (_actionSetting.distanceAttackType == DistanceAttackType.TRIPLE)
            {
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Triple attack settings:", EditorStyles.boldLabel);
                
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.Slider(_angleTriple, 0, 30, new GUIContent("Projectile triple angle: "));
                EditorGUILayout.EndHorizontal();
            }
            else if (_actionSetting.distanceAttackType == DistanceAttackType.MACHINE_GUN)
            {
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Machine gun attack settings:", EditorStyles.boldLabel);
                
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Max offset for attack:");
                _offset.floatValue = EditorGUILayout.FloatField(_offset.floatValue);
                EditorGUILayout.EndHorizontal();
                
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Rate in milliseconds:");
                _machineGunRate.intValue = EditorGUILayout.IntField(_machineGunRate.intValue);
                EditorGUILayout.EndHorizontal();
                
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Magazine amount:");
                _magazineAmount.intValue = EditorGUILayout.IntField(_magazineAmount.intValue);
                EditorGUILayout.EndHorizontal();
            }
        }
        
        EditorGUILayout.EndVertical();
        serializedObject.ApplyModifiedProperties();
    }

     void Some()
     {
         
        
     
        
        

        if (_actionSetting.actionType == EnemyActionType.DISTANCE)
        {

            

        }
        
     }
}
