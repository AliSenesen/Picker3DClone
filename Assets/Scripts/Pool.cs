using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
public class Pool : MonoBehaviour
{
    [SerializeField] private int poolValue;
    private int totalBallCount;
    [SerializeField] TextMeshPro gateTextNesh;
    private List<GameObject> balls = new List<GameObject>();

    [SerializeField] private Color groundColor;

    [SerializeField] private Collider parentCollider;

    public void Start()
    {
        SetUpdateText();
    }

    private void SetUpdateText()
    {
        gateTextNesh.SetText(totalBallCount + " / " + poolValue);

    }


    private void PoolCalculater()
    {
        if (poolValue <= totalBallCount)
        {
            StartCoroutine(PassedPool());
        }

        SetUpdateText();
    }

    private IEnumerator PassedPool()
    {
        for (int i = 0; i < balls.Count; i++)
        {

            float randomFloat = UnityEngine.Random.Range(3, 5);

            balls[i].GetComponent<Rigidbody>().AddForce(new Vector3(0, randomFloat, 0), ForceMode.VelocityChange);

        }

        yield return new WaitForSeconds(.5f);

        for (int i = 0; i < balls.Count; i++)
        {
            balls[i].GetComponent<Renderer>().material.DOColor(Color.magenta, 0.3f).SetEase(Ease.InFlash);
        }

        yield return new WaitForSeconds(2f);

        //BURAYA PARTICLE ATILACAK//

        transform.DOLocalMoveZ(-2.9f, 1).SetEase(Ease.Linear).OnComplete(() =>
        {

            transform.GetComponent<Renderer>().material.DOColor(groundColor, 0.3f).SetEase(Ease.InFlash);
          
        });


        yield return new WaitForSeconds(1.5f);
       
        parentCollider.enabled = false;

        PlayerController.instance.IsMoving = true;
        
        PlayerController.instance.PlayerRb.isKinematic = false;

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball") && other.TryGetComponent<Ball>(out var ball))
        {
            if (!ball.ballTouched)
            {
                ball.ballTouched = true;

                totalBallCount++;

                balls.Add(ball.gameObject);

                PoolCalculater();
            }

        }

    }
}


