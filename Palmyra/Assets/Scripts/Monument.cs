using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monument : MonoBehaviour
{
    [SerializeField] GameObject textPanelPrefab;
    private TextPanel localTextPanel = null;
    public string textPanelTitle;
    public string textPanelBody;
    public Texture2D textPanelImage;

    private GazeControl gazeControl;


    private void Awake()
    {
        gazeControl = GetComponent<GazeControl>();
    }

    private void Start()
    {
        gazeControl.OnMonumentActivated += ShowTextPanel;
        gazeControl.OnMonumentDeactivated += HideTextPanel;
    }

    private void ShowTextPanel()
    {
        if(localTextPanel == null)
        {
            localTextPanel = Instantiate(textPanelPrefab, transform.position, transform.rotation).GetComponent<TextPanel>();
            localTextPanel.Initialize(textPanelTitle, textPanelBody, textPanelImage);
        }
        localTextPanel.gameObject.SetActive(true);
        localTextPanel.PlayAnimation();
    }

    private void HideTextPanel()
    {
        localTextPanel.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        gazeControl.OnMonumentActivated -= ShowTextPanel;
        gazeControl.OnMonumentDeactivated -= HideTextPanel;
    }
}
