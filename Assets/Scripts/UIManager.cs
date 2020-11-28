using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{    [SerializeField]
    private Text _scoretext;
    [SerializeField]
    private Text _gameoverText;
    [SerializeField]
    private Image _LivesImage;
    [SerializeField]
    private Text _restartText;

    [SerializeField]
    private Sprite[] _LivesDisplay;

    private GameManager _gameManager;
    // Start is called before the first frame update
    void Start()
    {
        _scoretext.text = "Score: " + 0;
        _gameoverText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        if (_gameManager == null)
        {
            Debug.LogError("Game Manager is Null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore(int playerScore)
    {
        _scoretext.text = "Score: " + playerScore.ToString();
            
    }

    public void UpdateLives (int currentLives)
    {    
        if(currentLives < 4)
        {
            _LivesImage.sprite = _LivesDisplay[currentLives];
        }
        
         if (currentLives == 0)
        {
            GameOverSequence();
        }

    }


    public void GameOverSequence()
    {
        _gameManager.GameOver();
        _gameoverText.gameObject.SetActive(true);
        _restartText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlickerRoutine());
    }

    IEnumerator GameOverFlickerRoutine()
    {
        while(true)
        {
            _gameoverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _gameoverText.text = "";
            yield return new WaitForSeconds(0.5f);

        }

    }


}
