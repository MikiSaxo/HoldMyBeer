using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerAim : MonoBehaviour
{
    public static PlayerAim Instance;

    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _bulletParent;
    [SerializeField] private GameObject _mainSprite;
    [SerializeField] private GameObject _meleeObjRange;

    private Vector2 _aimInput;
    private Vector2 _aimInputLast;
    private Vector3 _transferPosition;

    private float _rangedCooldown;
    private float _rangedAtkSpeed;
    private float _rangedBulletSpeed;

    private float _meleeCooldown;
    private float _meleeAtkSpeed;
    private float _meleeAtkAngle;
    private float _meleeAtkRange;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _aimInputLast = new Vector2(1, 0);
        _meleeAtkRange = 1;
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        _aimInput = context.ReadValue<Vector2>();
        _aimInput.Normalize();
    }


    private void Update()
    {
        _transferPosition = new Vector3(_aimInputLast.x + transform.position.x, _aimInputLast.y + transform.position.y, 0);

        if (_aimInput.sqrMagnitude > 0)
            Aim();

        _rangedCooldown += Time.deltaTime;
        //_meleeCooldown += Time.deltaTime;

        if (_rangedCooldown > _rangedAtkSpeed)
        {
            _rangedCooldown -= _rangedAtkSpeed;
            RangedAtk();
        }

        //if (_meleeCooldown > _meleeAtkSpeed)
        //{
        //    _meleeCooldown -= _meleeAtkSpeed;
        //    MeleeAtk();
        //}
    }

    private void Aim()
    {
        _aimInputLast = _aimInput;

        float angle = Mathf.Atan2(_aimInputLast.y, _aimInputLast.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    private void RangedAtk()
    {
        var rotation = transform.rotation;
        rotation *= Quaternion.Euler(0, 0, 90);
        GameObject bullet = Instantiate(_bullet, _transferPosition, rotation);
        bullet.transform.SetParent(_bulletParent);
        bullet.GetComponent<BulletMovement>().Initialize(_aimInputLast, _rangedBulletSpeed);
    }

    private void MeleeAtk()
    {
        StartCoroutine(MeleeAnim());
    }

    IEnumerator MeleeAnim()
    {
        _meleeObjRange.SetActive(true);
        yield return new WaitForSeconds(.2f);
        _meleeObjRange.SetActive(false);
    }

    public void UpdateAimValues(float rangedBulletSpeed, float rangedAtkSpeed, float meleeAtkSpeed, float meleeAtkAngle, float meleeAtkRange)
    {
        _rangedBulletSpeed += rangedBulletSpeed;
        _rangedAtkSpeed += rangedAtkSpeed;
        _meleeAtkSpeed += meleeAtkSpeed;
        //UpdateMeleeAngle(meleeAtkAngle, meleeAtkRange);

    }

    private void UpdateMeleeAngle(float addedAngle, float meleeAtkRange)
    {
        _meleeObjRange.transform.Rotate(new Vector3(0, 0, addedAngle / 2));
        _meleeAtkAngle += addedAngle;
        _meleeObjRange.GetComponent<Image>().fillAmount = _meleeAtkAngle / 360;

        _meleeAtkRange += meleeAtkRange;
        _meleeObjRange.transform.localScale = Vector3.one * _meleeAtkRange;
    }

    //public void IncreaseAngleCursor()
    //{
    //    UpdateMeleeAngle(10f, _meleeAtkRange);
    //}
}