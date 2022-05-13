using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Button))]
public class WeaponTreeButton : MonoBehaviour
{
    // Adjustable Values
    public WeaponUpgrade details;

    [Space]

    // Other Serialized Fields
    [SerializeField] protected WeaponTreeManager manager;
    [SerializeField] protected Image weaponImage;
    [SerializeField] protected Button button;
    [SerializeField] protected TMP_Text pointsCostText;

    public void updateDetails()
    {
        if (details.uiImage != null)
            weaponImage.sprite = details.uiImage;

        pointsCostText.text = details.weaponPoints.ToString();

        if (details.treePurchased)
            weaponImage.color = Color.blue;
    }

    public void selectWeapon()
    {
        manager.setSelected(gameObject);
    }

    private void Awake()
    {
        updateDetails();
    }
}
