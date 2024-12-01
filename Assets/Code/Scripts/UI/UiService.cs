using System;

public class UiService
{
    public event Action<UiType> Change;
    public UiType ActualUiType { get; private set; }

    public void SetUi(UiType type)
    {
        ActualUiType = type;
        Change?.Invoke(type);
    }
}