using System.Collections.Generic;
using UnityEngine;

public class HeartUi : MonoBehaviour
{
    [Header("UI")] 
    [SerializeField] private List<HeartIndicator> heartIndicators;
    private List<bool> isHeartActive;

    #region SetUp

    public void SetUp(int activeHeartCt)
    {
        isHeartActive = new List<bool>();
        for (int i = 0; i < heartIndicators.Count; i++)
        {
            bool isActive = i < activeHeartCt;
            isHeartActive.Add(isActive);

            heartIndicators[i].SetHeartActivation(isActive);
        }
    }

    #endregion

    #region Update hearts Ui

    public void UpdateHeartUi(int activeHeartCt)
    {
        for (int i = 0; i < heartIndicators.Count; i++)
        {
            bool isActive = i < activeHeartCt;
            bool wasActive = isHeartActive[i];

            if (isActive != wasActive)
            {
                isHeartActive[i] = isActive;
                heartIndicators[i].SetHeartActivation(isActive);
            }
        }
    }

    #endregion
}
