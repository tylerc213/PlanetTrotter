using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlotUI : MonoBehaviour
{

    public int slotIndex;

    public Image itemIcon;
    public TextMeshProUGUI itemLabel;

    private InventorySystem inventorySystem;
    private InventoryUI inventoryUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize(InventorySystem system, InventoryUI ui)
    {
        inventorySystem = system;
        inventoryUI = ui;

        RefreshDisplay();
    }

    public void RefreshDisplay()
    {
        InventoryItem item = inventorySystem.GetItem(slotIndex);

        if (item == null)
        {
            itemIcon.enabled = false;
            itemLabel.text = "";
            return;
        }

        itemIcon.enabled = true;

        switch (item.itemType)
        {
            case InventoryItemType.Resource1:
                itemIcon.color = new Color(0.55f, 0.45f, 0.3f);
                itemLabel.text = "R1";
                break;

            case InventoryItemType.Resource2:
                itemIcon.color = new Color(0.35f, 0.4f, 0.5f);
                itemLabel.text = "R2";
                break;

            case InventoryItemType.Ice:
                itemIcon.color = new Color(0.4f, 0.8f, 1f);
                itemLabel.text = "ICE";
                break;
        }
    }

    public void OnSlotClicked()
    {
        inventoryUI.SelectSlot(slotIndex);
    }
}
