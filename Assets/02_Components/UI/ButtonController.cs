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


    public void PlayButton()
    {
        playBut.gameObject.SetActive(false);
        scoreParent.SetActive(true);

        GameStateEvent.Fire_OnChangeGameState(GameState.Play);
    }

    public void RestartButton()
    {
        restartBut.gameObject.SetActive(false);
        LevelManager.Instance.LevelCreated();

    }

    public void NextLevelButton()
    {
        nextLevelBut.gameObject.SetActive(false);
        LevelManager.Instance.LevelCreated();
    }

    public void ButtonReference()
    {
        playBut = transform.GetChild(0).GetComponent<Button>();
        nextLevelBut = transform.GetChild(1).GetComponent<Button>();
        restartBut = transform.GetChild(2).GetComponent<Button>();

    }
}
