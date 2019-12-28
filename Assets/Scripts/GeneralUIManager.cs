using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralUIManager : MonoBehaviour
{
    public SettingsUI settings;
    public GameObject converter;

    private void Start()
    {
        settings.gameObject.SetActive(false);
        converter.gameObject.SetActive(true);
    }
    
    public void OnQuit()
    {
        Application.Quit();
    }

    public void OnSettings()
    {
        settings.gameObject.SetActive(!settings.gameObject.activeSelf);
        converter.SetActive(!converter.activeSelf);
    }
}
