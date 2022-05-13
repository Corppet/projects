using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "New Mainframe", menuName = "Upgrade Object/Mainframe Upgrade", order = 1)]
public class MainframeUpgrade : UpgradeObject
{
    public MainframeUpgrade()
    {
        upgradeType = UpgradeType.Mainframe;
    }

    public int rating;

    public bool Equals(MainframeUpgrade other)
    {
        return this.upgradeName == other.upgradeName;
    }
}
