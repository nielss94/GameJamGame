using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScreenCanvas : MonoBehaviour
{
    public Text customText;
    public Button optionButton;
    

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
