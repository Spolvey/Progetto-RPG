using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPChangeAreaTest : MonoBehaviour
{
    public float playerHPChange;

    private float actionTime = 0.0f;
    private readonly float period = 0.5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerManager>())
            actionTime = Time.time;
    }
    private void OnTriggerStay(Collider other)
    {
        PlayerManager playerScript = other.GetComponent<PlayerManager>();

        if (Time.time > actionTime)
        {
            actionTime += period;
            if (playerScript)
                playerScript.playerStatus.ChangeHealthPoints(playerHPChange);
        }

    }
}
