using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monument : MonoBehaviour
{
    [SerializeField] GameObject textPanelPrefab;
    [HideInInspector] public TextPanel localTextPanel = null;
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
            Vector3 directionBetweenUserAndMonument = transform.position - GlobalReferences.instance.localUser.transform.position;
            localTextPanel = Instantiate(textPanelPrefab, transform.position + -transform.right * 0.35f + transform.up * 0.15f, transform.rotation).GetComponent<TextPanel>();
            localTextPanel.transform.SetParent(transform.GetChild(0).transform);
            localTextPanel.Initialize(textPanelTitle, textPanelBody, textPanelImage, transform.GetChild(0).transform);
        }
        localTextPanel.gameObject.SetActive(true);
        localTextPanel.PlayAnimation();
    }

    private void HideTextPanel()
    {
        if(localTextPanel != null)
        {
            localTextPanel.gameObject.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        gazeControl.OnMonumentActivated -= ShowTextPanel;
        gazeControl.OnMonumentDeactivated -= HideTextPanel;
    }
}
