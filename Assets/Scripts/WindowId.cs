public enum WindowId
{
    None    = 0,
    Login   = 1,
    Main    = 2,
    Mail    = 3,
    Bag     = 4,
    Chat    = 5,
    Battle  = 6,
    Chapter = 7,
    Result  = 8,
    /// <summary>
    /// 小loading,主动调用开关
    /// </summary>
    Loading = 9,
    /// <summary>
    /// 大Loading,底层调用开，根据逻辑，自己界面准备完成后，执行translationComplete由底层负责关闭
    /// </summary>
    Translation = 10
}

public class WindowConfig
{
    public WindowId windowId;
    public bool donotDestroy;
    public bool needTranslation;
}