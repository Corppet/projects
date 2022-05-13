using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BountyController : MonoBehaviour
{
    // Adjustable Values
    public List<GameObject> enemies;

    // Serialized Fields
    [SerializeField] private ShopManager shopManager;

    // Private Variables
    private int coins;

    void Start()
    {
        // Get all of the enemies
        enemies = GameObject.FindGameObjectsWithTag("Enemy").ToList();
    }

    void SetCurrentBounty()
    {

    }

    void UpdateBoard()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Coins
        coins = shopManager.coins;
    }
}
