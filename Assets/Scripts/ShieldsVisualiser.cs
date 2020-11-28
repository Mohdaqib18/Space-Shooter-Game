using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldsVisualiser : MonoBehaviour
{


    public int _shieldsLife = 4;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy" || other.tag == "Enemy Laser")
        {
           


          /* if (_shieldsLife == 3)
            {
               
            }
           else if(_shieldsLife == 2)
            {
                GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 100);
            }
           else if (_shieldsLife == 1 )
            {
                GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 50);
            }*/


           switch(_shieldsLife)
            {
                case 1:
                    GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 0);
                    break;


                case 2:
                    GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 70);
                    break;

                case 3:
                    GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 120);
                    break;

                case 4:
                    GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 170);
                    break;
            }
                
           
               
            
            if (_shieldsLife < 5 && _shieldsLife > 0 )
            {    
                _shieldsLife--;
            }


           /* if (_shieldsLife == 0)
            {
                _shieldsLife = 0;
            }*/
        }
    }
}
