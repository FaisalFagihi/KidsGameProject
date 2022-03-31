using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public TextMeshProUGUI question;
    public Image goalImage;
    public TextMeshProUGUI option1;
    public TextMeshProUGUI option2;
    public TextMeshProUGUI option3;
    public Image option1Image;
    public Image option2Image;
    public Image option3Image;
    public List<Level> levels;
    private Level currentLevel;
    public TextMeshProUGUI levelNumber;
    public TextMeshProUGUI Result;

    private int levelCounter;
    private float timeLeft;
    public float timeBetweenLevelsInSeconds = 1;
    private bool hasWon;
    private bool hasSelected;

    // Start is called before the first frame update
    void Start()
    {
        timeLeft = timeBetweenLevelsInSeconds;
        GoNextLevel();
        
        option1Image.preserveAspect = true;
        option2Image.preserveAspect = true;
        option3Image.preserveAspect = true;
    }

    private void GoNextLevel()
    {
        hasWon = false;
        timeLeft = timeBetweenLevelsInSeconds;
        if (levelCounter >= levels.Count)
        {
            levelCounter = 0;
        }
        Reset();

        levelNumber.text = $"{++levelCounter}/{levels.Count}";
        currentLevel = levels[levelCounter - 1];
        question.text = ArabicSupport.Fix(currentLevel.question);
        if (currentLevel.hasSpriteOptions)
        {
            option1Image.gameObject.SetActive(true);
            option1Image.sprite = currentLevel.option1Sprite;
            option2Image.gameObject.SetActive(true);
            option2Image.sprite = currentLevel.option2Sprite;
            option3Image.gameObject.SetActive(true);
            option3Image.sprite = currentLevel.option3Sprite;

        }
        else
        {
            option1Image.gameObject.SetActive(false);
            option2Image.gameObject.SetActive(false);
            option3Image.gameObject.SetActive(false);

            option1.text = ArabicSupport.Fix(currentLevel.option1);
            option2.text = ArabicSupport.Fix(currentLevel.option2);
            option3.text = ArabicSupport.Fix(currentLevel.option3);

        }
        if (currentLevel.goalSprite == null)
        {
            question.rectTransform.anchoredPosition = new Vector2(question.rectTransform.anchoredPosition.x, -300);
            goalImage.gameObject.SetActive(false);
            return;
        }
        else
        {
            question.rectTransform.anchoredPosition = new Vector2(question.rectTransform.anchoredPosition.x, -100);
            goalImage.gameObject.SetActive(true);
            goalImage.sprite = currentLevel.goalSprite;
        }


    }

    private void Reset()
    {
        option1Image.sprite = null;
        option2Image.sprite = null;
        option3Image.sprite = null;
        option1.text = string.Empty;
        option2.text = string.Empty;
        option3.text = string.Empty;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasSelected)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                Result.text = string.Empty;
                if (hasWon)
                {
                    GoNextLevel();
                }
            }
        }

    }
    public void SelectOption(int option)
    {
        if (hasWon)
        {
            return;
        }
        hasSelected = true;
        timeLeft = timeBetweenLevelsInSeconds;
        if (option == currentLevel.answer)
        {
            hasWon = true;
            Result.color = Color.green;
            Result.text = ":)";
            return;
        }
        Result.color = Color.red;
        Result.text = ":(";
    }
}
