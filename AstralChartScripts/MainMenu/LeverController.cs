using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : MonoBehaviour
{
    bool activate;
    [SerializeField] Animator anim;

    void Start(){
        activate = true;
        PlayerPrefs.SetInt("Tutorial", 0);
        anim.SetTrigger("On");
    }

    void Update(){
        if(Input.GetMouseButtonDown(0)){
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit, 100.0f)){
                if(hit.transform == this.transform){
                    if(!activate){
                        PlayerPrefs.SetInt("Tutorial", 0);
                        anim.SetTrigger("On");
                    } else {
                        PlayerPrefs.SetInt("Tutorial", 1);
                        anim.SetTrigger("Off");
                    }
                    activate ^= true;
                }
            }
        }
    }
}
