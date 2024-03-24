using System;

namespace Template.Scripts
{
    [Flags]
    public enum LogLevel
    {
        None = 0,
        Error = 1 << 0,
        Assert = 1 << 1,
        Warning = 1 << 2,
        Log = 1 << 3,
        Exception = 1 << 4,
        All = Error | Assert | Warning | Log | Exception
    }
    
    public enum SceneType
    {
        Load,
        Game,
    }
    
    public enum TransitionType
    {
        Fade,
        Door,
    }

    public enum PanelType
    {
        Dev,
        OpenDev,
        OpenSettings,
        Settings, 
        Win, 
        Lose, 
        Level, 
        Money, 
        Restart,
        EndContinue,
        Shop,
        OpenShop,
        DailyRewards,
        OpenDailyRewards,
    }

    public enum LevelTextType
    {
        Level,
        LevelCompleted,
        LevelFailed,
    }
    
    public enum PoolType
    {
        
    }

    public enum ParticlePoolType
    {
        
    }
    
    public enum SkinRarity
    {
        Standard,
        Vip,
        Epic
    }

    public enum IncomeTextType
    {
        Win,
    }

    public enum CameraType
    {
        Menu,
    }

    public enum AudioType
    {
        
    }

    public enum EconomyType
    {
        Money,
        Coin,
    }
}