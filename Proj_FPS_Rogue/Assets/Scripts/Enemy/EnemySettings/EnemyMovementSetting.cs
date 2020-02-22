using UnityEngine;

namespace Enemy
{
    [CreateAssetMenu(fileName = "EnemyMovementSetting", menuName = "Enemy/MovementSetting", order = 1)]
    public class EnemyMovementSetting : ScriptableObject
    {
        #region GENERAL

        public EnemyMovementType movementType;
        
        public float speed;
        
        #endregion

        #region GROUND

        public float stoppingDistance;

        #endregion
        
        #region FLYING

        //Game zone constraints
        public float xMin = -40f;
        public float xMax = 40f;
        public float yMin = 0f;
        public float yMax = 20f;
        public float zMin = -40f;
        public float zMax = 40f;
        
        //Pathfinding settings
        public Pursuer.PathfindingAlgorithm selectedPFAlg;
        public int pathfindingLevel = 1;
        public bool inEditorPathfindingTraverce;
        public float heuristicFactor = 0.9f;
        public bool trajectoryOptimization, trajectorySmoothing = true;
        
        //Movement settings
        public bool moveVectorOrientation;
        public float turnSpeed;
        public bool reckDynamicObstacles;
        
        //Update
        public float lesion;
        public float updateOffset;
        
        //Other settings
        public bool generateEventMessages;

        public bool generateCondMessages;

        //Debug settings
        public bool showPathfindingZone;
        public bool tracePath;
        public float lineWidth;
        public Material lineMaterial;


        #endregion
    }
}