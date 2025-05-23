using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerEnergy : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private PlayerConfig playerConfig;

    public bool CanUseEnergy => playerConfig.Energy > 0f;

    public void UseEnergy(float amount)
    {
        playerConfig.Energy -= amount;
        if (playerConfig.Energy < 0)
        {
            playerConfig.Energy = 0;
        }
    }

    public void RecoverEnergy(float amount)
    {
        playerConfig.Energy += amount;
        if (playerConfig.Energy > playerConfig.MaxEnergy)
        {
            playerConfig.Energy = playerConfig.MaxEnergy;
        }
    }
}
