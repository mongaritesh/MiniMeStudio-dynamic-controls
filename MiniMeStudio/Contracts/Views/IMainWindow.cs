using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace MiniMeStudio.Contracts.Views
{
    public interface IMainWindow
    {
        Frame GetNavigationFrame();

        void ShowWindow();

        void CloseWindow();
    }
}
