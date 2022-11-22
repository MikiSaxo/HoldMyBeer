using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _backButton;
    [SerializeField] private GameObject _startButton;

    public void OnStart()
    {
        SceneManager.LoadScene(1);
    }

    public void OnCredit()
    {
        EventSystem.current.SetSelectedGameObject(_backButton);
    }

    public void OnBackCredit()
    {
        EventSystem.current.SetSelectedGameObject(_startButton);
    }

    public void OnQuit()
    {
        Application.Quit();
    }
}
