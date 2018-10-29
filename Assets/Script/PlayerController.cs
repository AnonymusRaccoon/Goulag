using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Definition des Inputs
    [HideInInspector]
     public string Horizontal;
    [HideInInspector] public string Vertical;
    [HideInInspector] public KeyCode JumpKey;
    [HideInInspector] public KeyCode UseKey;
    [HideInInspector] public KeyCode TakeKey;
    public bool setuped = false;

    //variable pour le movement processing
    [Space]
    Rigidbody2D rb2;
    [SerializeField]
    float sensiX = 1;
    [SerializeField]
    float sensiY = 1;
    private float vertical;
    [SerializeField]
    float DownDetector = 1;
    [SerializeField]
    float SideMargin = 0.3f;
    [SerializeField]
    int layerMask = 1 << 9;
   //int PlayerMask = 1 << 9;
    private bool IsGrounded = true;
    RaycastHit2D hit;
    SpriteRenderer rend;
    [SerializeField]
    ParticleSystem Particule;
    [SerializeField]
    float UseRange = 1;
    //variables de mort

    [SerializeField]
    AudioClip DeathSound;
    GameObject GameManager;

    //Du bazar
    AudioSource AS;
    [SerializeField]
    AudioClip LandingSound;
    

    void Start()
    {
        rb2 = gameObject.GetComponent<Rigidbody2D>();
        layerMask = ~layerMask;
        rend = gameObject.GetComponent<SpriteRenderer>();
        GameManager = GameObject.FindGameObjectWithTag("GameManager");
        AS = gameObject.GetComponent<AudioSource>();
        Particule = GetComponentInChildren<ParticleSystem>();
        //gameObject.GetComponent<ParticleSystem>();
    }


    void Update()
    {
        ShortInput();
        
    }
    #region



    void OnCollisionStay2D(Collision2D other)
    {
        if (other.transform != null && hit.transform != null)
        {
           
            if (other.transform.name == hit.transform.name)
            {
                bool _IsGrounded = true;

                if(_IsGrounded != IsGrounded)
                {
                    //PlaySound(LandingSound);
                    Particule.Play();
                }

                IsGrounded = true;
                _IsGrounded = IsGrounded;
               
            }
        }
    }

    void ShortInput()
    {
        // Si il faut rajouter du code pour différencer les manettes c'est ici
        float horizontal = Input.GetAxisRaw(Horizontal) * sensiX;
        float rawtical = Input.GetAxisRaw(Vertical) * sensiY;

        DectectInput(horizontal, rawtical);
    }

    void DectectInput(float horizontal, float rawtical)
    {
        //Ici on preprocess les inputs

        //preprocess du saut
        if (IsGrounded)
        {
            if (Input.GetKeyDown(JumpKey))
            {

                vertical = sensiY;
            }
            else
            {
                vertical = 0;
            }
        }
        if (Input.GetKeyUp(JumpKey))
        {
            rb2.velocity = new Vector2(rb2.velocity.x, rb2.velocity.y * 0.2f);
        }

        hit = Physics2D.Raycast(transform.position, Vector2.down, DownDetector, layerMask);
        if (hit.collider == null)
        {
            IsGrounded = false;
        }
        //preprocess de mouvements latéraux
        RaycastHit2D hitGauche = Physics2D.Raycast(transform.position, Vector2.left, SideMargin, layerMask);
        RaycastHit2D hitDroit = Physics2D.Raycast(transform.position, Vector2.right, SideMargin, layerMask);

        if (hitGauche.collider != null)
        {
            if (horizontal <= 0)
            {
                horizontal = 0;
            }
        }
        if (hitDroit.collider != null)
        {
            if (horizontal >= 0)
            {
                horizontal = 0;
            }
        }
        Move(horizontal, vertical);

        //on check pour péter un block

        if (Input.GetKey(UseKey))
        {
            Debug.Log("here");
            Ray2D DestroyRay = new Ray2D(transform.position,Vector2.right);
            RaycastHit2D hit;

            if (rend.flipX)
            {           
                hit = Physics2D.Raycast(DestroyRay.origin, DestroyRay.direction, UseRange, layerMask);
                Debug.DrawRay(DestroyRay.origin, DestroyRay.direction, Color.blue);
            }
            else
            {
                DestroyRay = new Ray2D(transform.position, Vector2.left);
                hit = Physics2D.Raycast(DestroyRay.origin, DestroyRay.direction, UseRange, layerMask);
                Debug.DrawRay(DestroyRay.origin, DestroyRay.direction, Color.blue);
            }

            if(hit.collider != null)
            {
                GameManager.GetComponent<EnvironementManager>().BreakTile(new Vector3Int(Mathf.RoundToInt(hit.transform.position[0]), Mathf.RoundToInt(hit.transform.position[1]), Mathf.RoundToInt(hit.transform.position[2]) ));
            }
        }


    }

   
    
        
    

    private void Move(float horizontal, float vertical)
    {
        if (horizontal == 0)
        {
            rb2.velocity = new Vector2(0, rb2.velocity.y);
        }
        if (horizontal > 0)
        {
            rend.flipX = true;
        }
        else if (horizontal < 0)
        {
            rend.flipX = false;
        }

        if (vertical > 0)
        {
            rend.flipY = true;
        }
        else
        {
            rend.flipY = false;
        }
        

        rb2.AddForce(new Vector2(0, vertical), ForceMode2D.Impulse);
        rb2.velocity = new Vector2(horizontal * Time.deltaTime, rb2.velocity.y);
    }
    #endregion 
    public void Die()
    {
        GameManager.GetComponent<AudioSource>().clip =DeathSound;
        GameManager.GetComponent<AudioSource>().Play();
        GameManager.GetComponent<GameManager>().Respawn();
        Debug.Log("Les vivants morts: ceux qui tolèrent l'injustice");
        Destroy(gameObject);
    }
    void PlaySound(AudioClip son)
    {
        AS.clip = son;
        AS.Play();
    }
}
