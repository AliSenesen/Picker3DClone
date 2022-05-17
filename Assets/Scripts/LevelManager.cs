using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    #region Singleton



    public static LevelManager instance;

    public static LevelManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<LevelManager>();

                if (instance == null)
                {
                    GameObject Lm = new GameObject("LevelManager");

                    instance = Lm.AddComponent<LevelManager>();

                }

            }
            return instance;

        }
    }
    #endregion


    public void NextLevel()
    {

        if (SceneManager.GetActiveScene().buildIndex == 2)
            SceneManager.LoadScene(0);
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
