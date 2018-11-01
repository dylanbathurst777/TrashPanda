using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AI;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

public class Player : SerializedMonoBehaviour, IPointerClickHandler, ISelectHandler, IDeselectHandler
{
    public Animator myAnimator;
    public static HashSet<Player> allMySelectables = new HashSet<Player>();
    public static HashSet<Player> currentlySelected = new HashSet<Player>();

    public float maxHealth = 100;
    [SerializeField]
    public float curretHeath;

    public Image healthBar;

    private DamageText damageText;


    [OdinSerialize]
    private Dictionary<DamageType, float> resistances;

    [SerializeField]
    private DamageType damageType;
    [SerializeField]
    private int damageAmount;

    private Color minionColor;
    public Renderer playerRenderer;
    private float TimeStamp;

    public Color HighLightColor;
    [SerializeField]
    public float RangeDetect = 30f;

    public Color SelectedLightColor;

    public Camera cam;

    public float fireRate = 0.5f;
    private float nextFire = 0.0f;

    public NavMeshAgent agent;
    public GameObject target;

    public enum STATE { idle, Moving, Combat};
    public STATE currentState;

    public bool MovingReady;

    void Awake()
    {
        allMySelectables.Add(this);
        playerRenderer = gameObject.GetComponentInChildren<Renderer>();
        cam = Camera.main;
    }

    private void FixedUpdate()
    {
    
       
    }

        // Use this for initialization
        void Start ()
    {
        curretHeath = maxHealth;
        damageText = GetComponentInChildren<DamageText>();
        minionColor = playerRenderer.material.color;
        ChangeState(STATE.idle);
    }
	
	// Update is called once per frame
	void Update () {
        Movement();

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

            ChangeState(STATE.Combat);
            target = nearestEnemy.gameObject;
            Attack();



        }

     
            
        
}


    public void OnPointerClick(PointerEventData eventData)
    {
        if (!Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.RightControl))
        {
            MovingReady = false;
            DeselectAll(eventData);
        }
        OnSelect(eventData);
    }

    public void OnSelect(BaseEventData eventData)
    {
        
            currentlySelected.Add(this);
            playerRenderer.material.color = SelectedLightColor;
        if (gameObject.GetComponent<PlayerFarmer>())
        {
            
            gameObject.GetComponent<PlayerFarmer>().AddFarmer();
        }

        MovingReady = true;


    }


    public void OnDeselect(BaseEventData eventData)
    {
        playerRenderer.material.color = minionColor;
        MovingReady = false;
        currentState = STATE.idle;
        myAnimator.SetBool("Walk", false);

    }

    public static void DeselectAll(BaseEventData eventData)
    {
        foreach (Player selectable in currentlySelected)
        {
            selectable.OnDeselect(eventData);
        }
        currentlySelected.Clear();
    }

    public void Movement()
    {
        if (MovingReady == true)
        {
            if (Input.GetMouseButtonDown(1))
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    ChangeState(STATE.Moving);
                    agent.SetDestination(hit.point);
                    myAnimator.SetBool("Walk", true);
                }
            }
        }
    }
    public void Attack()
    {
        if (Time.time > nextFire)
        {


            myAnimator.SetTrigger("Attack");
            target.gameObject.GetComponent<Enemy>().TakeDamage(damageAmount, damageType);
            nextFire = Time.time + fireRate;


        }
       


    }

    public void ChangeState(STATE state)
    {
        currentState = state;
        if (currentState == STATE.idle)
        {

           
        }

        if (currentState == STATE.Moving)
        {

            MovingReady = true;
        }
        if (currentState == STATE.Combat)
        {

          // MovingReady = false;
            
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, RangeDetect);
    }

    private void OnTriggerStay(Collider other)
    {
       /* if (other.CompareTag)
        {

        }
        */
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

            allMySelectables.Remove(this);
            currentlySelected.Remove(this);
            Destroy(gameObject);
           
        }
       
        damageText.ShowDamage(modifiedAmount, damageType);
       
    }


}





