using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitApp : MonoBehaviour
{
    public void Exit()
    {
        Application.Quit();
    }
    public void SetExitPanel(bool value)
    {
        gameObject.SetActive(value);
    }
}
