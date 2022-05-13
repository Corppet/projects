using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ReputationManager : MonoBehaviour
{
    [SerializeField] TMP_Text ASRepText;
    [SerializeField] TMP_Text BURepText;
    [SerializeField] TMP_Text FORRepText;
    [SerializeField] TMP_Text MRDRepText;

    private int ASRep;
    private int BURep;
    private int FORRep;
    private int MRDRep;

    // Start is called before the first frame update
    void Start(){
        ASRep = 1;
        BURep = 1;
        FORRep = 1;
        MRDRep = 1;
        // everyone begins with rep = 1 (Hostile)
        UpdateText();
    }

    public void UpdateReputation(Faction who){
        switch(who){
            case Faction.AscendantSystems:
                ASRep += 1;
                break;
            case Faction.BastionUnited:
                BURep += 1;
                break;
            case Faction.FollowingOfReunion:
                FORRep += 1;
                break;
            case Faction.MatrioshkaResearchDivision:
                MRDRep += 1;
                break;
        }
        UpdateText();
    }

    public void UpdateText(){
        ASRepText.text = ASRep + "";
        BURepText.text = BURep + "";
        FORRepText.text = FORRep + "";
        MRDRepText.text = MRDRep + "";
    }
}
