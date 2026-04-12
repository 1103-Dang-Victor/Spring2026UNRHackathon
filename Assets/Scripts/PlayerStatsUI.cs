using TMPro;
using UnityEngine;

public class PlayerStatsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerStatsText;
    private string PlayerHealth = "";
    private const int BAR_LENGTH = 22;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerStatsText.text = "++++++++++++++++++++++";
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
        char[] display = new char[BAR_LENGTH];

        float percent = (float)newHealth / maxHealth;
        int filledAmount = Mathf.Clamp(Mathf.RoundToInt(percent * BAR_LENGTH), 0, BAR_LENGTH);

        for (int i = 0; i < BAR_LENGTH; i++)
        {
            display[i] = (i < filledAmount) ? '+' : '-';
        }

        return display;
    }

    public void updateHealthBar(int newHealth, int maxHealth)
    {
        PlayerHealth = new string(calculateNewHealthString(newHealth, maxHealth));
        playerStatsText.text = PlayerHealth;
    }
}
