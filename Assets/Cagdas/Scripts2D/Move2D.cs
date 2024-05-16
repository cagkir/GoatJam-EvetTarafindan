using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Move2D : MonoBehaviour
{

    [SerializeField] float moveSpeed2D, ForJumpDistance2D, JumpPower2D;
    [SerializeField] public Boolean left = false;
    [SerializeField] GameObject Child, meteor1, meteor2;
    [SerializeField] Boolean isRunning = false, canRunAnim = true;
    Animator animator;
    RaycastHit2D hit;
    [SerializeField] LayerMask lm;
    [SerializeField] TextMeshPro tmp;
    Boolean left1 = false, right1 = false;
    int kalp = 3;
    [SerializeField] Animator[] anims;
    public float toplanancoin = 0;



    //test için sonra silebilirsin
    float sayac = 0;
    Boolean oldu = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = Child.GetComponent<Animator>();
        tmp.text = toplanancoin + " / 6";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            right1 = true;            
        }
        if (Input.GetKey(KeyCode.A))
        {
            left1 = true;           
        }


        if (Input.GetKeyUp(KeyCode.D))
        {
            right1 = false;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            left1 = false;
        }

        if (Input.GetKeyUp(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            isRunning = false;
        }
        if (Input.GetKeyUp(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            isRunning = false;
        }
        if (Input.GetKeyUp(KeyCode.D) && Input.GetKeyUp(KeyCode.A))
        {
            isRunning = false;
        }


        hit = Physics2D.Raycast(gameObject.transform.position, Vector2.down, ForJumpDistance2D, lm);
        if (hit.collider != null)
        {
            animator.SetBool("jump", false);
            canRunAnim = true;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetBool("run", false);
                gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(0, JumpPower2D, 0));
                Console.Clear();
                transform.GetComponent<sfxcontroller>().jumped();
            }
        }
        else
        {
            canRunAnim = false;
            animator.SetBool("jump", true);
            
        }


        if (left)
        {
            Child.GetComponent<SpriteRenderer>().flipX = true;
        }
        if (!left)
        {
            Child.GetComponent<SpriteRenderer>().flipX = false;
        }

        if (isRunning && canRunAnim)
        {
            animator.SetBool("run", true);
        }
        if (!isRunning)
        {
            animator.SetBool("run", false);
        }

    }

    private void FixedUpdate()
    {
        if (left1)
        {
            transform.position += new Vector3(-moveSpeed2D, 0, 0);
            left = true;
            isRunning = true;
        }
        if (right1)
        {
            transform.position += new Vector3(moveSpeed2D, 0, 0);
            left = false;
            isRunning = true;
        }



        /*if (oldu)
        {
            sayac += Time.deltaTime;
        }

        if(sayac > 1f)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }*/
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "restart")
        {
            transform.GetComponent<sfxcontroller>().gothurt();
            restartSceneByHand();
        }
        if (collision.gameObject.tag == "trap")
        {
            transform.GetComponent<sfxcontroller>().gothurt();
            restartSceneByHand();
        }

        if(collision.gameObject.tag == "littlealien")
        {
            if(collision.transform.position.x < transform.position.x)
            {
                gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(500,500,0));
            }
            if (collision.transform.position.x > transform.position.x)
            {
                gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(-500, 500, 0));
            }
            transform.GetComponent<sfxcontroller>().gothurt();
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "cliff2")
        {
            GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");
            cam.GetComponent<Camfollow2D>().cliff2 = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "cliff")
        {
            GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");
            cam.GetComponent<Camfollow2D>().cliff = true;
            GameObject[] aliens = GameObject.FindGameObjectsWithTag("alienai");
            aliens[0].GetComponent<AIPath>().maxSpeed = 6;
            aliens[1].GetComponent<AIPath>().maxSpeed = 6;
        }
        


        if (collision.gameObject.tag == "coin")
        {
            toplanancoin += 1;
            Destroy(collision.gameObject);
            tmp.text = toplanancoin + " / 6";           
            if (toplanancoin == 6)
            {
                transform.GetComponent<sfxcontroller>().successed();
            }
            else
            {
                transform.GetComponent<sfxcontroller>().gotcoin();
            }
        }
        if(collision.gameObject.tag == "win2d")
        {
            if(toplanancoin == 6)
            {
                GameObject manager = GameObject.FindGameObjectWithTag("manager2d");
                manager.GetComponent<Manager>().GetInSpaceShip();
                animator.SetBool("run", false);
                this.gameObject.GetComponent<Move2D>().enabled = false;
                manager.GetComponent<Manager>().süresay = false;
            }
            else
            {
                //tüm yakýtlarý toplamadan roketi uçuramazsýn!
            }
        }
        if(collision.gameObject.tag == "alienai")
        {
            transform.GetComponent<sfxcontroller>().gothurt();
            restartSceneByHand();
        }
        if(collision.gameObject.tag == "meteor")
        {
            meteor1.GetComponent<Rigidbody2D>().gravityScale = 5;
        }
        if (collision.gameObject.tag == "meteor2")
        {
            meteor2.GetComponent<Rigidbody2D>().gravityScale = 5;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "cliff")
        {
            GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");
            cam.GetComponent<Camfollow2D>().cliff = false;
            
        }
        if (collision.gameObject.tag == "cliff2")
        {
            GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");
            cam.GetComponent<Camfollow2D>().cliff2 = false;
        }
    }

    void restartSceneByHand()
    {
        this.gameObject.transform.position = new Vector3(0, 0, -0.5f);
        GameObject[] aliens = GameObject.FindGameObjectsWithTag("alienai");
        aliens[0].gameObject.transform.position = new Vector3(-10, 1.5f, -0.9f);
        aliens[1].gameObject.transform.position = new Vector3(6, 13, -0.9f);
        aliens[0].GetComponent<AIPath>().maxSpeed = 3;
        aliens[1].GetComponent<AIPath>().maxSpeed = 3;

    }

    void won()
    {

    }


}
