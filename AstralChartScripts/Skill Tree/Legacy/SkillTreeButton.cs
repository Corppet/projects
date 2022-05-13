using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTreeButton : MonoBehaviour
{
    public SkillObject skill;
    private GameObject lockerButton;

    void Awake(){ lockerButton = this.gameObject; }

    public SkillObject getSkill(){ return skill; }
    public void DestroyThis(){ Destroy(this.gameObject); }
}
