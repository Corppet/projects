using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CustomEventTypes {
    Bastion, Reunion
}

[CreateAssetMenu(fileName = "CustomEvent", menuName = "AstralChart/CustomEvent", order = 0)]
public class CustomEvent : ScriptableObject {
    public void RunEvent(CustomEventTypes c){
        switch(c){
            case CustomEventTypes.Bastion:
                Debug.Log("lol");
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
                break;
            case CustomEventTypes.Reunion:
                List<EnemyAI> reunion = new List<EnemyAI>();
                // Get all reunion ships
                foreach (Affiliation aff in GameObject.FindObjectsOfType<Affiliation>()){
                    if(aff.affiliation == FactionAffiliation.REUNION) 
                        reunion.Add(aff.gameObject.GetComponent<EnemyAI>());
                }
                // Target BastionConstantine
                foreach(EnemyAI r in reunion){
                    GameObject BastionConstantine = GameObject.Find("BastionConstantine");
                    r.SetCustomTracker(BastionConstantine);
                }
                break;
        }
    }

}
