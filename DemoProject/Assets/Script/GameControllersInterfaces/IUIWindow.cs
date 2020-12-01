using System;

namespace Script.GameControllersInterfaces
{
    public interface IUIWindow
    {
        void ShowWindow();
        void HideWindow();

        event Action OnCloseButtonPressed;
    }
}