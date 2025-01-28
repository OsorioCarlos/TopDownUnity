using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Sciptable Objects/GameManager")]
public class GameManagerSO : ScriptableObject
{
    private Player player;
    private InventorySystem inventorySystem;

    public InventorySystem InventorySystem { get => inventorySystem; }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += NewSceneLoaded;
    }

    private void NewSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        player = FindAnyObjectByType<Player>();
        inventorySystem = FindAnyObjectByType<InventorySystem>();
    }

    public void ChangePlayerState(bool state)
    {
        player.Interacting = !state;
    }
}
