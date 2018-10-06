using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb2;
    [SerializeField]
    float sensiX = 1;
    [SerializeField]
    float sensiY = 1;
    private float vertical;
    [SerializeField]
    float DownDetector = 1;
    [SerializeField]
    float echo_act_3 = 10;

    int layerMask = 1 << 9;
    private bool IsGrounded = true;
    RaycastHit2D hit;
    SpriteRenderer rend;

    private Vector2 _lastpos = Vector2.zero;
    public string Horizontal = "Horizontal";
    public string Vertical = "Vertical";



<<<<<<< HEAD
    void Start() {
=======
    // Use this for initialization
    void Start ()
    {
<<<<<<< HEAD
>>>>>>> 26f285ae27df3e2756e31ec432f0687368a8cfcb
=======
>>>>>>> 26f285ae27df3e2756e31ec432f0687368a8cfcb
        rb2 = gameObject.GetComponent<Rigidbody2D>();
        layerMask = ~layerMask;
        rend = gameObject.GetComponent<SpriteRenderer>();
        _lastpos = transform.position;
    }
<<<<<<< HEAD


    void Update() {
=======
	
	// Update is called once per frame
	void Update ()
    {
<<<<<<< HEAD
>>>>>>> 26f285ae27df3e2756e31ec432f0687368a8cfcb
        DectectInput();
    }
<<<<<<< HEAD
    #region
    void OnCollisionStay2D(Collision2D other)
    {

        if (other.transform != null && hit.transform != null)
=======
=======
        DectectInput();
    }
>>>>>>> 26f285ae27df3e2756e31ec432f0687368a8cfcb

    void OnCollisionStay2D(Collision2D other)
    {
        if (other.transform != null && hit.transform !=null)
>>>>>>> 26f285ae27df3e2756e31ec432f0687368a8cfcb
        {
            if (other.transform.name == hit.transform.name)
            {
                IsGrounded = true;
<<<<<<< HEAD
<<<<<<< HEAD

            }
        }
    }
    
=======
            }
        }
    }

>>>>>>> 26f285ae27df3e2756e31ec432f0687368a8cfcb
=======
            }
        }
    }

>>>>>>> 26f285ae27df3e2756e31ec432f0687368a8cfcb
    void DectectInput()
    {
        float horizontal = Input.GetAxisRaw(Horizontal) * sensiX;

        float rawtical = Input.GetAxisRaw(Vertical) * sensiY;


        switch (Mathf.RoundToInt(Input.GetAxisRaw(Vertical)))
        {
            case 1:

                vertical = 1 * sensiY;
                break;
            case -1:

                break;
            case 0:

                vertical = 0;
                break;
            default:
                Debug.LogWarning("Somehow getaxis did not return -1,0,1");
                break;
        }

        hit = Physics2D.Raycast(transform.position, Vector2.down, DownDetector, layerMask);
<<<<<<< HEAD

        if (hit.collider == null)

<<<<<<< HEAD
        {
            IsGrounded = false;

        }

        if (IsGrounded == false)
        {

=======
        
        if(hit.collider == null)
        {
            IsGrounded = false;
        }


        if (IsGrounded == false)
        {
>>>>>>> 26f285ae27df3e2756e31ec432f0687368a8cfcb
=======
        hit = Physics2D.Raycast(transform.position, Vector2.down, DownDetector, layerMask);
        
        if(hit.collider == null)
        {
            IsGrounded = false;
        }


        if (IsGrounded == false)
        {
>>>>>>> 26f285ae27df3e2756e31ec432f0687368a8cfcb
            vertical = 0;
        }


        Move(horizontal, vertical);

        if (IsGrounded == false)
        {
            rb2.gravityScale = echo_act_3;
        }
        else
        {
            rb2.gravityScale = 1;
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
<<<<<<< HEAD
<<<<<<< HEAD
        } else if (horizontal < 0)
=======
        }
        else if (horizontal < 0)
>>>>>>> 26f285ae27df3e2756e31ec432f0687368a8cfcb
=======
        }
        else if (horizontal < 0)
>>>>>>> 26f285ae27df3e2756e31ec432f0687368a8cfcb
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

<<<<<<< HEAD
<<<<<<< HEAD
        rb2.AddForce(new Vector2(horizontal, vertical) * Time.deltaTime, ForceMode2D.Impulse);

    }



    #endregion

    public void Die()
    {
        Debug.Log("Belle journée pour mourir");
    }
=======
        rb2.AddForce(new Vector2(horizontal, vertical)*Time.deltaTime, ForceMode2D.Impulse);
    }
>>>>>>> 26f285ae27df3e2756e31ec432f0687368a8cfcb
=======
        rb2.AddForce(new Vector2(horizontal, vertical)*Time.deltaTime, ForceMode2D.Impulse);
    }
>>>>>>> 26f285ae27df3e2756e31ec432f0687368a8cfcb
}
