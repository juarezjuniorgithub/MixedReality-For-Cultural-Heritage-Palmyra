using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScenes : MonoBehaviour
{
   public void Change()
    {
        SceneManager.LoadScene(1);
    }

    public void Change2()
    {
        SceneManager.LoadScene(0);
    }
}
