using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkillTreemanager : MonoBehaviour
{

    private int BUBountyPoints;
    public TMP_Text BUBPText;

    // Start is called before the first frame update
    void Start(){
        BUBountyPoints = 3;
        BUBPText.text = BUBountyPoints + "";
    }

    void UpdatePoints(){
        BUBPText.text = BUBountyPoints + "";
    }

    public void PurchaseSkill(SkillTreeButton sb){
        if(sb.getSkill().points <= BUBountyPoints && (sb.getSkill().prerequisite == null || !sb.getSkill().prerequisite.locked)){
            BUBountyPoints -= sb.getSkill().points;
            sb.getSkill().locked = false;
            UpdatePoints();
            sb.DestroyThis();
        }
    }

    
}
