using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
public class Pool : MonoBehaviour
{

    [SerializeField] private TextMeshPro countText;
    [SerializeField] private int neededBallCount;
    [SerializeField] private Collider gate;

    private int currentBallCount;

    

    private Material mat;

    public void Start()
    {
        UpdateText();
    }
    private void Passed()
    {
        mat = GetComponent<Renderer>().material;
        mat.DOColor(Color.blue, 2);

        transform.DOMoveY(0, 2).OnComplete(()=>PlayerController.instance.ContinueMove());

        gate.enabled = false;
        
    }

    private bool CanPlayerPass()
    {
        if (currentBallCount >= neededBallCount)
            return true;
        else
            return false;
    }

    public void OnCollisionEnter(Collision collision)
    {

        if (collision.transform.CompareTag("Ball"))
        {
            currentBallCount++;

            UpdateText();

            if (CanPlayerPass())
            {
                Passed();
            }
        }
    }

    public void UpdateText()
    {
        countText.text = currentBallCount.ToString() + " / " + neededBallCount.ToString();
    }
}


