using UnityEngine;

public static class Constants
{
    public static class Player
    {
        public static readonly float LevelBelowYPos = -10.0f;
        
        public static readonly float HangingAxeHitSpeed = 15.0f;
        public static readonly float RotatingHammerHitSpeed = 15.0f;
        public static readonly Vector3 ObstacleHitSpeedDecrement = new Vector3(3.5f, 10.0f, 3.5f);
    }
    
    public static class PhaseData
    {
        public static readonly float PhaseActiveDuration = 5.0f;
        public static readonly float TimeShiftTimeSlow = 0.25f;
        public static readonly float MirrorImageDist = 5.5f;
    }

    public static class ObstacleData
    {
        public static readonly float HangingAxeRotationSpeed_Normal = 0.75f;
        public static readonly float HangingAxeRotationSpeed_Fast = 2.5f;
        
        public static readonly float RotatingHammerSpeed_Normal = 100f;
        public static readonly float RotatingHammerSpeed_Fast = 300f;
    }

    public static class LocalData
    {
        public static readonly string LevelNumber_Tag = "LevelNumber";
        public static readonly string CheckPointInfoShown_Tag = "CheckPointInfoShown";
        public static readonly string TimeShiftInfoShown_Tag = "TimeShiftInfoShown";
        public static readonly string AirInfoShown_Tag = "AirInfoShown";
        public static readonly string TotalPhasesUnlocked_Tag = "TotalPhasesUnlocked";
    }
}
