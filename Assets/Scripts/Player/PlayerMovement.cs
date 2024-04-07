using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public GameObject panel;
    public Vector2 lastMove;
    public float attackingTime;
    public string startPoint;

    private Animator anim;
    private Rigidbody2D myBody;
    private bool playerMoving;
    private static bool playerExist;
    private bool attacking;
    private float attackTimeCounter;

    void Awake()
    {
        anim = GetComponent<Animator>();
        myBody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!playerExist)
        {
            playerExist = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Vector3 pos = transform.position;
        // pos.z = 0;
        // transform.position = pos;

        Movement();
    }

    void Movement()
    {
        playerMoving = false;

        if (!attacking)
        {
            if (Input.GetAxis(Axis.HORIZONTAL_AXIS) > 0.5f || Input.GetAxis(Axis.HORIZONTAL_AXIS) < -0.5f)
            {
                myBody.velocity = new Vector2(Input.GetAxisRaw(Axis.HORIZONTAL_AXIS) * moveSpeed, myBody.velocity.y);
                playerMoving = true;
                lastMove = new Vector2(Input.GetAxisRaw(Axis.HORIZONTAL_AXIS), 0f);
            }

            if (Input.GetAxis(Axis.VERTICAL_AXIS) > 0.5f || Input.GetAxis(Axis.VERTICAL_AXIS) < -0.5f)
            {
                myBody.velocity = new Vector2(myBody.velocity.x, Input.GetAxisRaw(Axis.VERTICAL_AXIS) * moveSpeed);
                playerMoving = true;
                lastMove = new Vector2(Input.GetAxisRaw(Axis.VERTICAL_AXIS), 0f);
            }

            if (Input.GetAxisRaw(Axis.HORIZONTAL_AXIS) < 0.5f && Input.GetAxisRaw(Axis.HORIZONTAL_AXIS) > -0.5f)
            {
                myBody.velocity = new Vector2(0f, myBody.velocity.y);
            }

            if (Input.GetAxisRaw(Axis.VERTICAL_AXIS) < 0.5f && Input.GetAxisRaw(Axis.VERTICAL_AXIS) > -0.5f)
            {
                myBody.velocity = new Vector2(myBody.velocity.x, 0f);
            }
        }

        if (attackTimeCounter > 0)
            attackTimeCounter -= Time.deltaTime;

        if (attackTimeCounter <= 0)
        {
            attacking = false;
            anim.SetBool(AnimationTags.PLAYER_ATTACKING_BOOL, false);
        }

        GetInput();

        anim.SetFloat(AnimationTags.MOVE_X_FLOAT, Input.GetAxisRaw(Axis.HORIZONTAL_AXIS));
        anim.SetFloat(AnimationTags.MOVE_Y_FLOAT, Input.GetAxisRaw(Axis.VERTICAL_AXIS));
        anim.SetBool(AnimationTags.PLAYER_MOVING_BOOL, playerMoving);
        anim.SetFloat(AnimationTags.LAST_MOVE_X_FLOAT, lastMove.x);
        anim.SetFloat(AnimationTags.LAST_MOVE_Y_FLOAT, lastMove.y);
    }

    void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            attackTimeCounter = attackingTime;
            attacking = true;
            myBody.velocity = Vector3.zero;
            anim.SetBool(AnimationTags.PLAYER_ATTACKING_BOOL, true);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Tags.PICKABLE_1_TAG))
        {
            Score.instance.ScoreUp();
            PlayerHPmanager.instance.playerCurrentHealth += 3.5f;
            other.gameObject.SetActive(false);
        }

        if (other.CompareTag(Tags.PICKABLE_2_TAG))
        {
            Score.instance.ScoreUp();
            PlayerHPmanager.instance.playerCurrentHealth += 6.5f;
            other.gameObject.SetActive(false);
        }

        if (other.CompareTag(Tags.PICKABLE_3_TAG))
        {
            Score.instance.ScoreUp();
            PlayerHPmanager.instance.playerCurrentHealth += 9f;
            other.gameObject.SetActive(false);
        }

        if (other.CompareTag(Tags.FINISH_TAG))
        {
            panel.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
