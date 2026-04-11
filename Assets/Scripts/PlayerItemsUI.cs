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

    // Update is called once per frame
    void Update()
    {
        
    }
}
