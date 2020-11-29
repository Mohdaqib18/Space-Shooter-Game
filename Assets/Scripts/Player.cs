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
    private CameraShake _cameraShake;


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
    private ShieldsVisualiser _shieldVisualiser;
   

    private bool _isTripleShotActive;
    private bool _isSpeedBoostActive;
    private bool _isShieldsActive;
    private bool _isHealthActive;
    private bool _isAmmoActive;

    [SerializeField]
    private List<GameObject> _laserPrefabLimit;
    private int _laserLimit = 15;
   


    



    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("UI_Manager").GetComponent<UIManager>();
         _shieldVisualiser = _shieldsVisualiser.transform.GetComponent<ShieldsVisualiser>();
     
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
        if(_shieldVisualiser == null)
        {
            Debug.Log("Sheild Visualiser in Null ");
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

        if(Input.GetKey(KeyCode.LeftShift))
        {
            transform.Translate(direction * _speed * 2 * Time.deltaTime);
        }
        else
        {
            transform.Translate(direction * _speed * Time.deltaTime);
        }

       


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


            if (_laserPrefabLimit.Count < _laserLimit )
            {
                _laserPrefabLimit.Add(Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.15f, 0), Quaternion.identity));
                AmmoLeft(_laserLimit);
            }
        }

        _audioSource.Play();
    }


    public void Damage()
    {
     

        if (_isShieldsActive == true && _shieldVisualiser._shieldsLife == 0 )
        {
            _isShieldsActive = false;
            _shieldsVisualiser.SetActive(false);
        }


        if(_shieldVisualiser._shieldsLife == 0 || _isShieldsActive == false)
        {
            _lives--;
            StartCoroutine(_cameraShake.ShakeRoutine(.15f , .4f));
        }



       

       

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
        _shieldsVisualiser.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
        _shieldVisualiser._shieldsLife = 4;

      
        
    }


    public void HealthActive()
    {
        _isHealthActive = true;
        
        if (_lives < 3)
        {
            _lives++ ;
            _uiManager.UpdateLives(_lives);
        }

        if (_lives == 3)
        {
            _leftEngine.SetActive(false);
            _rightEngine.SetActive(false);
        }
       
        else if (_lives == 2)
        {
            _leftEngine.SetActive(false);
            _rightEngine.SetActive(true);
        }

        else if (_lives == 1)
        {
            _rightEngine.SetActive(false);
            _leftEngine.SetActive(true);
        }

    }


    public void AmmoActive()
    {
        _isAmmoActive = true;
        _uiManager.UpdateAmmoCount(_laserLimit);
        _laserPrefabLimit.Clear();
      
        

    }

    public void AddScore(int points)
    {
        _score += points;

        _uiManager.UpdateScore(_score);

    }

    public void AmmoLeft (int _laserLimit)
    {
      
        _laserLimit -= _laserPrefabLimit.Count;

        _uiManager.UpdateAmmoCount(_laserLimit);

    }

  

}
