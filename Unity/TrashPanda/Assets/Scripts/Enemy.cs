using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

public class Enemy : SerializedMonoBehaviour {

    public float maxHealth = 100;
    public float curretHeath;

    public GameObject CurrentTarget;

    public Image healthBar;
 

    private DamageText damageText;

   public UIM Currency;

   


    [OdinSerialize]
    private Dictionary<DamageType, float> resistances;

    private void Start()
    {
        Currency = FindObjectOfType<UIM>();
    }

    private void OnEnable()
    {
        curretHeath = maxHealth;
        damageText = GetComponentInChildren<DamageText>();
    }

    public void Update()
    {
    
       
    }
    public void TakeDamage(int amount, DamageType damageType)
    {
      
        float resistance = resistances[damageType];
        float resistanceMultiplier = resistance / 100f;
        int modifiedAmount = (int)((float)amount * resistanceMultiplier);
        curretHeath -= modifiedAmount;
        healthBar.fillAmount = curretHeath / maxHealth;
        if (curretHeath <= 0)
        {
            Currency.CurrencyTotal = Currency.CurrencyTotal + 10;
            Destroy(gameObject);
           

        }

        damageText.ShowDamage(modifiedAmount, damageType);
    }
   

}
