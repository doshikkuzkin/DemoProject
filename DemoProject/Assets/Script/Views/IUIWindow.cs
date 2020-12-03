using System;

namespace Script.Views
{
    public interface IUIWindow
    {
        void ShowWindow();
        void HideWindow();

        event Action OnStartButtonPressed;
    }
}