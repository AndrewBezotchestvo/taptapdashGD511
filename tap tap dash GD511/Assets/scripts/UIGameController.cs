using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIGameController : MonoBehaviour
{
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private Text score;
    [SerializeField] private Text record;
    [SerializeField] private Transform _player;
    [SerializeField] private AudioSource _music;

    private float _scoreValue;
    private float _recordValue;

    private bool _isPaused;

    private void Start()
    {
        _isPaused = false;

        _scoreValue = 0;
        _recordValue = PlayerPrefs.GetFloat("Record", 0);

        score.text = _scoreValue.ToString();
        record.text = _recordValue.ToString();
    }

    private void Update()
    {
        if (_player.position.x + _player.position.z >= 0)
        {
            _scoreValue = _player.position.x + _player.position.z;
        }
        if (_scoreValue > _recordValue)
        {
            _recordValue = Mathf.RoundToInt(_scoreValue);
            PlayerPrefs.SetFloat("Record", _recordValue);
            PlayerPrefs.Save();
        }

        score.text = Mathf.RoundToInt(_scoreValue).ToString();
        record.text = _recordValue.ToString();

        if (_isPaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ChangePause();
        }

        _pausePanel.SetActive(_isPaused);
    }

    public void ChangePause()
    {
        _isPaused = !_isPaused;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToMenuGame()
    {
        SceneManager.LoadScene(0);
    }

    public void SwitchSound(bool state)
    {
        if (state)
        {
            _music.volume = 0.5f;
        }
        else
        {
            _music.volume = 0f;
        }
    }
}
