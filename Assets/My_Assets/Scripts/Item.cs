using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }  
    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.tag == Game.snakeTag)
    //    {
    //        Debug.Log("hit" + transform.name);
    //        animator.SetTrigger("end");
    //        Destroy(gameObject, .5f);
    //    }
    //}
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == Game.snakeTag)
    //    {
    //        Debug.Log("hit" + transform.name);
    //        animator.SetTrigger("end");
    //        Destroy(gameObject, .5f);
    //    }
    //}
}
