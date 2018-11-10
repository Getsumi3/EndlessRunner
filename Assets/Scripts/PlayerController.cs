using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    [Header("Controller vars")]
    public float speed = 2f;
    public float leftDistance = -4f, rightDistance = 4f;
    private AudioSource aSource;
    public Animator anim;

    [Header("UI vars")]
    public Text coinsTxt;
    public Text distanceTxt;
    public Text finalScore;
    public AudioClip coinSfx;
    public GameObject UIPanel;
    public GameObject gameOverPanel;
    private int coins;
    private float distance, finalDistance;

    private CharacterController cc;
    private Vector3 move = Vector3.zero;
    
	// Use this for initialization
	void Start () {
        cc = GetComponent<CharacterController>();
        aSource = GetComponent<AudioSource>();
        anim.Play("Running");
    }
	
	// Update is called once per frame
	void Update () {

        move.x = Time.deltaTime * speed;
        GetPlayerInput(move.z);
        cc.Move(move);

        distance += Time.deltaTime*16.5f;
        distanceTxt.text = distance.ToString("0.#") + "m";
    }

    private void GetPlayerInput(float _direction)
    {
        _direction = transform.position.z;
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (_direction == leftDistance)
            {
                transform.position = new Vector3(transform.position.x, 0, 0);
                
            }
            else if (_direction == 0)
            {
                transform.position = new Vector3(transform.position.x, 0, rightDistance);
            }

        } 

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (_direction == rightDistance)
            {
                transform.position = new Vector3(transform.position.x, 0, 0);
            }
            else if (_direction == 0)
            {
                transform.position = new Vector3(transform.position.x, 0, leftDistance);
            }
        }

    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Coins")
        {
            aSource.clip = coinSfx;
            aSource.Play();
            coins++;
            coinsTxt.text = "Coins: " + coins;
            hit.gameObject.SetActive(false);
        }

        if (hit.gameObject.tag == "Obstacles")
        {
            finalDistance = distance;
            anim.Play("Idle");
            finalScore.text = finalDistance.ToString("0.#") + "m and " + coins + " coins collected";
            StopAllCoroutines();
            gameOverPanel.SetActive(true);
            UIPanel.SetActive(false);
            this.enabled = false;
        }
    }
}
