using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
    //private GameObject shopUI;
    //[SerializeField] GameObject cam;
    private GameObject player;
    //[SerializeField] Transform cameraLocation;
    [SerializeField] private float delay = 5.0f;


    //private bool inShop;
    //private bool inTrigger;
    //private Transform previousLocation;
    private GameObject upgradeShip;
    private UIManager uiManager;

    private bool inRange;
    private bool playerHasEnteredShop;
    private bool delayTrigger;
    [SerializeField] ShopManager shopManager;

    [Space]

    [Tooltip("Tutorial popup that closes when you enter the shop.")]
    [SerializeField] ShopUIPopup triggerPopup;

    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.Find("PlayerHolder").transform.GetChild(0).gameObject;
        //this.shopUI = GameObject.Find("Upgrade Station UI");
        this.uiManager = GameObject.Find("UI Manager").GetComponent<UIManager>();
        upgradeShip = GameObject.Find("PlayerHolder").gameObject;

        inRange = false;
        playerHasEnteredShop = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B) && inRange)
            ButtonPressed();

        if (Input.GetKeyDown(KeyCode.Escape) && playerHasEnteredShop)
            CloseShop();
    }

    public void ButtonPressed()
    {
        if (!playerHasEnteredShop)
            OpenShop();
        else
            CloseShop();
    }

    public void OpenShop() 
    {
        //Cursor.visible = true;
        playerHasEnteredShop = true;

        shopManager.Open();
        //shopManager.trigger = this;

        //player.GetComponent<ShipMovement>().StopAllMovement();
        //player.GetComponent<ShipMovement>().stopMovement = true;
        //player.GetComponent<ShipMovement>().QuitMoveMode();
        //cam.GetComponent<CameraController>().LockPlayer();
	}

    public void CloseShop() 
    {
        //Cursor.visible = false;

        shopManager.Close();
        //shopManager.trigger = null;
        //player.GetComponent<ShipMovement>().stopMovement = false;
        //player.GetComponent<ShipMovement>().QuitMoveMode();
        //cam.GetComponent<CameraController>().UnlockPlayer();
        playerHasEnteredShop = false;
        //cam.GetComponent<CameraController>().zoomTo(previousLocation);
	}

    bool canEnterShop = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = true;

            //if (triggerPopup != null)
            //    triggerPopup.ButtonPressed();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
        }
    }

    IEnumerator Delay(){
        delayTrigger = true;
        yield return new WaitForSeconds(delay);
        delayTrigger = false;
    }

    //private void OnTriggerStay(Collider other){
    //    if(other.tag == "Player" && Input.GetKey(KeyCode.B) && !inShop){
    //        shopUI.SetActive(true);
    //        inShop = true;
    //        player.GetComponent<ShipMovement>().StopAllMovement();
    //        player.GetComponent<ShipMovement>().stopMovement = true;
    //        player.GetComponent<ShipMovement>().QuitMoveMode();
    //        cam.GetComponent<CameraController>().LockPlayer();
    //        previousLocation = cam.GetComponent<CameraController>().getCameraTransform();
    //        //cam.GetComponent<CameraController>().zoomTo(cameraLocation);
    //        mr.enabled = false;
    //    }
        // exit
        /*
        if(other.tag == "Player" && Input.GetKeyDown(KeyCode.B) && other.GetComponent<ShipMovement>().stopMovement == true){
            shopM.SetActive(false);
            other.GetComponent<ShipMovement>().stopMovement = false;
            cam.GetComponent<CameraController>().UnlockPlayer();
        }*/
    //}
    /*
    private void OnTriggerExit(Collider other){
        if(other.tag == "Player"){
            shopM.SetActive(false);
            other.GetComponent<ShipMovement>().stopMovement = false;
            cam.GetComponent<CameraController>().UnlockPlayer();
        }
    }*/
}
