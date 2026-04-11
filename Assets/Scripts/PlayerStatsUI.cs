using TMPro;
using UnityEngine;

public class PlayerStatsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerStatsText;
    private string PlayerHealth = "";


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerStatsText.text = "======================";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
