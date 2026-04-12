using UnityEngine;

public class PowerUpCharacteristics : MonoBehaviour
{
    public string PowerUpType;
    public int statBonus;
    private GameObject currentPowerup;
    private SpriteRenderer sr;
    public string spriteTitle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        sr = GetComponent<SpriteRenderer>(); 
        spriteTitle = sr.sprite.name;

        switch (spriteTitle) // get type of powerup based on sprite
        {
            case "gs_plus": // current health 
                statBonus = 342;
                break;
            case "gs_star": // max health 
                statBonus = 10;
                break;
            case "gs_carat": // damage
                statBonus = 2;
                break;
            default:
                statBonus = 4;
                break;
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int getStatBonus() {
        return this.statBonus;
    }
}