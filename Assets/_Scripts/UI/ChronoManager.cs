using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChronoManager : MonoBehaviour
{
    public static ChronoManager Instance;

    [SerializeField] private TextMeshProUGUI chronoText;
    private bool isGameEnded;
    private float chrono;

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (!isGameEnded)
        {
            chrono += Time.deltaTime;
            TimeSpan time = TimeSpan.FromSeconds(chrono);
            chronoText.text = time.ToString(@"hh\:mm\:ss");
        }
    }
}
