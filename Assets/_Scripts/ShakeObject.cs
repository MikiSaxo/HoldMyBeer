using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShakeObject : MonoBehaviour
{
    [SerializeField] private float durationShaking = 1f;
    [SerializeField] private AnimationCurve _shakeCurve;

    public static ShakeObject Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void StartShakingCam(float _numberChoose)
    {
        StartCoroutine(Shaking(_numberChoose));
    }

    private IEnumerator Shaking(float _numberChoose)
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        var _addDuration = _numberChoose / 25 + durationShaking;

        while (elapsedTime < _addDuration)
        {
            elapsedTime += Time.deltaTime;
            float strength = _shakeCurve.Evaluate(elapsedTime / _addDuration);
            transform.position = startPosition + Random.insideUnitSphere * strength;
            yield return null;
        }

        transform.position = startPosition;
    }
}