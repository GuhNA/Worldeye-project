using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //Static Parameters
    float jumpForce;
    float blinkTimerS;

    float speedS_;

    #region movimentParameters

    [Header("Run Parameters")]
    [SerializeField] float speed_;
    [SerializeField] float acceleration;
    [SerializeField] float decceleration;
    [SerializeField] float fricAmount;
    //[SerializeField] float velPower;
    float accelerate;
    //[SerializeField] bool run2T;

    
    //Life and Dmg parameters
    int life_;
    bool takingDmg;
    bool fallDmg_;
    bool invencible;

    [Space(10)]
    [Header("Jump parameters")]
    [SerializeField] float coyoteTime;
    [SerializeField] float jumpBuffer;
    [SerializeField] float jumpForceS;
    [SerializeField, Range(0f,1f)] float jumpCutSpeed;
    bool isJumping;
    float groundTime;
    float lastJumpTime;

    [Space(10)]
    [Header("Run Parameters")]
    [SerializeField] float airAccel;
    [SerializeField] float airDeccel;

    #endregion

    [Space(20)]
    [Header("Checks")]
    [SerializeField] float groundCheckSize_;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] BoxCollider2D box;

    float inputs;
    bool lookRight;


    
    bool cantJump_;
    bool canChange_;

    //blink function
    float blinkTimer;
    bool blinkb;
    bool tandf;
    int i;

    //CameraChangeView
    CinemachineCameraOffset cv_offset;
    float offset;

    #region encapsulation
    public int life
    {   
        get{return life_;}
        set{life_ = value;}
    }

    public bool fallDmg
    {
        get{return fallDmg_;}
        set{fallDmg_ = value;}
    }
    public bool canChange
    {
        get{return canChange_;}
        set{canChange_ = value;}
    }

    public float speed
    {
        get{return speed_;}
        set{speed_ = value;}
    }
    
    public float speedS
    {
        get{return speedS_;}
        set{speedS_ = value;}
    }

    public bool cantJump
    {
        get{return cantJump_;}
        set{cantJump_ = value;}
    }

    public float groundCheckSize
    {
        get{return groundCheckSize_;}
        set{groundCheckSize_ = value;}
    }
    #endregion


    Rigidbody2D rg;
    

    private void Awake()
    {
        rg = GetComponent<Rigidbody2D>();
        cv_offset = FindObjectOfType<CinemachineCameraOffset>();
    }

    private void Start()
    {
        speedS = speed;
        tandf = true;
        blinkTimer = 0.4f;
        life = 100;
        jumpForce = jumpForceS;
        lookRight = true;
        i = 0;
        offset = 0;
    }
    private void Update()
    {
        if(life <= 0)
        {
            SceneManager.LoadScene(0);
        }
        #region timers
        lastJumpTime -= Time.deltaTime;
        groundTime -= Time.deltaTime;
        #endregion

        inputs = Input.GetAxisRaw("Horizontal");

        if(inputs != 0) direcaoGiro(inputs > 0);

        if(Input.GetKeyDown(KeyCode.Space)) onJumpInput();

        if(Input.GetKeyUp(KeyCode.Space)) jumpCut();


        if((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W)) || 
        (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) ))
        {
            if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                offset = Mathf.Clamp(offset += Time.deltaTime, -0.7f,0.7f);
                cv_offset.m_Offset = new Vector3(0,offset,0);
            }
            else
            {
                offset = Mathf.Clamp(offset -= Time.deltaTime, -0.7f,0.7f);
                cv_offset.m_Offset = new Vector3(0,offset,0);
            }
        }
        else
        {
            if(offset > -0.1f && offset < 0.1f) 
            {
                offset = 0;
                cv_offset.m_Offset = new Vector3(0,0,0);
            }
            else if(offset > 0)
            {
                offset = Mathf.Clamp(offset -= Time.deltaTime, -0.7f,0.7f);
                cv_offset.m_Offset = new Vector3(0,offset,0);
            }
            else if (offset < 0)
            {
                offset = Mathf.Clamp(offset += Time.deltaTime, -0.7f,0.7f);
                cv_offset.m_Offset = new Vector3(0,offset,0);
            }
        }

        #region blinkdmg
        if(blinkb)
        {
            if(takingDmg && !fallDmg)
            {
                invencible = true;
            }
            blinkTimer -= Time.deltaTime;
            if(blinkTimer <= 0f)
            {
                tandf = !tandf;
                GetComponent<SpriteRenderer>().enabled = tandf;
                i++;
                blinkTimer = 0.4f;
            }

            if(i == 4)
            {
                GetComponent<SpriteRenderer>().enabled = true;
                blinkb = false;
                invencible = false;
                i = 0;
            }
        }
        #endregion

    }

    private void FixedUpdate()
    {
        #region friction
        if (groundTime > 0 && Mathf.Abs(inputs) < 0.01)
        {
            //Para caso o valor da velocity seja menor q o fric amount ele n va para tras.
            float fric = Mathf.Min(Mathf.Abs(rg.velocity.x), Mathf.Abs(fricAmount));

            //Pegando direcao
            fric *= Mathf.Sign(rg.velocity.x);

            //Contra a direcao.
            rg.AddForce(Vector2.right * -new Vector2(fric, 0), ForceMode2D.Impulse);
        }

        #endregion

        run();

        if(onGrounded())
        {
            jumpForce = jumpForceS;
            isJumping = false;
        }

        if (canJump())
        {
            isJumping = true;
            jump();
        }

    }

    #region moviment
    /*void run()
    {
        //Pegando o valor da velocidade pelo input(1 ou -1)
        float targetSpeed = inputs * speed;

        // Velocidade para parar maior e velocidade para avancar menor
        // ex: vel.x = 4 e targetSpeed = 6  speedDiff = 6-4 = 2
        //     vel.x = 4 e targetSpeed = -6 speedDiff = -6-4 = -10
        float speedDiff = targetSpeed - rg.velocity.x;

        //Est� freando ou acelerando?
        if (onGrounded())
        {
            accelerate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : decceleration;
        }
        else
        {
            accelerate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration * airAccel: decceleration * airDeccel;
        }

        if (Mathf.Abs(rg.velocity.x) > Mathf.Abs(targetSpeed) && Mathf.Sign(rg.velocity.x) == Mathf.Sign(targetSpeed) && Mathf.Abs(targetSpeed) > 0.01f && groundTime < 0)
        {
            //Prevent any deceleration from happening, or in other words conserve are current momentum
            //You could experiment with allowing for the player to slightly increae their speed whilst in this "state"
            accelerate = 0;
        }
        //aqui setamos os valores para provar oq foi falado em speed diff e usamos o sign para manter as direcoes
        float movement = Mathf.Pow(Mathf.Abs(targetSpeed) * accelerate, velPower) * Mathf.Sign(speedDiff);

        rg.AddForce(movement * Vector2.right);

    }*/

    void run()
    {
        float targetSpeed = inputs * speed;

        // Velocidade para parar maior e velocidade para avancar menor
        // ex: vel.x = 4 e targetSpeed = 6  speedDiff = 6-4 = 2
        //     vel.x = 4 e targetSpeed = -6 speedDiff = -6-4 = -10
        float speedDiff = targetSpeed - rg.velocity.x;

        //Esta freando ou acelerando?
         if (onGrounded())
        {
            accelerate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : decceleration;
        }
        else
        {
            accelerate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration * airAccel: decceleration * airDeccel;
        }


        float movement = speedDiff * accelerate;

        rg.AddForce(movement* Vector2.right, ForceMode2D.Force);
    }

    bool canJump()
    {
        if (groundTime > 0 && lastJumpTime > 0 && !isJumping && !cantJump) return true;
        else return false;
    }

    void jump()
    {
        groundTime = 0;
        lastJumpTime = 0;

        //caindo
        if (rg.velocity.y < 0) jumpForce -= rg.velocity.y;

        rg.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }


    void jumpCut()
    {
        if(rg.velocity.y > 0 && isJumping)
        {
            rg.AddForce(Vector2.down * rg.velocity.y * (1- jumpCutSpeed), ForceMode2D.Impulse);


            lastJumpTime = 0;
        }
    }
    void onJumpInput()
    {
        lastJumpTime = jumpBuffer;
    }

    void virar()
    {
        /*Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        lookRight = !lookRight;*/
        if(lookRight) transform.eulerAngles = new Vector2 (0,180);
        if(!lookRight) transform.eulerAngles = new Vector2 (0,0);
        lookRight = !lookRight;

    }


    void direcaoGiro(bool input)
    {
        if(input != lookRight)
        {
            virar();
        }    
    }

    #endregion

    public bool onGrounded()
    {   
        RaycastHit2D hitGround = Physics2D.BoxCast(box.bounds.center, box.bounds.size, 0, Vector2.down, groundCheckSize, groundLayer);
        if (hitGround)
        {
            if(hitGround.collider.CompareTag("DimensionChange")) canChange = true;

            groundTime = coyoteTime;
            isJumping = false;
            return true;
        }
        canChange = false;
        return false;
    }

    public void ChangeLife(int value)
    {
        if(value > 0)
        {
            life = Mathf.Clamp(life + value,0,100);
        }

        if(!invencible)
        {
            life = Mathf.Clamp(life + value,0,100);

            if(value < 0) 
            {
                takingDmg = true;
                i = 0;
                blinkb = true;
            }
        }
    }

}