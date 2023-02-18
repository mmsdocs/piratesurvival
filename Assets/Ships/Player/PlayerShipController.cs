using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerShipController : ShipController
{
    [Header("Player Ship Specs")]
    public float engineImpulse = 0.0f;
    public float engineTorque = 0.0f;
    public Transform shipStates;
    

    [Header("Player Weapons Specs")]
    public Transform frontWeapon;
    public Transform[] sideWeapons;

    [Header("Player UI Specs")]
    public HUDController hud;

    private int currentScore = 0;
    private int currentTime = 0;
    private int levelTime = 60;

    private void Start()
    {
        s_Rigidbody = GetComponent<Rigidbody2D>();
        levelTime = PlayerPrefs.GetInt("levelTime");
        currentTime = levelTime;
    }

    private void Update()
    {
        UpdateLevelTime();
        LookAtMousePosition();
        Inputs();
        
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void LookAtMousePosition()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Utils.AngleBetween(transform.position, mousePosition, Vector2.up);
        float newAngle = Mathf.LerpAngle(transform.eulerAngles.z, angle, engineTorque * Time.deltaTime);

        s_Rigidbody.MoveRotation(newAngle);
    }

    private void UpdateLevelTime()
    {
        currentTime = (int) (levelTime - Time.timeSinceLevelLoad);

        hud.SetLevelTimeValue(currentTime, currentTime <= 5);

        bool isTimeout = currentTime == 0;
        if(isTimeout) GameOver(isTimeout);
    }

    private void Inputs()
    {
        if (CanShoot())
        {
            if (Input.GetButtonDown("Front Weapon"))
            {
                Shoot(frontWeapon);
            }
            if (Input.GetButtonDown("Side Weapon"))
            {
                foreach(Transform weapon in sideWeapons)
                {
                    Shoot(weapon);
                }
            }
        }
    }

    private void Move()
    {
        if (canMove)
        {
            float forwardInput = Input.GetAxis("Move Forward");
            s_Rigidbody.AddForce(s_Rigidbody.transform.up * engineImpulse * forwardInput);
        }
    }

    public void IncrementScore()
    {
        currentScore++;
        hud.SetScoreValue(currentScore);
    }

    protected override void NextShipState()
    {
        if (shipStates.childCount > 0 && currentShipState < shipStates.childCount)
        {
            shipStates.GetChild(currentShipState).gameObject.SetActive(false);
            currentShipState++;
            shipStates.GetChild(currentShipState).gameObject.SetActive(true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Damage();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("EnemyCannonBall"))
        {
            Damage();
        }
    }

    private void OnDestroy()
    {
        GameOver();
    }

    private void GameOver(bool wasTimeout = false)
    {
        int highscore = PlayerPrefs.GetInt("highscore");
        if (currentScore > highscore) PlayerPrefs.SetInt("highscore", currentScore);
        
        PlayerPrefs.SetInt("lastScore", currentScore);
        PlayerPrefs.SetString("wasTimeout", wasTimeout.ToString());
        
        SceneManager.LoadScene("GameOver");
    }
}
