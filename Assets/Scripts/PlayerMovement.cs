using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed=5f;
    [SerializeField] float turnSpeed = 5f;
    [SerializeField] float animTurnSpeed=5f;
    
    [Header("Inventory")]
    [SerializeField] InventoryComponent inventoryComponent;

    [Header("HealthAndDamage")]
    [SerializeField] HealthComponent healthComponent;
    [SerializeField] PlayerHealthBar healthBar;


    public CharacterController controller; // Reference to the Character Controller
    private PlayerInput playerInput; // Reference to the PlayerInput component

    Camera mainCam;
    CameraController cameraController;

    Animator animator;

    float animatorTurnSpeed;
    void Start()
    {
        mainCam = Camera.main;
        controller = GetComponent<CharacterController>(); // Assigning the Character Controller reference
        playerInput = GetComponent<PlayerInput>(); // Assigning the PlayerInput component
        cameraController = FindObjectOfType<CameraController>();
        animator = GetComponent<Animator>();

        healthComponent.onHealthChange += HealthChanged;
        healthComponent.BroadcastHealthValueImmeidately();
    }

    private void HealthChanged(float health, float delta, float maxHealth)
    {
        healthBar.UpdateHealth(health,delta,maxHealth);
    }

    void Update()
    {
        PerfromMoveAndAim();

    }

    public void AttackPoint()
    {
        inventoryComponent.GetActiveWeapon().Attack();
    }

    void StartSwitchWeapon()
    {
        animator.SetTrigger("switchWeapon");
    }

    public void SwitchWeapon()  //this method is being accessed in the event system of Weapon_Put_Away as  it check the animator and all components in the player script
    {
        inventoryComponent.NextWeapon();

    }


    private void PerfromMoveAndAim()
    {
        Vector2 moveInput = GetMovementInput(); //left stick
        Vector2 aimInput = GetAimInput(); //right stick
        float weaponSwitchInput = GetWeaponSwitchInput();


        if (aimInput.magnitude > 0)
        {
            animator.SetBool("attacking", true);
        }
        else
        {
            animator.SetBool("attacking", false);
        }


        if (weaponSwitchInput == 1f)   //performs weapon switch for joystick
        {            
                StartSwitchWeapon();           
        }

        Vector3 moveDirection = CalculateMoveDirection(moveInput);  //moveDirection=(rightDir * moveInput.x + upDir * moveInput.y).normalized;

        Vector3 MoveDir = moveDirection * speed * Time.deltaTime; //to move character face direction

        //Debug.Log(moveInput);
        // Move the character using Character Controller
        controller.Move(MoveDir * speed * Time.deltaTime);

        Vector3 AimDir = MoveDir;

        if (aimInput.magnitude != 0)
        {
            AimDir = StickInputToWorldDir(aimInput);
        }

        RotateTowards(AimDir);
        UpdateCamera(moveInput,aimInput);

        //dot product for animation movement
        float forward = Vector3.Dot(MoveDir, transform.forward);
        float right = Vector3.Dot(MoveDir, transform.right);

        animator.SetFloat("ForwardSpeed", forward);
        animator.SetFloat("RightSpeed", right);

    }

    private void UpdateCamera(Vector2 moveInput,Vector2 aimInput)
    {
        //player is moving but not aiming, and cameraController exists
        if (moveInput.magnitude != 0 && aimInput.magnitude==0 && cameraController!=null)  //here we give the value for the x input to change camera rotation
        {
            cameraController.AddYawInput(moveInput.x);
        }
    }

    private void RotateTowards(Vector3 AimDir)
    {
        float currentTurnSpeed = 0f;
        if (AimDir.magnitude != 0)
        {
            Quaternion prevRot = transform.rotation; //for aim rotation blend tree 2 
            float turnLerpAlpha = turnSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(AimDir, Vector3.up), turnLerpAlpha);

            Quaternion currentRot = transform.rotation;  //for aim rotation blend tree 2 
            float Dir = Vector3.Dot(AimDir, transform.right) > 0 ? 1 : -1; //if bigger then  else -1
            float rotationDelta = Quaternion.Angle(prevRot, currentRot) * Dir; //
            currentTurnSpeed = rotationDelta / Time.deltaTime;
        }
        animatorTurnSpeed = Mathf.Lerp(animatorTurnSpeed, currentTurnSpeed, Time.deltaTime * animTurnSpeed);

        animator.SetFloat("TurningSpeed", animatorTurnSpeed);//
    }


    private Vector2 GetMovementInput()
    {
        // Get movement input from PlayerInput component
        return playerInput.actions["PlayerActionMap/Movement"].ReadValue<Vector2>();
    }

    private Vector2 GetAimInput()
    {
        // Get movement input from PlayerInput component
        return playerInput.actions["PlayerActionMap/Aim"].ReadValue<Vector2>();
    }

    private float GetWeaponSwitchInput()
    {
        // Get movement input from PlayerInput component
        return playerInput.actions["PlayerActionMap/WeaponSwitch"].triggered? 1f:0f;
    }

    private Vector3 CalculateMoveDirection(Vector2 moveInput)
    {
        // Calculate movement based on camera's orientation
        Vector3 rightDir = mainCam.transform.right;
        Vector3 upDir = Vector3.Cross(rightDir, Vector3.up);
        Vector3 moveDirection = (rightDir * moveInput.x + upDir * moveInput.y).normalized;

        // Keep the y-component constant
        moveDirection.y = 0f;

        return moveDirection;
    }

    private Vector3 StickInputToWorldDir(Vector2 inputVal)
    {
        Vector3 rightDir = mainCam.transform.right;
        Vector3 upDir = Vector3.Cross(rightDir, Vector3.up);
        Vector3 worldDirection = (rightDir * inputVal.x + upDir * inputVal.y).normalized;

        return worldDirection;
    }
}
