using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, Interactable
{
    [SerializeField] private ItemSO myData;
    [SerializeField] private GameManagerSO gameManager;

    public void Interact()
    {
        gameManager.InventorySystem.SaveItem(myData);
        Destroy(gameObject);
    }
}
