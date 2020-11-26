using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float speed2;
    public Animator aniController;
    Rigidbody playerRb;
    Renderer playerRdr;
    public Material[] playerMtrs;
    float gravityModifier = 2f;
    float jumpCount = 0f;
    public GameObject PosText;
    float timeCount = 10f;
    int timeCountInt;
    public GameObject Bridge;
    public GameObject MovingPlatform;
    public GameObject Child;
    float coinCount;
    bool timerstart= false;
    bool forward = true;
    bool platformmoving = false;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerRdr = Child.GetComponent<SkinnedMeshRenderer>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            StartRun();
        }
        if(Input.GetKeyUp(KeyCode.W))
        {
            aniController.SetBool("Run", false);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            StartRun();
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            aniController.SetBool("Run", false);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Euler(0, -90, 0);
            StartRun();
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            aniController.SetBool("Run", false);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
            StartRun();
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            aniController.SetBool("Run", false);
        }
        jumpplayer();
        if(timerstart == true)
        {
            timerCountDown();
            if (timeCountInt <= 0)
            {
                Bridge.transform.rotation = Quaternion.Euler(0, 0, 0);
                timeCount = 10;
                timerstart = false;
            }
        }
        if(platformmoving == true)
        {
            movingPlatform();
        }
        if(transform.position.y <-5)
        {
            SceneManager.LoadScene("LoseScene");
        }
    }
    void StartRun()
    {
        aniController.SetBool("Run", true);
        aniController.SetFloat("startRun", 3);
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("MovingPlane") || collision.gameObject.CompareTag("Bridge"))
        {
            jumpCount = 0f;
            aniController.SetBool("Jump", false);
            playerRdr.material.color = playerMtrs[1].color;
        }
        if(collision.gameObject.CompareTag("QuestionMark"))
        {
            SceneManager.LoadScene("WinScene");
        }
    }
    private void jumpplayer()
    {
        if (Input.GetKey(KeyCode.Space) && jumpCount < 1)
        {
            playerRb.AddForce(Vector3.up * 10, ForceMode.Impulse);
            jumpCount++;
            playerRdr.material.color = playerMtrs[0].color;
            aniController.SetBool("Jump", true);
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            aniController.SetBool("Jump", false);
        }
    }
    private void timerCountDown()
    {
        if (timeCount > 0)
        {
            timeCount -= Time.deltaTime;
            timeCountInt = Mathf.RoundToInt(timeCount);
        }
        PosText.GetComponent<Text>().text = "Timer: " + timeCountInt;
    }
    private void movingPlatform()
    {
        if (MovingPlatform.transform.position.z < 45 && forward == true)
        {
            MovingPlatform.transform.Translate(Vector3.forward * Time.deltaTime * speed2);
        }
        else if (MovingPlatform.transform.position.z > 29 && forward == false)
        {
            MovingPlatform.transform.Translate(Vector3.forward * Time.deltaTime * -speed2);
        }
        else
        {
            forward = !forward;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (coinCount == 4)
        {
            if (other.gameObject.tag == "Cone")
            {
                if (timerstart == false)
                {
                    Bridge.transform.rotation = Quaternion.Euler(0, 90, 0);
                    timerstart = true;
                }
            }
        }
        if(other.gameObject.tag == "Box")
        {
            platformmoving = true;
        }
        if (other.gameObject.tag == "Coin")
        {
            Destroy(other.gameObject);
            coinCount++;
        }
    }
}
