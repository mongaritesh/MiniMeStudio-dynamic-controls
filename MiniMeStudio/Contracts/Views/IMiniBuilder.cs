using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace MiniMeStudio.Contracts.Views
{
    public interface IMiniBuilder
    {
        Frame GetNavigationFrame();

        void ShowWindow();

        void CloseWindow();
    }
}
