using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;

using playControll = PlayerController;

public class ButtonsActions : MonoBehaviour
{
    public TMP_InputField interpreterText;
    public static string inputText = "";


    public void StartButton()
    {
        // Read context of InputField
        if(interpreterText.text != null)
        {
            ButtonsActions.inputText = interpreterText.text;
        }
        else
        {
            print("Nothing to interpret");
        }
    }

    public void LoadSceneOnClick(int sceneNo)
    {
        SceneManager.LoadScene(sceneNo);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenLink(string URL)
    {
        Application.OpenURL(URL);
    }



}
