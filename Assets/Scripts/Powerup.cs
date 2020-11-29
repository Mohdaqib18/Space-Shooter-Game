using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{   [SerializeField ]
    private float _speed = 3.0f;
    [SerializeField]
    private int _powerupId;
    [SerializeField]
    private AudioClip _clip;
    


    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < -5.4f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {

           Player player = other.transform.GetComponent<Player>();

            AudioSource.PlayClipAtPoint(_clip, transform.position);

            if (player !=null)
            {
           
                switch (_powerupId)
                {
                    case 0:
                        player.TripleShotActive();
                        break;

                    case 1:
                        player.SpeedBoostActive();
                        break;

                    case 2:
                        player.ShieldsActive();                     
                        break;
                    case 3:
                        player.HealthActive();
                        break;
                    case 4:
                        player.AmmoActive();
                        break;

                    default:
                        Debug.Log("Default case selected");
                        break;
                }
            }

            Destroy(this.gameObject);
        }
    }


}
