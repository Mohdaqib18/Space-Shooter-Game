using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDummy : MonoBehaviour

{
    private float _speed = 8f;
    private bool _isEnemyLaser = false;





    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveDown();
    }

    public void  MoveDown()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);


        if (transform.position.y <-7f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }

            Destroy(this.gameObject);
        }

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        

        if (other.tag == "Player" )
        {
            Player player = other.GetComponent<Player>();

            if (player !=null)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
