using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UIM : MonoBehaviour {

    public GoogleAnalyticsV4 googleAnalytics;
    [SerializeField]
    public GameObject CurrentselectedWall;
    [SerializeField]
    public GameObject UpgradewallCurrentselectedWall;

    [SerializeField]
    public GameObject sellingObject;

    public GameObject wallLevel2;
    public GameObject wallLevel3;

    [SerializeField]
    public int CurrentWallLevel;


    public Image deactivatedHouse;
    public Image deactivatedWall;
    public Image deactivatedGate;
    public Image deactivatedRangeUnit;
    public Image deactivatedMeleeUnit;
    public Image deactivatedFarmUnit;
    public Image deactivatedWallUpgrade;

    public Image sunImage;
    public Image moonImage;

    public bool GatheringMaster;

    public Button activatedHouse;
    public Button activatedWall;
    public Button activatedGate;
    public Button activatedRangeUnit;
    public Button activatedMeleeUnit;
    public Button activatedFarmUnit;
    public Button activatedWallUpgrade;

    public Button SendFarmersToGather;
    public Button StopFarmersToGather;

    public Button sellingButton;


    public Text CurrencyText;
    public int CurrencyTotal;
    public int HouseCost = -20;
    public int WallCost = -5;
    public int GateCost = -50;
    public int RangeUnit = -15;
    public int MeleeUnit = -25;
    public int FarmerUnit = -5;
    //WallUpgrade Costs
    public int Level1Cost = -10;
    public int Level2Cost = -20;
    //wall selling
    public int wallLevel1sell = 5;
    public int wallLevel2sell = 15;
    public int wallLevel3sell = 20;
    public int Housesell = 10;
    public int Gatesell = 25;

    public int NumberofHouses;
    public int maxUnits;
    public int CurrentUnits;


    public PlayerFarmer playerfarm;




    // Main Menu


    public void Awake()
    {
        googleAnalytics = FindObjectOfType<GoogleAnalyticsV4>();
    }
        public void Start()
    {


        maxUnits = 0;

        googleAnalytics.GetComponent<GoogleAnalyticsV4>().LogEvent("LocationViewing", "TheMainMenu", "Viewing", 1);


        Player[] playerUnits = GameObject.FindObjectsOfType<Player>();


        foreach (Player p in playerUnits)
        {
            CurrentUnits = CurrentUnits + 1;
        }
      









    }
    void LateUpdate()
    {
        
    }
    void FixedUpdate()
    {
       

        CurrencyUpdate();

        if(UpgradewallCurrentselectedWall == null)
        {
            activatedWallUpgrade.gameObject.SetActive(false);
            deactivatedWallUpgrade.gameObject.SetActive(true);
            CurrentWallLevel = 0;
        }


        if (CurrentWallLevel == 1 && CurrencyTotal >= 10)
        {
          
            activatedWallUpgrade.gameObject.SetActive(true);
            deactivatedWallUpgrade.gameObject.SetActive(false);

        }
        else
        {
            activatedWallUpgrade.gameObject.SetActive(false);
            deactivatedWallUpgrade.gameObject.SetActive(true);
        }
       
        if (CurrentWallLevel == 2 && CurrencyTotal >= 20)
        {
          
            activatedWallUpgrade.gameObject.SetActive(true);
            deactivatedWallUpgrade.gameObject.SetActive(false);
        }
     
        if (CurrentWallLevel == 3)
        {
            activatedWallUpgrade.gameObject.SetActive(false);
            deactivatedWallUpgrade.gameObject.SetActive(true);
        }
        if (CurrentWallLevel == 0)
        {
            activatedWallUpgrade.gameObject.SetActive(false);
            deactivatedWallUpgrade.gameObject.SetActive(true);
        }


    }
    
       
    
    public void CurrencyUpdate()
    {
        if (CurrentUnits < maxUnits)
        {
            



            CurrencyText.text = "Scrap:" + CurrencyTotal.ToString();
            if (CurrencyTotal < 50 && activatedGate)
            {
                activatedGate.gameObject.SetActive(false);
                deactivatedGate.gameObject.SetActive(true);
            }
            else
            {
                activatedGate.gameObject.SetActive(true);
                deactivatedGate.gameObject.SetActive(false);
            }
            if (CurrencyTotal < 25 || CurrentUnits >= maxUnits)
            {
                activatedMeleeUnit.gameObject.SetActive(false);
                deactivatedMeleeUnit.gameObject.SetActive(true);
            }
            else
            {
                activatedMeleeUnit.gameObject.SetActive(true);
                deactivatedMeleeUnit.gameObject.SetActive(false);
            }
            if (CurrencyTotal < 20 && activatedHouse)
            {
                activatedHouse.gameObject.SetActive(false);
                deactivatedHouse.gameObject.SetActive(true);
            }
            else
            {
                activatedHouse.gameObject.SetActive(true);
                deactivatedHouse.gameObject.SetActive(false);
            }
            if (CurrencyTotal < 15 || CurrentUnits >= maxUnits)
            {
                activatedRangeUnit.gameObject.SetActive(false);
                deactivatedRangeUnit.gameObject.SetActive(true);
            }
            else
            {

                activatedRangeUnit.gameObject.SetActive(true);
                deactivatedRangeUnit.gameObject.SetActive(false);
            }
            if (CurrencyTotal < 5 || CurrentUnits >= maxUnits)
            {
                activatedFarmUnit.gameObject.SetActive(false);
                deactivatedFarmUnit.gameObject.SetActive(true);
            }
            else
            {
                activatedFarmUnit.gameObject.SetActive(true);
                deactivatedFarmUnit.gameObject.SetActive(false);
            }

            if (CurrencyTotal < 5 && activatedWall)
            {
                activatedWall.gameObject.SetActive(false);
                deactivatedWall.gameObject.SetActive(true);
               
            }
            else
            {
               
                activatedWall.gameObject.SetActive(true);
                deactivatedWall.gameObject.SetActive(false);
            }

            if (CurrencyTotal <= 999)
            {
                CurrencyText.text = "Scrap:0" + CurrencyTotal.ToString();
            }
            if (CurrencyTotal <= 99)
            {
                CurrencyText.text = "Scrap:00" + CurrencyTotal.ToString();
            }
            if (CurrencyTotal <= 9)
            {
                CurrencyText.text = "Scrap:000" + CurrencyTotal.ToString();
            }
        }
        if(CurrentUnits > maxUnits)
        {
            activatedRangeUnit.gameObject.SetActive(false);
            activatedFarmUnit.gameObject.SetActive(false);
            activatedMeleeUnit.gameObject.SetActive(false);
        }
    }
    public void GatheringIsActive()
    {
        GatheringMaster = true;
    }
    public void GatheringIsNotActive()
    {
        GatheringMaster = false;
    }

    public void PlayGame()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
       
        googleAnalytics.LogEvent("Load", "Level_1", "Playing", 1);

     
     

         // Builder Hit with all Timing parameters.
        googleAnalytics.GetComponent<GoogleAnalyticsV4>().LogTiming(new TimingHitBuilder()
            .SetTimingCategory("Loading")
            .SetTimingInterval(50L)
            .SetTimingName("Level_1")
            .SetTimingLabel("First load")); 
    }

    public void QuitGame()
    {
      /*  googleAnalytics.LogEvent("LocationViewing", "HasViewed", "Quit", 1);
        googleAnalytics.LogEvent(new EventHitBuilder()
     .SetEventCategory("LocationViewing")
     .SetEventAction("HasViewed")
    .SetEventLabel("QuitGame")
     .SetEventValue(1));*/
        Debug.Log("Quit");
        googleAnalytics.GetComponent<GoogleAnalyticsV4>().LogEvent("QuitGame", "QuitPressed", "Quit", 1);
        Application.Quit();
      
    }

    // Sound

    public AudioMixer audioMixer;

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    // Graphics

    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    // Fullscreen

    public void SetFullscreen (bool isFullscreen)
    {
        if (isFullscreen)
        {
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }
    }

    // Restart Menu

    public GameObject RestartMenu;
    
    public void Restart()

    {

        SceneManager.LoadScene("Level_1");

    }

    // Pause Menu

    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    void Update()
    {
       

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        // Restart Menu if:
       

        /*if (Player1.Playerdeath || Player2.Playerdeath == true)
        {
            RestartMenu.SetActive(true);
        }
        else
        {
            RestartMenu.SetActive(false);
        }*/

    }

    public void Resume ()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level_1");
    }

    public void QuitGamePause()
    {

        SceneManager.LoadScene("UI");
       // Application.Quit();
    }

    public void UpgradingWall()
    {
        CurrentselectedWall = UpgradewallCurrentselectedWall;
       CurrentselectedWall.gameObject.GetComponent<UpgradeWall>().CurrentLevel = CurrentWallLevel;
      

        if (CurrentWallLevel == 1 && CurrencyTotal >= 10)
        {
            CurrencyTotal += Level1Cost;
            Instantiate(wallLevel2, CurrentselectedWall.transform.position, CurrentselectedWall.transform.rotation);
            Destroy(UpgradewallCurrentselectedWall);

        }
       
        if (CurrentWallLevel == 2 && CurrencyTotal >= 20)
        {
            CurrencyTotal += Level2Cost;
            Instantiate(wallLevel3, CurrentselectedWall.transform.position, CurrentselectedWall.transform.rotation);
            Destroy(UpgradewallCurrentselectedWall);
        }

        
     
       
       
    }
    public void GameOver()
    {
        SceneManager.LoadScene("OtherSceneName", LoadSceneMode.Additive);
    }

    public void Selling()
    {
      
      


        if (sellingObject.GetComponent<SellObjects>().houseOb == true)
        {
            CurrencyTotal += Housesell;
            Destroy(sellingObject);

        }
        if (sellingObject.GetComponent<SellObjects>().gateOb == true)
        {
            CurrencyTotal += Gatesell;
            Destroy(sellingObject);

        }
        if (sellingObject.GetComponent<SellObjects>().wallOb1 == true)
        {
            CurrencyTotal += wallLevel1sell;
            Destroy(sellingObject);

        }
        if (sellingObject.GetComponent<SellObjects>().wallOb2 == true)
        {
            CurrencyTotal += wallLevel2sell;
            Destroy(sellingObject);

        }
        if (sellingObject.GetComponent<SellObjects>().wallOb3 == true)
        {
            CurrencyTotal += wallLevel3sell;
            Destroy(sellingObject);

        }




    }
 


}
