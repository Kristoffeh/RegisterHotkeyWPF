using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WindowsInput;
using WindowsInput.Native;
using WindowsInput.Events;
using WindowsInput.Events.Sources;
using static WindowsInput.Native.SystemMetrics;
using System.ComponentModel;

namespace RegisterHotkeyWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IKeyboardEventSource? m_Keyboard;

        public MainWindow()
        {
            InitializeComponent();
            Subscribe();
        }

        private void Subscribe()
        {
            var Keyboard = default(IKeyboardEventSource);
            Keyboard = WindowsInput.Capture.Global.Keyboard();
            SubscribeKB(Keyboard);
        }

        private void SubscribeKB(IKeyboardEventSource Keyboard)
        {
            this.m_Keyboard?.Dispose();
            this.m_Keyboard = Keyboard;

            if(Keyboard != default)
            {
                Keyboard.KeyEvent += this.Keyboard_KeyEvent;
            }
        }

        private void Keyboard_KeyEvent(object? sender,EventSourceEventArgs<KeyboardEvent> e)
        {
            string prefKey = "G";

            if(Enum.TryParse(prefKey,out KeyCode key))
            {
                if(e.Data.KeyDown?.Key == key)
                {
                    txtLog.Text += $"{e.Data.KeyDown}\r\n";
                }
            }
            else
            {
                if(e.Data.KeyDown?.Key == KeyCode.K)
                {
                    txtLog.Text += $"{e.Data.KeyDown}\r\n";
                }
            }
        }

        private void Window_Closing(object sender,System.ComponentModel.CancelEventArgs e)
        {
            Unsubscribe();
        }

        private void Unsubscribe()
        {
            m_Keyboard?.Dispose();
            m_Keyboard = null;
        }

        private void Window_MouseMove(object sender,MouseEventArgs e)
        {
            lblMouse.Content = $"{e.GetPosition(this)}";
        }
    }
}
