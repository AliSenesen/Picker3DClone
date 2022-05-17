using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;
public class Pool : MonoBehaviour
{

    [SerializeField] private TextMeshPro countText;
    [SerializeField] private int neededBallCount;
    [SerializeField] private Collider gate;

    private int currentBallCount;

    private Material mat;

    List<GameObject> ballList = new List<GameObject>();

    public void Start()
    {
        UpdateText();
    }
    private void Passed()
    {
        mat = GetComponent<Renderer>().material;
        mat.DOColor(Color.blue, 2);

        transform.DOMoveY(0, 2).OnComplete(() => PlayerController.instance.ContinueMove());

        foreach (var item in ballList)
        {
            Destroy(item);
        }

        gate.enabled = false;
       
        GameManager.Instance.isGameOver = false;

        
    }
  

    private bool CanPlayerPass() 
    {
        if (currentBallCount >= neededBallCount)
        {
            return true;
        }
         

        else
        {
            return false;
        }
    }

    public void OnCollisionEnter(Collision collision)
    {

     

        if (collision.transform.CompareTag("Ball"))
        {

            ballList.Add(collision.gameObject );

            currentBallCount++;

            UpdateText();

            if (CanPlayerPass())
            {
               
                Passed();
               
               
            }

        }
       

    }
     IEnumerator Wait()
    {
        yield return new WaitForSeconds(2);

        if (!CanPlayerPass())
        {
            GameManager.Instance.isGameOver = true;
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Wait());


        }
    }

    public void UpdateText()
    {
        countText.text = currentBallCount.ToString() + " / " + neededBallCount.ToString();
    }
}


