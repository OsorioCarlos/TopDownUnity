using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    [SerializeField] private GameObject inventoryBox;
    [SerializeField] private Button[] inventoryButtons;

    private int availableItems = 0;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < inventoryButtons.Length; i++)
        {
            int index = i;
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

    private void ClickedButton(int index)
    {
        Debug.Log("ClickedButton: " + index);
    }

    public void SaveItem(ItemSO itemData)
    {
        inventoryButtons[availableItems].gameObject.SetActive(true);
        inventoryButtons[availableItems].GetComponent<Image>().sprite = itemData.icon;
        availableItems++;
    }
}
