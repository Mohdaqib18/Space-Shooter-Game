using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;
    private int _speedMultiplier = 2;

    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private GameObject _shieldsVisualiser;
    [SerializeField]
    private GameObject _leftEngine, _rightEngine;
    [SerializeField]
    private AudioClip _laserShotAudioClip;
    private AudioSource _audioSource;


    [SerializeField]
    private float _fireRate = 0.15f;
    private float _canFire = -1f;

    [SerializeField]
    private int _lives = 3;

    [SerializeField]
    private int _score;

    private SpawnManager _spawnManager;
    private UIManager _uiManager;
    private Enemy _enemy;

    private bool _isTripleShotActive;
    private bool _isSpeedBoostActive;
    private bool _isShieldsActive;

   


    



    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
     
        _audioSource = GetComponent<AudioSource>();

       if (_spawnManager == null)
        {
            Debug.Log("SpawnManager is NULL");
        }

        if(_uiManager == null)
        {
            Debug.LogError("The SpawnManger is NULL");
        }

        if (_audioSource == null)
        {
            Debug.LogError("Audiosource is NULL");
        }
        else
        {
            _audioSource.clip = _laserShotAudioClip;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }


    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(direction * _speed * Time.deltaTime);


        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.93f, 0), 0);

        if (transform.position.x <= -11.33f)
        {
            transform.position = new Vector3(11.33f, transform.position.y, 0);
        }
        else if (transform.position.x >= 11.33f)
        {
            transform.position = new Vector3(-11.33f, transform.position.y, 0);
        }
    }

    void FireLaser()
    {
        _canFire = Time.time + _fireRate;



        if (_isTripleShotActive == true)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.15f, 0), Quaternion.identity);
        }

        _audioSource.Play();
  
    }


    public void Damage()
    {
        if (_isShieldsActive == true)
        {
            _isShieldsActive = false;
            _shieldsVisualiser.SetActive(false);
            return;
        }


        _lives--;

        if (_lives == 2)
        {
            _rightEngine.SetActive(true);
        }
        else if(_lives == 1)
        {
            _leftEngine.SetActive(true);
        }

        _uiManager.UpdateLives(_lives);

        if (_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());


    }


    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5);
        _isTripleShotActive = false;

    }

    public void SpeedBoostActive()
    {
        _speed *= _speedMultiplier;
        _isSpeedBoostActive = true;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5);      
        _isSpeedBoostActive = false;
        _speed /= _speedMultiplier;

    }
    public void ShieldsActive()
    {
       _isShieldsActive = true;
        _shieldsVisualiser.SetActive(true);
    }

    public void AddScore(int points)
    {
        _score += points;

        _uiManager.UpdateScore(_score);

    }
        

}
