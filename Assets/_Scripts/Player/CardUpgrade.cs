using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class CardUpgrade : MonoBehaviour
{
    [HideInInspector] public UpgradesData UpgradeData;

    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _description;

    //private float _movementSpeed;
    //private float _atkSpeed;
    //private int _atkDamage;

    public void Initialize(UpgradesData upgradeData)
    {
        UpgradeData = upgradeData;

        _icon.sprite = UpgradeData.Icon;
        _description.text = UpgradeData.Description;

        //print("Init " + UpgradeData.MovementSpeed + " " + gameObject.name);

        //_movementSpeed = UpgradeData.MovementSpeed;
        //_atkSpeed = UpgradeData.AtkSpeed;
        //_atkDamage = UpgradeData.AtkDamage;
    }

    //public void OnMouseEnterred()
    //{
    //    gameObject.transform.DOComplete();
    //    gameObject.transform.DOScale(Vector3.one * 1.1f, .3f);
    //}
    //public void OnMouseLeaved()
    //{
    //    gameObject.transform.DOComplete();
    //    gameObject.transform.DOScale(Vector3.one, .1f);
    //}

    public void OnClicked(int which)
    {
        PlayerManager.Instance.GetNewUpgrades(which);
        //print("Click " + UpgradeData.MovementSpeed + " " + gameObject.name);

    }
}
