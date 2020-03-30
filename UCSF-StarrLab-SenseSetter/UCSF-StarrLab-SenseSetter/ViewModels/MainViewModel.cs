using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCSF_StarrLab_SenseSetter.ViewModels
{
    public partial class MainViewModel : Screen
    {
        /// <summary>
        /// Event called when window is closed by user originally called in Bootstrapper upon closing
        /// </summary>
        public static void WindowClosing()
        {
            Environment.Exit(0);
        }
    }
}
