using UnityEngine;
using UnityEngine.UI;

public class PanelSwitch : MonoBehaviour
{
    public GameObject[] panels;

    public Button nextButton;
    public Button prevButton;

    private int currentIndex = 0;

    void Start()
    {
        ShowPanel(0); 
    }

    public void NextPanel()
    {
        if (currentIndex >= panels.Length - 1)
            return;

        currentIndex++;
        ShowPanel(currentIndex);
    }

    public void PreviousPanel()
    {
        if (currentIndex <= 0)
            return;

        currentIndex--;
        ShowPanel(currentIndex);
    }

    private void ShowPanel(int index)
    {
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(i == index);
        }

        UpdateButtons();
    }

    private void UpdateButtons()
    {
        if (prevButton != null)
            prevButton.interactable = currentIndex > 0;

        if (nextButton != null)
            nextButton.interactable = currentIndex < panels.Length - 1;
    }
}
