using TMPro;
using UnityEngine;
using System.Collections;

public class TutorialManager : MonoBehaviour
{
    public TutorialFloor tutorialFloor;
    public GameObject tutorialPanel;
    public TMP_Text tutorialText;

    [Header("Tutorial Arrows")]
    public GameObject leftArrow;
    public GameObject rightArrow;
    public GameObject upArrow;
    public GameObject downArrow;

    private PlayerController player;
    private PlayerAnimation playerAnimation;

    public bool waitingForTutorial = true;

    [Header("Intro")]
    public GameObject introPanel;

    private void Start()
    {
        player = FindFirstObjectByType<PlayerController>();
        playerAnimation = FindFirstObjectByType<PlayerAnimation>();

        HideAllArrows();

        StartCoroutine(ShowIntro());
    }

    void HideAllArrows()
    {
        leftArrow.SetActive(false);
        rightArrow.SetActive(false);
        upArrow.SetActive(false);
        downArrow.SetActive(false);
    }

    public enum TutorialStep
    {
        None,
        MoveLeft,
        MoveRight,
        AvoidObstacle,
        Jump,
        Roll
        
    }

    public TutorialStep currentStep;

    public void StopFloor(string message, TutorialStep step)
    {
       
        tutorialFloor.canMove = false;

        if (MusicFade.Instance != null)
        {
            MusicFade.Instance.LowerVolume();
        }

        waitingForTutorial = false;
        playerAnimation.StopRun();

        tutorialPanel.SetActive(true);
        tutorialText.text = message;

        HideAllArrows();

        currentStep = step;

        switch (step)
        {
            case TutorialStep.MoveLeft:
                leftArrow.SetActive(true);
                break;

            case TutorialStep.MoveRight:
                rightArrow.SetActive(true);
                break;

            case TutorialStep.AvoidObstacle:
                StartCoroutine(AlternateArrows());
                break;

            case TutorialStep.Jump:
                upArrow.SetActive(true);
                break;

            case TutorialStep.Roll:
                downArrow.SetActive(true);
                break;
        }

       
    }

    public void ResumeFloor()
    {
        
        tutorialFloor.canMove = true;

        waitingForTutorial = true;
        playerAnimation.ResumeRun();

        StopAllCoroutines();

        HideAllArrows();

        if (MusicFade.Instance != null)
        {
            MusicFade.Instance.RestoreVolume();
        }

        tutorialPanel.SetActive(false);
    }

    public void CheckMoveLeft()
    {
        if (currentStep == TutorialStep.MoveLeft)
        {
            ResumeFloor();
            currentStep = TutorialStep.None;
        }
    }

    public void CheckMoveRight()
    {
        if (currentStep == TutorialStep.MoveRight)
        {
            ResumeFloor();
            currentStep = TutorialStep.None;
        }
    }

    public void CheckObstacle()
    {
        if (currentStep == TutorialStep.AvoidObstacle)
        {
            ResumeFloor();
            currentStep = TutorialStep.None;
        }
    }

    public void CheckAvoidObstacle()
    {
        if (currentStep == TutorialStep.AvoidObstacle)
        {
            ResumeFloor();
            currentStep = TutorialStep.None;
        }
    }

    public void CheckJump()
    {
        if (currentStep == TutorialStep.Jump)
        {
            ResumeFloor();
            currentStep = TutorialStep.None;
        }
    }

    public void CheckRoll()
    {
        if (currentStep == TutorialStep.Roll)
        {
            ResumeFloor();
            currentStep = TutorialStep.None;
        }
    }

    public bool IsTutorialPaused()
    {
        return tutorialPanel.activeSelf;
    }

    IEnumerator AlternateArrows()
    {
        while (currentStep == TutorialStep.AvoidObstacle)
        {
            leftArrow.SetActive(true);
            rightArrow.SetActive(false);

            yield return new WaitForSeconds(0.5f);

            leftArrow.SetActive(false);
            rightArrow.SetActive(true);

            yield return new WaitForSeconds(0.5f);
        }

        HideAllArrows();
    }

    IEnumerator ShowIntro()
    {
        introPanel.SetActive(true);

        yield return new WaitForSeconds(3f);

        introPanel.SetActive(false);
    }
}
