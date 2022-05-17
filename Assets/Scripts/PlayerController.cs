using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    private bool _isPlaying = false;

    private bool _isMoving = true;

    public bool IsMoving
    {
        get
        {
            return _isMoving;
        }
        set
        {
            _isMoving = value;
        }

    }

    private Rigidbody playerRb;

    public Rigidbody PlayerRb
    {
        get
        {
            return playerRb;
        }
        set
        {
            playerRb = value;
        }
    }


    public float _playerSpeed = 2.5f;
    [SerializeField] private float _sideLerpSpeed;
    [SerializeField] LayerMask layer;

    public bool onGate = false;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }


    }
    public void Start()
    {
        DOTween.Init();
        playerRb = GetComponent<Rigidbody>();


    }


    public void FixedUpdate()
    {

        if (Input.GetMouseButtonDown(0))
        {

            _isPlaying = true;

        }

        if (_isPlaying && _isMoving)
        {

            MoveForward();
            MoveSideWays();
        }



        Vector3 posX = transform.position;
        posX.x = Mathf.Clamp(transform.position.x, -1.16f, 1.16f);
        transform.position = posX;



    }

    void MoveForward()
    {
        playerRb.velocity = transform.forward * _playerSpeed;

    }
    void MoveSideWays()
    {
        Ray MyRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(MyRay, out hit, 200, layer))
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(hit.point.x, transform.position.y, transform.position.z), _sideLerpSpeed * Time.deltaTime);
        }
    }


    private IEnumerator TouchGate()
    {
        if (onGate)
        {

            _isMoving = false;


            playerRb.isKinematic = true;

            yield return new WaitForSeconds(1);
        }

    }


     public void ContinueMove()
    {
        playerRb.isKinematic = false;
        _isMoving = true;
    }
  
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gate"))
        {
            onGate = true;

            StartCoroutine(TouchGate());


        }
        if (other.CompareTag("LevelEnd"))
        {
            LevelManager.Instance.NextLevel();
        }
    
    }
}




