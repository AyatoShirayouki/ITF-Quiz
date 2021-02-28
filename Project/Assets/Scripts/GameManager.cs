using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Globalization;
using System.Resources;

public class GameManager : MonoBehaviour {



    public static int level = 1;
    public static int points = 0;
    public static int strikes = 0;

    public Question[] questions;
    private static List<Question> unansweredQuestions;
    private Question currentQuestion;


    //[SerializeField]
    //private Text Points;
    

    [SerializeField]
    private Text factText;

    [SerializeField]
    public Text StrikesText;

    [SerializeField]
    public Text pointsText;

    [SerializeField]
    private Text trueAnswerText;

    [SerializeField]
    private Text falseAnswerText;

    [SerializeField]
    private float timeBetweenQuestions = 1f;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    public Text LevelText;

    void Start()
    {
        if (unansweredQuestions == null || unansweredQuestions.Count == 0)
        {
            unansweredQuestions = questions.ToList<Question>();
        }

        SetCurrentQuestion();

        
    }
   
    void SetCurrentQuestion()
    {
        int randomQuestionIndex = Random.Range(0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[randomQuestionIndex];

        if (currentQuestion.isTrue)
        {
            trueAnswerText.text = "CORRECT";
            falseAnswerText.text = "WRONG";
        }
        else
        {
            trueAnswerText.text = "WRONG";
            falseAnswerText.text = "CORRECT";
        }
        factText.text = currentQuestion.fact;
        pointsText.text = "Point:" + points.ToString() + "/10";
        LevelText.text = "Level:" + level.ToString();
        StrikesText.text = "Strikes:" + strikes.ToString() + "/3";
    }

    IEnumerator transitionToNextQuestion()
    {
        unansweredQuestions.Remove(currentQuestion);
        yield return new WaitForSeconds(timeBetweenQuestions);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void UserSelectTrue()
    {
        animator.SetTrigger("True");
        if (currentQuestion.isTrue)
        {
            Debug.Log("Correct");
            points += 1;
            pointsText.text = "Point:" + points.ToString() + "/10";
            Debug.Log(points.ToString());
            if (points == 10)
            {
                points = 0;
                level += 1;
                LevelText.text = "Level:" + level.ToString();
                if (level == 3)
                {
                    SceneManager.LoadScene(2);
                }
            }
        }
        else
        {
            Debug.Log("Wrong");
            strikes += 1;
            StrikesText.text = "Strikes:" + strikes.ToString() + "/3";
            if (strikes >= 3)
            {
                SceneManager.LoadScene(3);
            }
        }
        StartCoroutine(transitionToNextQuestion());
    }

    public void UserSelectFalse()
    {
        animator.SetTrigger("False");
        if (!currentQuestion.isTrue)
        {
            Debug.Log("Correct");
            points += 1;
            pointsText.text = "Point:" + points.ToString() + "/10";
            Debug.Log(points.ToString());
            if (points == 10)
            {
                points = 0;
                level += 1;
                LevelText.text = "Level:" + level.ToString();
                if (level == 3)
                {
                    SceneManager.LoadScene(2);
                }
            }
        }
        else
        {
            Debug.Log("Wrong");
            strikes += 1;
            StrikesText.text = "Strikes:" + strikes.ToString() + "/3";
            if (strikes >= 3)
            {
                SceneManager.LoadScene(3);
            }
        }
        StartCoroutine(transitionToNextQuestion());
    }
}
