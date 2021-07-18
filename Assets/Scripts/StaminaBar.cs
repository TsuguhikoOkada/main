using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StaminaBar : MonoBehaviour
{
    public Slider staminaBar;
    
    private int maxStamina = 100;
    private float currentStamina;

    private WaitForSeconds reginTick = new WaitForSeconds(0.1f);
    private Coroutine regen;

    public static StaminaBar instance;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        currentStamina = maxStamina;
        staminaBar.maxValue = maxStamina;
        staminaBar.value = maxStamina;
    }


    public void UseStamina(float amount)
    {
        if (currentStamina - amount >= 0)
        {
            currentStamina -= amount;
            staminaBar.value = currentStamina;

            if (regen != null)
            {
                StopCoroutine(regen);
            }

           regen = StartCoroutine(RegenStamina());
        }
        else
        {
            Debug.Log("Not enough stamina");
        }

         IEnumerator RegenStamina()
        {
            yield return new WaitForSeconds(2);

            while (currentStamina < maxStamina)
            {
                currentStamina += maxStamina / 100;
                staminaBar.value = currentStamina;
                yield return reginTick;
            }
            regen = null;
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
