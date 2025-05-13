using System;
using UnityEngine;

public class SelectablePlayer : MonoBehaviour
{
    [SerializeField] private PlayerConfig config;
    public PlayerConfig Config => config;

    private void OnMouseDown()
    {
        MenuManager.Instance.ClickPlayer(this);
    }
}
