using System.Collections;
using UnityEngine;
using UnityEngine.AI;


public class EnemyFollow : MonoBehaviour {

    private NavMeshAgent myAgent;
    public GameObject target;
    public GameObject startingTarget;
    private int index;
    private GameObject[] playerUnits;

    public float RangeDetect = 5f;
    public GameObject dumpster;
    public Animator myAnimator;

    /// <summary>
    /// ///////////////////////////////////
    /// </DamagePlayer>
    [SerializeField]
    private DamageType damageType;
    [SerializeField]
    private int damageAmount;

    public bool buildingDestroyer = true;
    public float fireRate = 0.5f;
    private float nextFire = 0.0f;
    /// <DamagePlayer>
    /// //////////////////////////////////
    /// </summary>
    private void OnEnable()
    {
        if (!buildingDestroyer)
        {
            playerUnits = GameObject.FindGameObjectsWithTag("Player");
        }
        if (buildingDestroyer)
        {
            playerUnits = GameObject.FindGameObjectsWithTag("Building");
        }
        index = Random.Range(0, playerUnits.Length);
        startingTarget = playerUnits[index];
        
    }
    // Use this for initialization
    void Start () {
        myAgent = GetComponent<NavMeshAgent>();

      

    }
	
	// Update is called once per frame
	void Update () {

        

        Player[] playerUnits = GameObject.FindObjectsOfType<Player>();

        Player nearestEnemy = null;
        float dist = RangeDetect;
        foreach (Player p in playerUnits)
        {
            float d = Vector3.Distance(this.transform.position, p.transform.position);
            if (d < dist)
            {
                nearestEnemy = p;
                dist = d;


            }
        }

        if (startingTarget != null && nearestEnemy == null)
        {
            target = startingTarget;
            myAgent.SetDestination(target.transform.position);
            myAnimator.SetBool("Walk", true);
            return;
        }



        if (nearestEnemy == null && startingTarget == null)
        {
           
            target = dumpster.gameObject;
            myAgent.SetDestination(target.transform.position);
            return;

        }

        if (dumpster == null && target != null)
        {
            //gameOver
            target = null;
        }
     


        if (Vector3.Distance(transform.position, nearestEnemy.transform.position) < RangeDetect)
        {

         
            target = nearestEnemy.gameObject;
            Attack();
            myAgent.SetDestination(target.transform.position);
            myAnimator.SetBool("Walk", true);
            return;


        }
        
      

     
    }
    public void Attack()
    {
        if (Time.time > nextFire)
        {


           myAnimator.SetBool("Attack", true);
            target.gameObject.GetComponent<Player>().TakeDamage(damageAmount, damageType);
            nextFire = Time.time + fireRate;


        }



    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, RangeDetect);
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Building"))
        {
            startingTarget = other.gameObject;
            if (Time.time > nextFire)
            {

                target = other.gameObject;

                target.gameObject.GetComponent<BuildingDamage>().TakeDamage(damageAmount, damageType);

                nextFire = Time.time + fireRate;
                myAgent.SetDestination(target.transform.position);
                return;


            }

        }
    }
}
