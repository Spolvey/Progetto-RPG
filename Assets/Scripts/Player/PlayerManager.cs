using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{  
	public PlayerStatus playerStatus;
	public PlayerMovement playerMovement;

    void Start()
    {
        //Locking the mouse to the game once it loads
        Cursor.lockState = CursorLockMode.Locked;

		playerStatus = gameObject.GetComponent<PlayerStatus>();
		playerMovement = gameObject.GetComponent<PlayerMovement>();
        
    } 
}
