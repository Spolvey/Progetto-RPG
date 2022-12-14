using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using UnityEngine.UI;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    //Public variables declaration
    public float playerSpeed;
    public float gravity;
    public float jumpForce;

    
    //Status variables declaration
    public float playerHP = 100f;
    public float maxPlayerHP = 100f;
    public bool canPlayerMove = true;

    //Connected UI gameObjects declaration
    public Image playerHealthBar;
    //Connected gameObjects declaration
    public PlayerInput playerInputComponent;
    public CharacterController playerController;
    public GameObject playerCamera;
    
    //Private variables declaration
    private Vector3 playerVelocity;

    //Enabling and disabling the new Input System's actions 
    private void OnEnable()
    {
        playerInputComponent.actions.Enable();
    }

    private void OnDisable()
    {
        playerInputComponent.actions.Disable();
    }
   
    void Start()
    {
        //Locking the mouse to the game once it loads
        Cursor.lockState = CursorLockMode.Locked;
    }
   
    private void Update()
    {
        //Rounding the player's HP and writing them on the Text Object
        playerHP = Mathf.Round(playerHP);

        //Update HealthBar and "clamp" hp to max or KillPlayer() at 0 hp
        switch (playerHP)
        {
            default:
                playerHealthBar.fillAmount = playerHP/maxPlayerHP;
                break;

            case var _ when playerHP > maxPlayerHP:
                playerHP = maxPlayerHP;
                playerHealthBar.fillAmount = playerHP / maxPlayerHP;
                break;

            case var _ when playerHP <= 0:
                playerHealthBar.fillAmount = playerHP / maxPlayerHP;
                KillPlayer();
                break;
        }

        playerHealthBar.fillAmount = playerHP / maxPlayerHP;
        if (playerHP > maxPlayerHP)
            playerHP = maxPlayerHP;
        else if (playerHP <= 0f)
            KillPlayer();

        if (playerController.isGrounded) 
        {
            //Make the player jump if the action is called
            if (playerInputComponent.actions.FindAction("Jump").IsPressed())
            {
                playerVelocity.y += Mathf.Sqrt(jumpForce * -3.0f * gravity);
            }
        }

        if (canPlayerMove)
        {
            //Modify velocity according to input
            playerVelocity = new Vector3(
                playerInputComponent.actions.FindAction("Movement").ReadValue<Vector2>().x * playerSpeed,
                playerVelocity.y,
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

    public void ChangeHealthPoints(float amount)
    {
        playerHP += amount;
    }

    public void KillPlayer()
    {
        canPlayerMove = false;
        playerVelocity.x = 0; playerVelocity.z = 0;
    }
}
