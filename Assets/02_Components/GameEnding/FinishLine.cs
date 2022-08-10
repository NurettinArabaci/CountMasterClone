using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.Player))
        {
            EventManager.Fire_OnFinishArea();

            ScoreIncrease();

            other.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    void ScoreIncrease()
    {
        PlayerPrefs.SetInt(Tags.Score, PlayerPrefs.GetInt(Tags.Score) + StackController.scoreAmount);
        ButtonController.Instance.totalScore.text = PlayerPrefs.GetInt(Tags.Score).ToString();
        StackController.scoreAmount = 0;
    }
}
