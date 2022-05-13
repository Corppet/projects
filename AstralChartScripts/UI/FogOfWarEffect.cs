using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWarEffect : MonoBehaviour{

    //private Camera camera;
    // Start is called before the first frame update
    void Start(){
        CullingGroup group = new CullingGroup();
        group.targetCamera = Camera.main;
        BoundingSphere[] spheres = new BoundingSphere[1000];
        spheres[0] = new BoundingSphere(Vector3.zero, 1f);
        group.SetBoundingSpheres(spheres);
        group.SetBoundingSphereCount(1);
        group.onStateChanged = StateChangedMethod;
        group.Dispose();
        group = null;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    private void StateChangedMethod(CullingGroupEvent evt){
        if(evt.hasBecomeVisible)
            Debug.LogFormat("Sphere {0} has become visible!", evt.index);
        if(evt.hasBecomeInvisible)
            Debug.LogFormat("Sphere {0} has become invisible!", evt.index);
    }
}
