using UnityEngine;

public class MobileOptimizer : MonoBehaviour
{
    [Header("Quality Settings")]
    [SerializeField] private bool autoDetectDevice = true;
    [SerializeField] private int targetFPS = 60;
    
    [Header("Performance Levels")]
    [SerializeField] private QualityLevel lowEndSettings;
    [SerializeField] private QualityLevel midEndSettings;
    [SerializeField] private QualityLevel highEndSettings;
    
    [System.Serializable]
    public class QualityLevel
    {
        public int qualityLevel = 0;
        public int textureQuality = 0;
        public bool enableShadows = true;
        public ShadowResolution shadowResolution = ShadowResolution.Medium;
        public int antiAliasing = 0;
    }
    
    private void Start()
    {
        if (autoDetectDevice)
        {
            DetectAndApplyOptimalSettings();
        }
        
        Application.targetFrameRate = targetFPS;
        
        // Prevent screen dimming
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
    
    private void DetectAndApplyOptimalSettings()
    {
        // Simple device classification based on memory and processor count
        int memoryMB = SystemInfo.systemMemorySize;
        int processorCount = SystemInfo.processorCount;
        string deviceModel = SystemInfo.deviceModel.ToLower();
        
        if (IsLowEndDevice(memoryMB, processorCount, deviceModel))
        {
            ApplyQualitySettings(lowEndSettings);
        }
        else if (IsHighEndDevice(memoryMB, processorCount, deviceModel))
        {
            ApplyQualitySettings(highEndSettings);
        }
        else
        {
            ApplyQualitySettings(midEndSettings);
        }
    }
    
    private bool IsLowEndDevice(int memoryMB, int processorCount, string deviceModel)
    {
        return memoryMB < 2048 || processorCount < 4;
    }
    
    private bool IsHighEndDevice(int memoryMB, int processorCount, string deviceModel)
    {
        return memoryMB > 6144 && processorCount >= 8;
    }
    
    private void ApplyQualitySettings(QualityLevel settings)
    {
        QualitySettings.SetQualityLevel(settings.qualityLevel);
        QualitySettings.masterTextureLimit = settings.textureQuality;
        QualitySettings.shadows = settings.enableShadows ? ShadowQuality.All : ShadowQuality.Disable;
        QualitySettings.shadowResolution = settings.shadowResolution;
        QualitySettings.antiAliasing = settings.antiAliasing;
        
        Debug.Log($"Applied quality settings: Quality Level {settings.qualityLevel}");
    }
    
    public void SetLowQuality()
    {
        ApplyQualitySettings(lowEndSettings);
    }
    
    public void SetMidQuality()
    {
        ApplyQualitySettings(midEndSettings);
    }
    
    public void SetHighQuality()
    {
        ApplyQualitySettings(highEndSettings);
    }
}