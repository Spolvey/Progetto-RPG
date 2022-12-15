using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    //Status variables declaration
    public float playerHP = 100f;
    public float maxPlayerHP = 100f;
    static public bool canPlayerMove = true;

    //Connected UI gameObjects declaration
    public Image playerHealthBar;

    void Start()
    {
        playerHealthBar = GameObject.Find("HealthBar").GetComponent<Image>();
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
    }

    public void ChangeHealthPoints(float amount)
    {
        playerHP += amount;
    }

    public void KillPlayer()
    {
        canPlayerMove = false;
        playerHP = 0f;
    }
    
}
