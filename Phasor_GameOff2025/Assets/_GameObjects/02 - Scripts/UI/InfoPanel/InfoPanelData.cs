using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InfoPanelData", menuName = "ThisGame/Data/InfoPanelData")]
public class InfoPanelData : ScriptableObject
{
    [System.Serializable]
    public class Data
    {
        public string Title;
        public string Description;
    }
    
    [SerializeField] private List<Data> infoPanelData;

    public Data GetInfoPanelData(int index)
    {
        if (index >= infoPanelData.Count)
        {
            return null;
        }
        
        return infoPanelData[index];
    }
}
