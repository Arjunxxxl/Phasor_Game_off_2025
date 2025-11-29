using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour
{
    [Header("Ui - Data items")] 
    [SerializeField] private TMP_Text titleTxt;
    [SerializeField] private TMP_Text descriptionTxt;
    [SerializeField] private Button continueButton;
    
    private Canvas canvas;
    private InfoPanelManager infoPanelManager;

    #region SetUp

    public void SetUp()
    {
        canvas = GetComponent<Canvas>();
        infoPanelManager = InfoPanelManager.Instance;
        
        continueButton.onClick.RemoveAllListeners();
        continueButton.onClick.AddListener(OnClickContinueButton);
        
        HideMenu();
    }

    #endregion

    #region Show Hide

    public void ShowMenu()
    {
        canvas.enabled = true;
    }

    public void HideMenu()
    {
        canvas.enabled = false;
    }

    #endregion

    #region Setting Data

    public void SetPanelData(InfoPanelData.Data infoPanelData)
    {
        titleTxt.text = infoPanelData.Title;
        descriptionTxt.text = infoPanelData.Description;
    }

    #endregion

    #region Button

    private void OnClickContinueButton()
    {
        infoPanelManager.InfoPanelHidden();
        HideMenu();
    }

    #endregion
}
