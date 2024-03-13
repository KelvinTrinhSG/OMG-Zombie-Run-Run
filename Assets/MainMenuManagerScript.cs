using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManagerScript : MonoBehaviour
{
    public GameObject tutorialBtn;
    public GameObject letGoBtn;
    public GameObject mainPanel;
    public GameObject tutorialPanel;
    public GameObject playerInputPanel;

    // Start is called before the first frame update
    void Start()
    {
        letGoBtn.SetActive(false);
        tutorialBtn.SetActive(true);
        tutorialPanel.SetActive(false);
        playerInputPanel.SetActive(false);
    }

    public void ChangeToPlayerInputPanel() {
        mainPanel.SetActive(false);
        playerInputPanel.SetActive(true);
    }

    public void GotoTutorialPanel() {
        tutorialPanel.SetActive(true);
        mainPanel.SetActive(false);
    }

    public void GobackToMainMenu() {
        tutorialPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    public void LoadScenePLayingGame() {
        SceneManager.LoadScene(1);
    }

}
