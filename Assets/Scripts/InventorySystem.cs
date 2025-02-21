using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    [SerializeField] private GameObject inventoryBox;
    [SerializeField] private Button[] inventoryButtons;
    private List<ItemSO> inventoryData = new List<ItemSO>();

    private int availableItems = 0;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        for (int i = 0; i < inventoryButtons.Length; i++)
        {
            int index = i;
            Debug.Log(index);
            inventoryButtons[i].onClick.AddListener(() => ClickedButton(index));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryBox.SetActive(!inventoryBox.activeSelf);
        }
    }

    public void ClickedButton(int index)
    {
        Debug.Log("ClickedButton: " + index);
        if (inventoryData[index].healAmount > 0) {
            SistemaVidas sistemaVidas = player.GetComponent<SistemaVidas>();
            if (!sistemaVidas.isFullLife()) {
                player.GetComponent<SistemaVidas>().CurarDanho(inventoryData[index].healAmount);
                RemoveFromButtons(index);
                inventoryData.Remove(inventoryData[index]);
                availableItems--;
            }
        }
        else if (inventoryData[index].isPlaceable) {
            GameObject item = Instantiate(inventoryData[index].placeableItem, player.GetComponent<Player>().GetInteractionPoint(), Quaternion.identity);
            if (item.TryGetComponent(out Bomb bomb))
            {
                bomb.TriggerBomb();
            }
            RemoveFromButtons(index);
            inventoryData.Remove(inventoryData[index]);
            availableItems--;
        }
        else if (inventoryData[index].raiseMaxHealthAmount > 0) {
            SistemaVidas sistemaVidas = player.GetComponent<SistemaVidas>();
            sistemaVidas.RaiseMaxHealth(inventoryData[index].raiseMaxHealthAmount);
            RemoveFromButtons(index);
            inventoryData.Remove(inventoryData[index]);
            availableItems--;
        }
        else if (inventoryData[index].isMainWeapon) {
            player.GetComponent<Player>().SetMainWeapon(inventoryData[index].damage);
        }
    }

    private void RemoveFromButtons(int index) {
        for (int i = index; i < inventoryButtons.Length; i++)
        {
            if (inventoryButtons.Length>1 && i+1 < inventoryButtons.Length && inventoryButtons[i+1].IsActive()) {
                inventoryButtons[i].GetComponent<Image>().sprite = inventoryButtons[i+1].GetComponent<Image>().sprite;
            } 
            else {
                inventoryButtons[i].gameObject.SetActive(false);
                inventoryButtons[i].GetComponent<Image>().sprite = null;
            }
        }
    }

    public void SaveItem(ItemSO itemData)
    {
        inventoryButtons[availableItems].gameObject.SetActive(true);
        inventoryButtons[availableItems].GetComponent<Image>().sprite = itemData.icon;
        inventoryData.Add(itemData);
        availableItems++;
    }
}
