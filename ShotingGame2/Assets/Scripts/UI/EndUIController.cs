using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndUIController : MonoBehaviour
{

    public void RestartBtnClick()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void GameOverBtnClick()
    {
        //Ω√¿€ æ¿
    }
}
