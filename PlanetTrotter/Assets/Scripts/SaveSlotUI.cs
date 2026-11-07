
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlotUI : MonoBehaviour
{
    public enum SlotMode
    {
        Save,
        Load
    }

    public SlotMode mode;

    public Button[] slotButtons;
    public TMP_Text[] slotLabels;
    public TMP_Text statusText;

    void Start()
    {
        for (int i = 0; i < slotButtons.Length; i++)
        {
            int slotNumber = i + 1;

            slotButtons[i].onClick.AddListener(
                () => SelectSlot(slotNumber)
            );
        }

        RefreshSlots();
    }

    void OnEnable()
    {
        RefreshSlots();
    }

    public void RefreshSlots()
    {
        for (int i = 0; i < slotButtons.Length; i++)
        {
            SaveData data = SaveFileSystem.Load(i + 1);

            if (data == null)
            {
                slotLabels[i].text =
                    "Slot " + (i + 1) + " - Empty";
            }
            else
            {
                slotLabels[i].text =
                    "Slot " + (i + 1) +
                    "\nSaved: " + data.savedAt +
                    "\nOxygen: " + data.oxygen.ToString("F1");
            }

            if (mode == SlotMode.Load)
            {
                slotButtons[i].interactable = data != null;
            }
            else
            {
                slotButtons[i].interactable = true;
            }
        }
    }

    public void SelectSlot(int slot)
    {
        if (PlayerStateManager.Instance == null)
        {
            Debug.LogError("PlayerStateManager not found!");
            return;
        }

        if (mode == SlotMode.Save)
        {
            bool success =
                PlayerStateManager.Instance.SaveSlot(slot);

            if (statusText != null)
            {
                statusText.text = success
                    ? "Game saved successfully!"
                    : "Unable to save game.";
            }

            RefreshSlots();
        }
        else
        {
            bool success =
                PlayerStateManager.Instance.LoadSlot(slot);

            if (!success && statusText != null)
            {
                statusText.text = "Unable to load save.";
            }
        }
    }
}
