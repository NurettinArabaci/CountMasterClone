using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    
    Button playBut, restartBut, nextLevelBut;

    public Text scoreText,totalScore;

    GameObject scoreParent;

    [SerializeField] Text levelText;

    public static ButtonController Instance;


    private void Awake()
    {
        Instance = this;
        ButtonReference();
        scoreParent = scoreText.transform.parent.gameObject;
    }

    private void Start()
    {
        levelText.GetComponent<Text>().text = Tags.Level+" " + (PlayerPrefs.GetInt(Tags.Level) +1).ToString();

    }

    private void OnEnable()
    {
        EventManager.OnGameOver += OpenRestartButton;

        EventManager.LevelCompleted += OpenNextButton;
    }
    private void OnDisable()
    {
        EventManager.OnGameOver -= OpenRestartButton;

        EventManager.LevelCompleted -= OpenNextButton;
    }

    public void PlayButton()
    {
        playBut.gameObject.SetActive(false);
        scoreParent.SetActive(true);

        EventManager.Fire_OnStartMovement();
    }

    void OpenRestartButton()
    {
        restartBut.gameObject.SetActive(true);
    }

    public void RestartButton()
    {
        restartBut.gameObject.SetActive(false);
        LevelController.Instance.RestartLevelButton();

    }

    void OpenNextButton()
    {
        nextLevelBut.gameObject.SetActive(true);
        scoreParent.SetActive(false);
    }

    public void NextLevelButton()
    {
        LevelController.Instance.NextLevelButton();
        nextLevelBut.gameObject.SetActive(false);
    }

    public void ButtonReference()
    {
        playBut = transform.GetChild(0).GetComponent<Button>();
        nextLevelBut = transform.GetChild(1).GetComponent<Button>();
        restartBut = transform.GetChild(2).GetComponent<Button>();

    }
}
