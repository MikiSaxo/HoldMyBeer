using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardUpgrade : MonoBehaviour
{
    public UpgradesData UpgradeData;

    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _description;

    public void Initialize(UpgradesData upgradeData)
    {
        UpgradeData = upgradeData;

        _icon.sprite = UpgradeData.Icon;
        _description.text = UpgradeData.Description;
    }
}
