using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb : MonoBehaviour
{

    [SerializeField] private GameObject explosionPreFab;
    [SerializeField] private LayerMask levelMask;
    [SerializeField] private LayerMask brickMask;
    private bool exploded = false;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Explode", 4f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Explode(){
        Instantiate(explosionPreFab, transform.position, Quaternion.identity);

        StartCoroutine(CreateExplosions(Vector3.forward));
        StartCoroutine(CreateExplosions(Vector3.right));
        StartCoroutine(CreateExplosions(Vector3.back));
        StartCoroutine(CreateExplosions(Vector3.left));  


        GetComponent<MeshRenderer>().enabled = false;
        exploded = true;
        //transform.Find("Rope").gameObject.SetActive(false); //3
        Destroy(gameObject, .3f);
    }

    private IEnumerator CreateExplosions(Vector3 direction){

        //how big the explosion is
        for (int i = 1; i < 3; i++) { 

            RaycastHit hit; 
            RaycastHit brik;

            Physics.Raycast(transform.position + new Vector3(0,.5f,0), direction, out hit, 
                i, levelMask); 

            Physics.Raycast(transform.position + new Vector3(0,.5f,0), direction, out brik, 
                i, brickMask);
            if (!hit.collider && !brik.collider){ 
                Instantiate(explosionPreFab, transform.position + (i * direction),
                //5 
                explosionPreFab.transform.rotation); 
                //6
            } 
            else if(brik.collider && !hit.collider){
                Instantiate(explosionPreFab, transform.position + (i * direction),
                //5 
                explosionPreFab.transform.rotation); 
                break;
            }
            else{ 
                break; 
            }


            yield return new WaitForSeconds(.05f); 
        }

    }

    public void OnTriggerEnter(Collider other){
        if (!exploded && other.CompareTag("Explosion")){  
            CancelInvoke("Explode"); // 2
            Explode(); // 3
        }  
    }
}
