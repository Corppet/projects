using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameController : MonoBehaviour
{
    private Transform player;
    private float speed;
    private ParticleSystem ps;
    public AudioSource ThrustSound;
    [SerializeField] bool one;
    // Start is called before the first frame update
    void Start(){
        // Get Player
        ps = GetComponent<ParticleSystem>();
        GameObject p = GameObject.Find("PlayerHolder");
        if(p != null)
            this.player = p.transform.GetChild(0).transform;
    }

    // Update is called once per frame
    void Update(){
        if(player == null){
            var emission = ps.emission;
            emission.enabled = true;
            var main = ps.main;
            main.startLifetime = 2.0f;
            return;
        }

        speed = player.GetComponent<TankControlsLockOn>().getSpeed();
        if(speed > 0){
            var emission = ps.emission;
            emission.enabled = true;
            var main = ps.main;
            main.startLifetime = speed;
            if (!ThrustSound.isPlaying)
            {
                //ThrustSound.Play();
            }
        } else {
            var emission = ps.emission;
            emission.enabled = false;
            ThrustSound.Stop();
        }
    }
}
