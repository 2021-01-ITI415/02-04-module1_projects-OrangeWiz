using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision coll)
    {
        GameObject collidedWith = coll.gameObject;
        if (collidedWith.tag == "Obstacle")
        {
            Destroy(collidedWith);
            Destroy(this.gameObject);
        }
        if(collidedWith.tag == "Floor")
        {
            Destroy(this.gameObject);
        }   
        if (collidedWith.tag == "Goal")
        {
            SceneManager.LoadScene("Main-Prototype 1");
        }       

    }
}
