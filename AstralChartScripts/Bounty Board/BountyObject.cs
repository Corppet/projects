using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Status of a bounty.
/// </summary>
public enum BountyStatus
{
    Available,
    InPursuit,
    Claimed
}

/// <summary>
/// All available factions.
/// </summary>
public enum Faction
{
    Neutral,
    AscendantSystems,
    BastionUnited,
    FollowingOfReunion,
    MatrioshkaResearchDivision
}

/// <summary>
/// General data/properties of a bounty.
/// </summary>
[CreateAssetMenu(fileName = "New Bounty", menuName = "Bounty Object", order = 1)]
public class BountyObject : ScriptableObject
{
    public string bountyName;
    public string description;
    public Faction faction;
    public Sprite factionLogo;

    public int requiredReputation;
    public WeaponUpgrade weaponReward;
    public float restoreHealthReward = 100f;
    public float maxHealthReward = 50f;

    public DialogueList start_interaction;
    public DialogueList end_interaction;

    // change later with a singleton?
    public CustomEvent scripedEvent;
    public CustomEventTypes eventOrigin;

    public bool Equals(BountyObject other)
    {
        return this.bountyName == other.bountyName;
    }
}
