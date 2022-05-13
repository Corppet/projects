using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissileLauncherWeaponUI : MonoBehaviour
{
    [SerializeField] GameObject rangeUI;
    [SerializeField] GameObject targetUI;

    private MissilePodController missiles;
    private Transform playerPos;
    private bool cooldown;

    public float rangeDistance;

    void Awake(){
        //rangeUI.SetActive(false);
        //targetUI.SetActive(false);
        rangeUI.SetActive(true);
        targetUI.SetActive(true);
        playerPos = GameObject.Find("PlayerHolder").transform.GetChild(0);
    }

    public void SetMissilePods(MissilePodController m){ missiles = m; }

    void Update(){
        Activate();

        rangeUI.transform.position = playerPos.position;

        Plane temp = new Plane(Vector3.up, Vector3.zero);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float entry;
        if(temp.Raycast(ray, out entry)){
            targetUI.transform.position = ray.GetPoint(entry);
        }

        //float distance = Vector3.Distance(targetUI.transform.position, transform.position);
        //distance = Mathf.Min(distance, rangeDistance);
    }

    void Activate(){
        /*
        if(Input.GetKeyDown(KeyCode.Keypad2) && !cooldown){
            rangeUI.SetActive(true);
            targetUI.SetActive(true);
        }*/
        //rangeUI.activeSelf
        if(Input.GetMouseButtonDown(0)){
            cooldown = true;
            CooldownTimer();
            missiles.manuallyShoot(targetUI.transform.parent.gameObject);
        }
    }

    IEnumerator CooldownTimer(){
        yield return new WaitForSeconds(10.0f);
        cooldown = true;
    }
}
