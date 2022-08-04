using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum OperatorTypes { Sum, Mult };

public class StackController : MonoBehaviour
{
    [SerializeField] private OperatorTypes sumOrMult;
    [SerializeField] private int operatorAmount;

    Transform operatorParent;

    TextMeshProUGUI operatorText, sembolText;    

    public static int playerChildAmount,scoreAmount;

    int sumNumber, multNumber;

    private void Awake()
    {
        VariableReferences();
        PlayerPrefs.GetInt("Score", 0);

        playerChildAmount = 1;
        operatorText.text = operatorAmount.ToString();
        sembolText.text = sumOrMult == OperatorTypes.Sum ? "+" : "x";
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.PlayerChild))
        {
            SumMultOperator();

            Spawn(other.transform.position+Vector3.right/10);


            
            for (int i = 0; i < operatorParent.childCount; i++)
                operatorParent.GetChild(i).gameObject.SetActive(false);
        }
    }


    public void SumMultOperator()
    {
        switch (sumOrMult)
        {
            case OperatorTypes.Sum:

                sumNumber = operatorAmount;
                playerChildAmount += sumNumber;
                operatorAmount = sumNumber;

                ScoreControl();

                PlayerCount.playerCount.text = playerChildAmount.ToString();

                break;

            case OperatorTypes.Mult:

                multNumber = operatorAmount;
                operatorAmount = (multNumber - 1) * playerChildAmount;
                playerChildAmount = multNumber * playerChildAmount;

                ScoreControl();

                PlayerCount.playerCount.text = playerChildAmount.ToString();

                break;
        }

    }

    public void Spawn(Vector3 pos)
    {
        
        for (int i = 0; i < operatorAmount; i++)
            ObjectPooling.Instance.GetSpawnObject(Tags.PlayerChild, pos, Quaternion.identity);


        EventManager.PlayerMove();
    }

    void VariableReferences()
    {        
        sembolText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        operatorText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        operatorParent = transform.parent;
    }

    void ScoreControl()
    {
        scoreAmount += operatorAmount;
        ButtonController.Instance.scoreText.text = scoreAmount.ToString();
    }

}