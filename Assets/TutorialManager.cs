using TMPro;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public TutorialFloor tutorialFloor;
    public GameObject tutorialPanel;
    public TMP_Text tutorialText;

    private PlayerController player;
    private PlayerAnimation playerAnimation;

    private void Start()
    {
        player = FindFirstObjectByType<PlayerController>();
        playerAnimation = FindFirstObjectByType<PlayerAnimation>();
    }

    public enum TutorialStep
    {
        None,
        MoveLeft,
        MoveRight,
        AvoidObstacle
    }

    public TutorialStep currentStep;

    public void StopFloor(string message, TutorialStep step)
    {
        tutorialFloor.canMove = false;

       
        playerAnimation.StopRun();

        tutorialPanel.SetActive(true);
        tutorialText.text = message;

        currentStep = step;
    }

    public void ResumeFloor()
    {
        tutorialFloor.canMove = true;

       
        playerAnimation.ResumeRun();

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

    public bool IsTutorialPaused()
    {
        return tutorialPanel.activeSelf;
    }
}
