using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton



    public static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();

                if (instance == null)
                {
                    GameObject gm = new GameObject("GameManager");
                    instance = gm.AddComponent<GameManager>();

                }

            }
            return instance;

        }


    }

    #endregion

    public Action OnStart;
    public Action OnWin;
    public Action OnFail;
    
    public void Awake()
    {
        instance = this;
    }





}


