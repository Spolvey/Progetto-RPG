using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed;
    public float gravity = -9.81f;
    public float jumpForce;
    public Vector3 playerVelocity;

    //Connected gameObjects declaration TODO: GET REFERENCE IN START
    PlayerInput playerInputComponent;
    CharacterController playerController;
    GameObject playerCamera;

    void Start()
    {
        playerInputComponent = gameObject.GetComponent<PlayerInput>();
        playerController = gameObject.GetComponent<CharacterController>();
        playerCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    #region InputSystem Enable/Disable
    //Enabling and disabling the new Input System's actions 
    private void OnEnable()
    {
        playerInputComponent = gameObject.GetComponent<PlayerInput>();
        playerInputComponent.actions.Enable();
    }

    private void OnDisable()
    {
        playerInputComponent.actions.Disable();
    }
#endregion 

    void Update()
    {
        if (playerController.isGrounded) 
        {
            //Make the player jump if the action is called
            if (playerInputComponent.actions.FindAction("Jump").IsPressed())
            {
                playerVelocity.y += Mathf.Sqrt(jumpForce * -3.0f * gravity);
            }
        }

        if (PlayerStatus.canPlayerMove)
        {
            //Modify velocity according to input
            playerVelocity = new Vector3(
                playerInputComponent.actions.FindAction("Movement").ReadValue<Vector2>().x * playerSpeed,
                0f,
                playerInputComponent.actions.FindAction("Movement").ReadValue<Vector2>().y * playerSpeed);
            
            playerVelocity = transform.TransformDirection(playerVelocity);

            //Rotate character towards where camera is looking when movement input is given
            if (playerVelocity != Vector3.zero)
            {
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,
                    playerCamera.transform.rotation.eulerAngles.y,
                    transform.rotation.eulerAngles.z);
            }
        }
        
        //apply gravity to velocity
        playerVelocity.y += gravity * Time.deltaTime;

        //move the player according to playerVelocity
        playerController.Move(playerVelocity * Time.deltaTime); 
    }
}
