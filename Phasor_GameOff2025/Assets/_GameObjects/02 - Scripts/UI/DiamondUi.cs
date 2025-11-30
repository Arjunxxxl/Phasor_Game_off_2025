using TMPro;
using UnityEngine;

public class DiamondUi : MonoBehaviour
{
    public TMP_Text diamondCtTxt;

    public void UpdateDiamondTxt(int val)
    {
        diamondCtTxt.text = val.ToString();
    }
}
