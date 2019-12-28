using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralUIManager : MonoBehaviour
{
    public SettingsUI settings;
    public GameObject converter;

    public void OnQuit()
    {
        Application.Quit();
    }

    public void OnSettings(){
        settings.gameObject.SetActive(!settings.gameObject.activeSelf);
        converter.SetActive(!converter.activeSelf);
    }
}
