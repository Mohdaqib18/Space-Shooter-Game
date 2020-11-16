using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class Enemy : MonoBehaviour
{   [SerializeField]
    private float _speed = 4.5f;

    private Player _player;
    


    private Animator _anim;
    private AudioSource _audioSource;
    [SerializeField]
    private GameObject _enemyLaserPrefab;
    private float _fireRate =4f;
    private float _canFire =-1f;
   


    // Start is called before the first frame update
    void Start()
    {
       _player = GameObject.Find("Player").GetComponent<Player>();
        _anim = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();

        if (_player == null)
        {
            Debug.LogError("Player is NULL");

        }

        if (_anim == null)
        {
            Debug.LogError("Animator is NULL");
        }
        if (_audioSource == null)
        {
            Debug.LogError("Audiosource on the enemy is NULL");
        }
    }

   

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (Time.time > _canFire)
        {
            _canFire = Time.time + _fireRate;
           GameObject enemyLaserShot = Instantiate(_enemyLaserPrefab, transform.position, Quaternion.identity);
            Laser[] lasers = enemyLaserShot.GetComponentsInChildren<Laser>();
            for(int i= 0 ; i  < lasers.Length ; i++ )
            {
                lasers[i].AssignEnemyLaser();
            }

        }


    }

    private void CalculateMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -5.4f)
        {
            float randomX = Random.Range(-9.4f, 9.4f);
            transform.position = new Vector3(randomX, 6.95f, 0);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
           Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }

            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _audioSource.Play();
            Destroy(this.gameObject , 2.8f);
        }

        if (other.tag == "Laser" )
        {
            Destroy(other.gameObject);

            if (_player !=null)
            { 
                _player.AddScore(10);
            }

            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _audioSource.Play();
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject , 2.7f);
         }
    }


    
        
}
