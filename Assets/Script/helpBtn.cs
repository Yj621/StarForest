using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class helpBtn : MonoBehaviour
{
    public GameObject helpPanel;

    void Start()
    {
        helpPanel.SetActive(false);

        Button helpBtn = GetComponent<Button>();
        helpBtn.onClick.AddListener(showHelpPanel);

        Button cancelButton = helpPanel.transform.Find("CancelButton").GetComponent<Button>();
        cancelButton.onClick.AddListener(HideHelpPanel);
    }

    private void showHelpPanel()
    {
        helpPanel.SetActive(true);
    }

    private void HideHelpPanel()
    {
        helpPanel.SetActive(false);
    }
}
