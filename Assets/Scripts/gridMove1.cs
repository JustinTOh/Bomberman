using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//BUG WHEN MOVING 2 PLAYERS TO THE SAME SQUARE
public class gridMove1 : MonoBehaviour
{
    private float moveSpeed = 5f;
    [SerializeField] private Transform movePoint;
    [SerializeField] private LayerMask playerMask;
    // Start is called before the first frame update
    //private int bombs = 2;
    private Rigidbody rigidPlayer;
    private Transform myTransform;
    [SerializeField] private bool canDropBombs = true;
    [SerializeField] private GameObject bombPreFab;
    public bool dead = false;
    public GlobalStateManager globalManager;

    void Start()
    {
        movePoint.parent = null;
        rigidPlayer = GetComponent<Rigidbody> ();
        myTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if(Vector3.Distance(transform.position, movePoint.position) <=0.15f){

            if(Input.GetKey(KeyCode.UpArrow)){
                if(Physics.OverlapSphere(movePoint.position + Vector3.forward, .2f, playerMask).Length == 0){
                    movePoint.position += Vector3.forward;
                }
            }
            
            else if(Input.GetKey(KeyCode.DownArrow)){
                if(Physics.OverlapSphere(movePoint.position + Vector3.back, .2f, playerMask).Length == 0){                
                    movePoint.position += Vector3.back;
                }
            }

            else if(Input.GetKey(KeyCode.LeftArrow)){
                if(Physics.OverlapSphere(movePoint.position + Vector3.left, .2f, playerMask).Length == 0){                
                    movePoint.position += Vector3.left;
                }
            }

            else if(Input.GetKey(KeyCode.RightArrow)){
                if(Physics.OverlapSphere(movePoint.position + Vector3.right, .2f, playerMask).Length == 0){                
                    movePoint.position += Vector3.right;
                }
            }
        }

        if(canDropBombs && (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))){
            DropBomb();
        }
    }

    private void DropBomb(){
        if(bombPreFab){
            //Instantiate(bombPreFab, myTransform.position, bombPreFab.transform.rotation);
            Instantiate(bombPreFab, new Vector3(Mathf.RoundToInt(myTransform.position.x), 
            bombPreFab.transform.position.y, Mathf.RoundToInt(myTransform.position.z)),
            bombPreFab.transform.rotation); 
        }
    }


    public void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag ("Explosion"))
        {
            Debug.Log ("P1 hit by explosion!");
            dead = true;
            globalManager.PlayerDied(1);
            Destroy(gameObject);
        }
    }
}
