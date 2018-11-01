using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

public class BuildingDamage : SerializedMonoBehaviour 
{

    public float maxHealth = 100;
    public float curretHeath;

    [SerializeField]
    private int damageAmount;
    public GameObject target;
    [SerializeField]
    private bool ElementalDamage;

    public float fireRate = 0.5f;
    private float nextFire = 0.0f;
    [SerializeField]
    public float RangeDetect = 30f;

    public Image healthBar;
    public DamageText damageText;

    [SerializeField]
    private DamageType damageType;
    [OdinSerialize]
    private Dictionary<DamageType, float> resistances;

    // Use this for initialization
    void Start () {
        curretHeath = maxHealth;
        damageText = GetComponentInChildren<DamageText>();
    }
	
	// Update is called once per frame
	void Update () {
        if (ElementalDamage == true)
        {
            Enemy[] enemies = GameObject.FindObjectsOfType<Enemy>();

            Enemy nearestEnemy = null;
            float dist = RangeDetect;
            foreach (Enemy e in enemies)
            {
                float d = Vector3.Distance(this.transform.position, e.transform.position);
                if (d < dist)
                {
                    nearestEnemy = e;
                    dist = d;


                }
            }
            if (nearestEnemy == null)
            {


                return;
            }


            if (Vector3.Distance(transform.position, nearestEnemy.transform.position) < RangeDetect)
            {

              
                target = nearestEnemy.gameObject;
                Attack();



            }
        }
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
          
            Destroy(gameObject);


        }

        damageText.ShowDamage(modifiedAmount, damageType);
    }
    public void Attack()
    {
        if (Time.time > nextFire)
        {



            target.gameObject.GetComponent<Enemy>().TakeDamage(damageAmount, damageType);
            nextFire = Time.time + fireRate;


        }



    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, RangeDetect);
    }
}
