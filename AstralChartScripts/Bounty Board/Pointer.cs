using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Pointer : MonoBehaviour
{
    private Transform targetPos;
    private Transform player;

    [SerializeField] Sprite arrowImage;
    [SerializeField] Sprite targetImage;

    [SerializeField] RectTransform arrow;
    [SerializeField] RectTransform textTransform;
    [SerializeField] GameObject pointer;
    [SerializeField] GameObject textGO;
    [SerializeField] TMP_Text textDist;

    private float max_height = 450.0f;
    private float max_width = 850.0f;
    private bool currentlyTracking;
    private GameObject target;
    private float distance;
    private bool trackDistanceOnce;

    // EVENT
    public delegate void DialogueLocation();
    public static event DialogueLocation onBeginDialogueLocation;

    public void BeginTracking(GameObject t){ currentlyTracking = true; target = t; targetPos = t.transform; }
    public void StopTracking(){ target = null; currentlyTracking = false; pointer.SetActive(false); textGO.SetActive(false); }

    void Start(){
        player = GameObject.Find("PlayerHolder").transform.GetChild(0);
        pointer.SetActive(false);
        textGO.SetActive(false);
        trackDistanceOnce = false;
    }

    private bool Visible(GameObject toCheck){ return GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(Camera.main), toCheck.gameObject.GetComponentInChildren<Renderer>().bounds); }


    /*
    public Renderer getRenderer(Transform t){
        foreach(Transform child in t){
            if(child.GetComponent<Renderer>() != null) return child.GetComponent<Renderer>();
            else return getRenderer(child);
        }
        return null;
    }*/

    public float GetDistance(){ return distance; }
    

    void Update(){
        if(target == null){
            currentlyTracking = false;
            trackDistanceOnce = true;
            textGO.SetActive(false);
            pointer.SetActive(false);
        }

        if(currentlyTracking){
            textGO.SetActive(true);
            pointer.SetActive(true);

            Vector3 dir = (targetPos.position - player.position).normalized;
            float angle = (Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg) % 360;
            distance = Vector3.Distance(player.position, targetPos.position);

            // send event call to event dialogue holder 
            if(trackDistanceOnce && distance > 600.0f){
                trackDistanceOnce = false;
                onBeginDialogueLocation();
            }

            if(angle < 0){ angle += 360.0f; }
            arrow.localEulerAngles = new Vector3(180.0f, 0.0f, angle);

            // if the object is not on screen then the pointer will tell the player where the object is and how far it is
            Vector3 pointOnScreen = Camera.main.WorldToScreenPoint(target.gameObject.GetComponentInChildren<Renderer>().bounds.center);
            if(Visible(target.gameObject)){
                pointer.GetComponent<Image>().sprite = targetImage;
                textDist.text = Mathf.Floor(distance) + "";
                textTransform.position = pointOnScreen;
                arrow.position = pointOnScreen;
                textTransform.localPosition = new Vector3(textTransform.localPosition.x, textTransform.localPosition.y, textTransform.localPosition.z);
                arrow.localPosition = new Vector3(arrow.localPosition.x, arrow.localPosition.y, arrow.localPosition.z);

            } else {
                pointer.GetComponent<Image>().sprite = arrowImage;
                
                float height = max_height;
                float width = max_width;

                if(angle >= 45.0f && angle <= 135.0f)
                    height = max_height/Mathf.Tan(arrow.localEulerAngles.z * Mathf.Deg2Rad);
                if(angle <= 315.0f && angle >= 225.0f)
                    height = -max_height/Mathf.Tan(arrow.localEulerAngles.z * Mathf.Deg2Rad);
                if(angle <= 45.0f || angle >= 315.0f)
                    width = Mathf.Sin(arrow.localEulerAngles.z * Mathf.Deg2Rad * 2) * max_width;
                if(angle >= 135.0f && angle <= 225.0f)
                    width = -Mathf.Sin(arrow.localEulerAngles.z * Mathf.Deg2Rad * 2) * max_width;
                if(angle <= 315.0f && angle >= 225.0f)
                    width = -max_width;
                if(angle >= 135.0f && angle <= 225.0f)
                    height = -max_height;
                
                textDist.text = Mathf.Floor(distance) + "";
                arrow.localPosition = new Vector3(width, height, 0f);
                textTransform.localPosition = new Vector3(width, height, 0f);
            }
        }
    }
}
