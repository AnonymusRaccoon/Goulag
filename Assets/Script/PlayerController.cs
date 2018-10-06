using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour {
    Rigidbody2D rb2;
    [SerializeField]
    float sensiX = 1;
    [SerializeField]
    float sensiY = 1;
    private float vertical;
    [SerializeField]
    float DownDetector = 1;
    
    int layerMask = 1 << 9;
    private bool IsGrounded = true;
    RaycastHit2D hit;
    SpriteRenderer rend;
    bool flip_flop = false;

    // Use this for initialization
    void Start () {
        rb2 = gameObject.GetComponent<Rigidbody2D>();
        layerMask = ~layerMask;
        rend = gameObject.GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        DectectInput();

    }
    void OnCollisionStay2D(Collision2D other)
    {
        
        if (other.transform != null && hit.transform !=null)
        {
            if (other.transform.name == hit.transform.name)
            {
                IsGrounded = true;
               
            }
        }
    }
    void DectectInput()
    {
        float horizontal =  Input.GetAxisRaw("Horizontal")*sensiX;
        
        float rawtical = Input.GetAxisRaw("Vertical")*sensiY;

        if (flip_flop == true)
        {
            vertical = 0;
        }

        switch (Mathf.RoundToInt(Input.GetAxisRaw("Vertical")))
        {
            case 1:
                
                vertical = 1 * sensiY;
                flip_flop = true;
                
                break;
            case -1:
               
                break;
            case 0:
            
                vertical = 0;
                flip_flop = false;
                break;
            default:
                Debug.LogWarning("Somehow getaxis did not return -1,0,1");
                break;
        }
      
        
      


        hit = Physics2D.Raycast(transform.position, Vector2.down, DownDetector, layerMask);
        
        if(hit.collider == null)
        
        {
            
                IsGrounded = false;
               
        }
        
       

        if (IsGrounded == false)
        {
           
            vertical = 0;
        }
       
        

        Move(horizontal, vertical);
       
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
        }else if (horizontal < 0)
        {
            rend.flipX = false;
        }

        if(vertical > 0)
        {
             rend.flipY = true;
        }
        else
        {
            rend.flipY = false;
        }

        rb2.AddForce(new Vector2(horizontal, vertical)*Time.deltaTime, ForceMode2D.Impulse);

    }


}
