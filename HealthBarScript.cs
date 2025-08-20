using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{

    private Image healthBar;

    public static float curHealth;
    public static float maxHealth;
    void Start()
    {
        maxHealth = 100;
        curHealth = maxHealth;
        healthBar = gameObject.GetComponent<Image>();
    }

    void Update()
    {
        healthBar.fillAmount = curHealth / maxHealth;

        if (Input.GetKeyDown(KeyCode.L))
        {
            curHealth -= 10;
        }   
    }
}
