using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 8f; //Used to determine how fast the player speed will be, can be set in inspector
    [SerializeField] private LayerMask collidableObject;
    [SerializeField] private Vector2 BoxCastSize = new Vector2(0.9f, 1f);
    [SerializeField] private Vector2 CastRightDirection = new Vector2(1f, 0f);
    [SerializeField] private Vector2 CastLeftDirection = new Vector2(-1f, 0f);
    [SerializeField] private Vector2 CastUpDirection = new Vector2(0f, 1f);
    [SerializeField] private Vector2 CastDownDirection = new Vector2(0f, -1f);
    [SerializeField] private float speedBoostTimeLimit = 0.4f;
    [SerializeField] private float speedBoostTimer = 0.4f;

    private float baseMoveSpeed = 8f;
    [SerializeField] private bool speedBoostBool = false;
    private Transform body;
    private float horizontal; //Used to store player input for x axis movement
    private float vertical;
    private float DiagnolSpeedReduction = 0.7f;
    private bool rightMovementAllowed = true;
    private bool lefttMovementAllowed = true;
    private bool upMovementAllowed = true;
    private bool downMovementAllowed = true;

    public bool isMoving = false; //public bool used to let other potential scripts to know player is moving

    // Start is called before the first frame update
    private void Start()
    {
        speedBoostTimer = speedBoostTimeLimit;
        baseMoveSpeed = moveSpeed;
        body = transform;
    }

    // Update is called once per frame
    private void Update() //used for non physic update methods, player input collection,check if player has moved logic...
    {
        DetectCollisions();
        InputCollection();
        CheckMovement();
        Movement();
        SpeedBoost();
    }

    private void InputCollection()
    {
        //Gives a value between -1 and 1. Uses old input manager. Can be viewd in Unity via Edit>Project Settings>Input Manager>Axes drop down arrow
        horizontal = Input.GetAxisRaw("Horizontal"); //-1 is left,use getaxisraw if you want snappy controls
        vertical = Input.GetAxisRaw("Vertical");
    }

    private void Movement() //physics behind horizontal movement
    {
        float horizontalSpeed = horizontal * moveSpeed; //store product of horizontal (-1,0,1) and moveSpeed
        float verticalSpeed = vertical * moveSpeed;

        if (horizontal > 0 && rightMovementAllowed == false)
        {
            horizontalSpeed = 0;
        }
        else if (horizontal < 0 && lefttMovementAllowed == false)
        {
            horizontalSpeed = 0;
        }
        else if (vertical > 0 && upMovementAllowed == false)
        {
            verticalSpeed = 0;
        }
        else if (vertical < 0 && downMovementAllowed == false)
        {
            verticalSpeed = 0;
        }

        if (horizontal != 0 && vertical != 0)
        {
            body.position += new Vector3(horizontalSpeed * DiagnolSpeedReduction * Time.deltaTime, verticalSpeed * DiagnolSpeedReduction * Time.deltaTime, 0f);
        }
        else
        {
            body.position += new Vector3(horizontalSpeed * Time.deltaTime, verticalSpeed * Time.deltaTime, 0f); //set the RB velocity to horizontalSpeed, results in horizontal player movement
        }
    }

    private void CheckMovement()
    {
        isMoving = horizontal != 0; //ismoving will set to true if horizontal is not equal to 0. This is a conditional expression
    }

    private void DetectCollisions()
    {
        Vector2 pos = new Vector2(transform.position.x, transform.position.y);

        RaycastHit2D hitRight = Physics2D.BoxCast(pos, BoxCastSize, 0f, CastRightDirection, 0.1f, collidableObject);
        RaycastHit2D hitLeft = Physics2D.BoxCast(pos, BoxCastSize, 0f, CastLeftDirection, 0.1f, collidableObject);
        RaycastHit2D hitUp = Physics2D.BoxCast(pos, BoxCastSize, 0f, CastUpDirection, 0.1f, collidableObject);
        RaycastHit2D hitDown = Physics2D.BoxCast(pos, BoxCastSize, 0f, CastDownDirection, 0.1f, collidableObject);

        if (hitRight.collider != null)
        {
            rightMovementAllowed = false;
        }
        else
        {
            rightMovementAllowed = true;
        }

        if (hitLeft.collider != null)
        {
            lefttMovementAllowed = false;
        }
        else
        {
            lefttMovementAllowed = true;
        }

        if (hitUp.collider != null)
        {
            upMovementAllowed = false;
        }
        else
        {
            upMovementAllowed = true;
        }

        if (hitDown.collider != null)
        {
            downMovementAllowed = false;
        }
        else
        {
            downMovementAllowed = true;
        }
    }

    private void SpeedBoost()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            speedBoostBool = true;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (speedBoostBool == true)
            {
                moveSpeed = baseMoveSpeed * 3;
                if (speedBoostTimer > 0)
                {
                    speedBoostTimer -= Time.deltaTime;
                }
                else
                {
                    speedBoostBool = false;
                    moveSpeed = baseMoveSpeed;
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            speedBoostBool = false;
            moveSpeed = baseMoveSpeed;
        }

        if (speedBoostBool == false && speedBoostTimer < speedBoostTimeLimit)
        {
            speedBoostTimer += Time.deltaTime;
        }
        else if (speedBoostTimer > speedBoostTimeLimit)
        {
            speedBoostTimer = speedBoostTimeLimit;
        }
    }
}
