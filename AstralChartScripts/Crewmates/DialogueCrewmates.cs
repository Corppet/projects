using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.IO;
using Random = UnityEngine.Random;
using System.Linq;

public class DialogueCrewmates : MonoBehaviour
{
    /*                          Anatomy of the storage for dialogue
        -----------------------------------------------------------------------------------
        (Folder) -> Two People (Richards and Carter)
        List -> All possible topics with the two characters

        Dialogues
            -> RC (R -> C)
                -> File: (Gun_RC_1)
    
    */
    public Dictionary<string, DialogueList[]> dl;
    [SerializeField] DialogueManager dm;
    [SerializeField] CrewButtonController cbc;

    // Start is called before the first frame update
    void Start(){
        dl = FileIO();
        //DebugFiles(dl);
    }

    // Reads in all of the DialogueList Files from the Crewmates folder and then organizes the lists to a dictionary
    Dictionary<string, DialogueList[]> FileIO(){
        DialogueList[] dialogues = Resources.LoadAll<DialogueList>("Crewmates");
        Dictionary<string, DialogueList[]> new_dl = new Dictionary<string, DialogueList[]>();
        Array.Sort(dialogues, (one, two) => string.Compare(one.personNames, two.personNames));
        int x = 0;
        while(x < dialogues.Length-1){
            DialogueList[] list = new DialogueList[dialogues.Length];
            int i = 0;
            while(x < dialogues.Length-1 && dialogues[x].personNames == dialogues[x+1].personNames){
                list[i] = dialogues[x];
                x++;
                i++;
            }
            list[i] = dialogues[x];
            x++;
            new_dl.Add(list[0].personNames, list);
        }
        return new_dl;
    }

    void DebugFiles(Dictionary<string, DialogueList[]> dict){
        // Look at files
        foreach(var entry in dict.Keys){
            Debug.Log(entry);
        }

        foreach(var entry_2 in dict.Values){
            Debug.Log(entry_2.Length);
        }
    }

    // one is the initiator
    public void BeginDialogue(Card one, Card two){
        StartCoroutine(DialogueCor(one, two));
    }

    DialogueList SearchForDialogue(string whoToWho, int progression, string topic){
        string file_name = topic + "_" + whoToWho + "_" + progression;
        DialogueList[] temp = null;
        DialogueList temp_dl = null;
        if(dl.TryGetValue(whoToWho, out temp))
            temp_dl = Array.Find(temp, element => element.fileName == file_name);
        return temp_dl;
    }

    /*
    public struct DCComparer{
        public DCComparer(DialogueCharacter one, DialogueCharacter two){
            this.one = one;
            this.two = two;
        }

        private int ImprovementsPiloting(){ return (one.piloting > two.piloting) ? two.piloting+1 : one.piloting+1; }
        private string ImprovementsPilotingName(){ return (one.piloting > two.piloting) ? two.name : one.name; }
        private int ImprovementsShooting(){ return (one.sharpshooter > two.sharpshooter) ? two.sharpshooter+1 : one.sharpshooter+1; }
        private string ImprovementsPilotingName(){ return (one.piloting > two.piloting) ? two.name : one.name; }
        private int ImprovementsEngineering(){ return (one.engineering > two.engineering) ? two.engineering+1 : one.engineering+1; }
        private string ImprovementsPilotingName(){ return (one.piloting > two.piloting) ? two.name : one.name; }

        public DialogueCharacter one { get; }
        public DialogueCharacter two { get; }

    }*/

    public DialogueList FindTalkingPoints(Card initiator, Card listener){
        //DCComparer dcc = new DCComparer(one, two);
        //vector<string> talking_points = new vector<string>();
        float progression_points = 5.0f;
        string person = initiator.getName() + listener.getName();
        string d_type = "";
        /*
        if(initiator.hasMaxPilotExperience()){
            d_type = "Pilot";
            listener.upgradePiloting();
        } else if(initiator.hasMaxWeaponExperience()){
            d_type = "Guns";
            listener.upgradeSharpShooting();
        } else if(initiator.hasMaxEngineeringExperience()){
            d_type = "Engineer";
            listener.upgradeEngineering();
        }*/
        return SearchForDialogue(person, (int) progression_points, d_type);
        //return SearchForDialogue("RichardsCarter", 1, "Guns");
    }

    IEnumerator DialogueCor(Card initiator, Card listener){
        cbc.Close();
        //yield return StartCoroutine(dm.StartDialogue(FindTalkingPoints(initiator, listener)));
        cbc.Open();
        yield return null;
    }

    // Update is called once per frame
    void Update(){
        
    }
}
