using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccelerationBoxController : MonoBehaviour
{
    [SerializeField] Button okButton;
    [SerializeField] Slider slider;
    private Transform player;

    // Start is called before the first frame update
    void Start(){
        okButton.onClick.AddListener(TaskOnClick);
        player = GameObject.FindWithTag("Player").transform;
    }

	void TaskOnClick(){
        Destroy(this.gameObject);
	}
}
