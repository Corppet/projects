using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomEventBastionEnd : CustomEvent {

    public void RunEvent(){
        List<EnemyAI> bastions = new List<EnemyAI>();
        // Get all bastion ships
        foreach (Affiliation aff in GameObject.FindObjectsOfType<Affiliation>()){
            if(aff.affiliation == FactionAffiliation.BASTION) 
                bastions.Add(aff.gameObject.GetComponent<EnemyAI>());
        }
        // Target LuSantka
        foreach(EnemyAI b in bastions){
            GameObject SoveriegnDraft = GameObject.Find("SoveriegnDraft");
            b.SetCustomTracker(SoveriegnDraft);
        }
    }
}
