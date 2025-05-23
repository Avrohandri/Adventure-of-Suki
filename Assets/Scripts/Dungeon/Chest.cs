using System;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private Transform itemPos;
    
    [Header("Item")]
    [SerializeField] private bool usePredefinedChest;
    [SerializeField] private GameObject predefinedItem;

    private Animator animator;
    private bool openedChest;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void ShowItem()
    {
        if (usePredefinedChest)
        {
            Instantiate(predefinedItem, transform.position,
                Quaternion.identity, itemPos);
        }
        else // Random Item
        {
            GameObject randomItem = LevelManager.Instance.GetRandomItemForChest();
            Instantiate(randomItem, transform.position, 
                Quaternion.identity, itemPos);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (openedChest) return;
        if (other.CompareTag("Player") == false) return;
        animator.SetTrigger("OpenChest");
        ShowItem();
        openedChest = true;
    }
}
