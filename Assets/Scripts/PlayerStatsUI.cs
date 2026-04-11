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

    void OnEnable()
    {
        PlayerStatHandler.OnHealthChanged += updateHealthBar;
    }

    void OnDisable()
    {
        PlayerStatHandler.OnHealthChanged -= updateHealthBar;
    }


    public char[] calculateNewHealthString(int newHealth, int maxHealth)
    {
        int healthClamp = Mathf.Clamp(newHealth, 0, maxHealth);
        char[] display = new char[maxHealth];
        for (int i = 0; i < maxHealth; i++)
        {
            if (i < healthClamp)
            {
                display[i] = '='; //health value
            } else
            {
                display[i] = '-'; //empty value/lost health
            }
        }
        return display;
    }

    public void updateHealthBar(int newHealth, int maxHealth)
    {
        PlayerHealth = new string(calculateNewHealthString(newHealth, maxHealth));
        playerStatsText.text = PlayerHealth;
    }
}
