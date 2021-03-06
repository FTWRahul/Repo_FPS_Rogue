using NaughtyAttributes;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MovementController : MonoBehaviour
{
    
    bool HasInput => inputVector != Vector2.zero;
    
    #region Locomotion
    [SerializeField] private float walkSpeed = 2f;
    [SerializeField] private float runSpeed = 3f;
    [SerializeField] private float jumpSpeed = 5f;
    [Range(0f,1f)][SerializeField] private float moveBackwardsSpeedPercent = 0.5f;
    [Range(0f,1f)][SerializeField] private float moveSideSpeedPercent = 0.75f;
    #endregion
    
    #region Run Settings
    [Range(-1f,1f)][SerializeField] private float canRunThreshold = 0.8f;
    [SerializeField] private AnimationCurve runTransitionCurve = AnimationCurve.EaseInOut(0f,0f,1f,1f);
    #endregion
    
    #region Smooth input
    [Range(1f,100f)] [SerializeField] private float smoothInputSpeed = 5f;
    [Range(1f,100f)] [SerializeField] private float smoothVelocitySpeed = 5f;
    [Range(1f,100f)] [SerializeField] private float smoothFinalDirectionSpeed = 5f;
    #endregion
    
    #region Gravity
    [SerializeField] private float gravityMultiplier = 2.5f;
    [SerializeField] private float stickToGroundForce = 5f;
    
    [SerializeField] private LayerMask groundLayer = ~0;
    [Range(0f,1f)][SerializeField] private float rayLength = 0.1f;
    [Range(0.01f,1f)] [SerializeField] private float raySphereRadius = 0.1f;
    #endregion

    #region Non serialize private
    
    private CharacterController _characterController;
    private CharacterData _characterData;
    private RaycastHit _raycastHit;
    
    #endregion

    #region DEBUG
    
    [BoxGroup("DEBUG")][ReadOnly] public Vector2 inputVector;
    [BoxGroup("DEBUG")][ReadOnly] public bool isSprinting;
    [BoxGroup("DEBUG")][ReadOnly] public bool isRunning;
    [BoxGroup("DEBUG")][ReadOnly] public bool isJumpClicked;
    [BoxGroup("DEBUG")][ReadOnly] public bool isFalling;
    [BoxGroup("DEBUG")][ReadOnly] public bool isGrounded;
    
    [Space]
    [BoxGroup("DEBUG")][SerializeField][ReadOnly] private Vector2 smoothInputVector;
    
    [Space]
    [BoxGroup("DEBUG")][SerializeField][ReadOnly] private Vector3 finalMoveDir;
    [BoxGroup("DEBUG")][SerializeField][ReadOnly] private Vector3 smoothFinalMoveDir;
    [BoxGroup("DEBUG")][SerializeField][ReadOnly] private bool previouslyGrounded;
    [Space] 
    [BoxGroup("DEBUG")][SerializeField][ReadOnly] private Vector3 finalMoveVector;
    
    [Space]
    [BoxGroup("DEBUG")][SerializeField][ReadOnly] private float currentSpeed;
    [BoxGroup("DEBUG")][SerializeField][ReadOnly] private float smoothCurrentSpeed;
    [BoxGroup("DEBUG")][SerializeField][ReadOnly] private float finalSmoothCurrentSpeed;
    [BoxGroup("DEBUG")][SerializeField][ReadOnly] private float walkRunSpeedDifference;
    
    [Space]
    [BoxGroup("DEBUG")][SerializeField][ReadOnly] private float inAirTimer;
    #endregion
    
    private void Start()
    {
        GetComponents();
    }

    private void GetComponents()
    {
        _characterController = GetComponent<CharacterController>();
        _characterData = GetComponent<CharacterData>();
    }
    
    protected virtual void Update()
    {
        if(_characterController)
        {
            // Check if Grounded,Wall etc
            CheckIfGrounded();

            // Apply Smoothing
            SmoothInput();
            SmoothSpeed();
            SmoothDir();
            
            // Calculate Movement
            CalculateMovementDirection();
            CalculateSpeed();
            CalculateFinalMovement();

            ApplyGravity();
            ApplyMovement();
            
            //Set all states to character data 
            SetData();

            /*previouslyGrounded = isGrounded;*/
        }
    }
    private void CheckIfGrounded()
    {
        Vector3 origin = transform.position + _characterController.center;

        isGrounded = Physics.SphereCast(origin, raySphereRadius,Vector3.down,out _raycastHit,rayLength, groundLayer);
        Debug.DrawRay(origin,Vector3.down * (rayLength),Color.red);
    }
    
    void SmoothInput()
    {
        inputVector = inputVector.normalized;
        smoothInputVector = Vector2.Lerp(smoothInputVector, inputVector,Time.deltaTime * smoothInputSpeed);
    }
    
    void SmoothSpeed()
    {
        smoothCurrentSpeed = Mathf.Lerp(smoothCurrentSpeed, currentSpeed, Time.deltaTime * smoothVelocitySpeed);

        if(isSprinting && CanRun())
        {
            float walkRunPercent = Mathf.InverseLerp(walkSpeed,runSpeed, smoothCurrentSpeed);
            finalSmoothCurrentSpeed = runTransitionCurve.Evaluate(walkRunPercent) * walkRunSpeedDifference + walkSpeed;
        }
        else
        {
            finalSmoothCurrentSpeed = smoothCurrentSpeed;
        }
    }
    
    void SmoothDir()
    {
        smoothFinalMoveDir = Vector3.Lerp(smoothFinalMoveDir, finalMoveDir, Time.deltaTime * smoothFinalDirectionSpeed);
        Debug.DrawRay(transform.position, smoothFinalMoveDir, Color.yellow);
    }
    
    bool CanRun()
    {
        Vector3 normalizedDir = Vector3.zero;

        if(smoothFinalMoveDir != Vector3.zero)
            normalizedDir = smoothFinalMoveDir.normalized;

        float _dot = Vector3.Dot(transform.forward,normalizedDir);
        return _dot >= canRunThreshold ? true : false;
    }
    
    void CalculateMovementDirection()
    {
        Vector3 desiredDir =  transform.forward * smoothInputVector.y +  transform.right * smoothInputVector.x;
        Vector3 flattenDir = FlattenVectorOnSlopes(desiredDir);

        finalMoveDir = flattenDir;
    }
    
    Vector3 FlattenVectorOnSlopes(Vector3 vectorToFlat)
    {
        if(isGrounded)
            vectorToFlat = Vector3.ProjectOnPlane(vectorToFlat, _raycastHit.normal);
                    
        return vectorToFlat;
    }
    
    void CalculateSpeed()
    {
        currentSpeed = isSprinting && CanRun() ? runSpeed : walkSpeed;
        currentSpeed = !HasInput ? 0f : currentSpeed;
        currentSpeed = inputVector.y == -1 ? currentSpeed * moveBackwardsSpeedPercent : currentSpeed;
        currentSpeed = inputVector.x != 0 && inputVector.y ==  0 ? currentSpeed * moveSideSpeedPercent :  currentSpeed;
    }
    
    void CalculateFinalMovement()
    {
        float smoothInputVectorMagnitude =  1f;
        Vector3 finalVector = smoothFinalMoveDir * (finalSmoothCurrentSpeed * smoothInputVectorMagnitude);

        finalMoveVector.x = finalVector.x ;
        finalMoveVector.z = finalVector.z ;

        if(_characterController.isGrounded)
            finalMoveVector.y += finalVector.y ;
    }
    
    void ApplyGravity()
    {
        if(_characterController.isGrounded)
        {
            isFalling = false;
            inAirTimer = 0f;
            finalMoveVector.y = -stickToGroundForce;

            HandleJump();
        }
        else
        {
            isFalling = true;
            inAirTimer += Time.deltaTime;
            finalMoveVector += Physics.gravity * gravityMultiplier * Time.deltaTime;
        }
    }
    
    void HandleJump()
    {
        if(isJumpClicked)
        {
            finalMoveVector.y = jumpSpeed;
                    
            previouslyGrounded = true;
            isGrounded = false;
        }
    }
    
    void ApplyMovement()
    {
        _characterController.Move(finalMoveVector * Time.deltaTime);
    }

    void SetData()
    {
        if (isGrounded)
        {
            _characterData.SetLocoState(HasInput ? CharacterData.LocoState.GROUND_MOVE : CharacterData.LocoState.STAND);
            _characterData.sprint = isSprinting;
        }
        if (isJumpClicked)
        {
            _characterData.SetLocoState(CharacterData.LocoState.JUMP);
        }
        else if (isFalling)
        {
            _characterData.SetLocoState(CharacterData.LocoState.IN_AIR);
        }
    }

}