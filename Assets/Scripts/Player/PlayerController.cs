using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour, IDataPers
{
    public static PlayerController Instance;

    [SerializeField] float speedMove;
    [SerializeField] float jumpForce;
    [SerializeField] float speedMax;

    float horizontalMove;
    private float currentSpeed;
    float acceleration = 10f;
    Rigidbody rb;
    private Bowl ball;
    private SceneController sceneController;
    private DataManager dataManager;
    private int life;
    public bool sizeState;
    private int CKPCount;
    private float timerInvincibility = 1f;
    private bool canTrigger = true;

    private void Awake() 
    {
        Instance = this;
    }

    void Start()
    {
        transform.position = new Vector3(0, 1, 0);
        rb = gameObject.GetComponent<Rigidbody>();
        ball = Bowl.instanceBall;
        sceneController = SceneController.Instance;
        dataManager = DataManager.Instance;

        life = 3;
    }

    // Récup ici les data qu'on veut load
    public void LoadData(GameData data)
    {
        //this.transform.position = data.playerPos;
    }

    // Mettre ici ce qu'on veut save
    public void SaveData(ref GameData data)
    {
        data.playerPos = this.transform.position;
    }

    void Update()
    {
        // Deplacement horizontal
        horizontalMove = Input.GetAxis("Horizontal") * speedMove * Time.deltaTime;
        transform.position += transform.right * horizontalMove;

        // JUMP
        if((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space))  && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        // AVANCE 
        currentSpeed += acceleration * Time.deltaTime;
        if(currentSpeed >= speedMax)
        {
            currentSpeed = speedMax;
        }
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);

        // S'ACCROUPIR
        if(Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftShift))
        {
            transform.localScale = new Vector3(1, 0.5f, 1);
        } else if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.LeftShift))
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        // DEAD / FALL
        if(life <=0 || transform.position.y < -2 )
        {
            acceleration = 0;
            horizontalMove = 0;
            currentSpeed = 0;
            dataManager.SaveGame();
            sceneController.ChangeScene();
        }

        // CHRONO INVINCIBLE
        if (timerInvincibility > 0)
        {
            timerInvincibility -= Time.deltaTime;
        }
        if (timerInvincibility <= 0)
        {
            timerInvincibility = 0;
            canTrigger = true;
        }
    }

    // On rentre dans un obstacle
    private void OnTriggerEnter(Collider other) 
    {
        if(canTrigger)
        {
            if(other.name == "Obstacle")
            {
                life--;
                ball.BallNext(life); 
                timerInvincibility = 1f;
                canTrigger = false;
                Debug.Log("touche");        
            }
        }

        if(other.tag == "CKP")
        {
            CKPCount++;
            if(CKPCount%2 == 0)
            {
                sizeState = false;
            }
            else { sizeState = true;}
        }
        
    }

    // Verif si on est sur le sol ou non
    bool IsGrounded()
    {
        RaycastHit hit; // On tir un rayon 
        float distanceToGround = 1.2f; // Ajuster ca en fonction la taille
        // Lance un rayon vers le bas depuis le player à une certaines distance puis envoie le res (oui ou non) à hit
        return Physics.Raycast(transform.position, -Vector3.up, out hit, distanceToGround); 
    }

    private void OnDestroy() 
    {
        Instance = null;
    }
}
