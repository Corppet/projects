using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlameController : MonoBehaviour
{
    // Thrusters have one outer thruster and one inner thruster

    [SerializeField] GameObject[] thrusters;
    [SerializeField] EnemyAI ai;

    public List<ParticleSystem> innerThrusters;
    public List<ParticleSystem> outerThrusters;

    void Start(){
        foreach(GameObject g in thrusters){
            outerThrusters.Add(g.GetComponent<ParticleSystem>());
            innerThrusters.Add(g.transform.GetChild(0).GetComponent<ParticleSystem>());
        }
    }

    void Update(){
        if(ai != null){
            if(ai.GetFlameSpeed() > Mathf.Epsilon){
                foreach(ParticleSystem ps in innerThrusters){
                    var emission = ps.emission;
                    emission.enabled = true;
                    ps.startLifetime = ai.GetFlameSpeed();
                }
                
                foreach(ParticleSystem ps in outerThrusters){
                    var emission = ps.emission;
                    emission.enabled = true;
                    ps.startLifetime = ai.GetFlameSpeed();
                }
            } else {
                foreach(ParticleSystem ps in innerThrusters){
                    var emission = ps.emission;
                    emission.enabled = false;
                }
                
                foreach(ParticleSystem ps in outerThrusters){
                    var emission = ps.emission;
                    emission.enabled = false;
                }
            }
        }
    }
}
