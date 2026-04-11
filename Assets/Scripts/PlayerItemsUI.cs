using TMPro;
using UnityEngine;

public class PlayerItemsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerItemText;
    private string ItemsList = "";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerItemText.text = "empty";
    }

    void OnEnable()
    {
        PowerUpHandler.OnItemCollected += updateItemList;
    }

    void OnDisable()
    {
        PowerUpHandler.OnItemCollected -= updateItemList;
    }

    public char[] CalculateNewItemString(char newItem)
    {
        char[] display = new char[20];
        if (playerItemText.text == "empty")
        {
            display[0] = newItem;
        } else
        {
            for (int i = 0; i < 20; i++)
            {
                if (display[i] == '-')
                {
                    display[i] = newItem;
                    break;
                }
            }
        }
        return display;
    }

    private void updateItemList(char newItem)
    {
        ItemsList = new string(CalculateNewItemString(newItem));
        playerItemText.text = ItemsList;
    }
}
