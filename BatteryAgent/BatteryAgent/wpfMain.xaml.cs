using System;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Runtime.InteropServices;

namespace BatteryAgent
{
    /// <summary>
    /// Interaction logic for wpfMain.xaml
    /// </summary>
    public partial class wpfMain : Window
    {
        public wpfMain()
        {
            InitializeComponent();
        }

        #region Window styles
        [Flags]
        public enum ExtendedWindowStyles
        {
            // ...
            WS_EX_TOOLWINDOW = 0x00000080,
            // ...
        }

        public enum GetWindowLongFields
        {
            // ...
            GWL_EXSTYLE = (-20),
            // ...
        }

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowLong(IntPtr hWnd, int nIndex);

        public static IntPtr SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong)
        {
            int error = 0;
            IntPtr result = IntPtr.Zero;
            // Win32 SetWindowLong doesn't clear error on success
            SetLastError(0);

            if (IntPtr.Size == 4)
            {
                // use SetWindowLong
                Int32 tempResult = IntSetWindowLong(hWnd, nIndex, IntPtrToInt32(dwNewLong));
                error = Marshal.GetLastWin32Error();
                result = new IntPtr(tempResult);
            }
            else
            {
                // use SetWindowLongPtr
                result = IntSetWindowLongPtr(hWnd, nIndex, dwNewLong);
                error = Marshal.GetLastWin32Error();
            }

            if ((result == IntPtr.Zero) && (error != 0))
            {
                throw new System.ComponentModel.Win32Exception(error);
            }

            return result;
        }

        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr", SetLastError = true)]
        private static extern IntPtr IntSetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll", EntryPoint = "SetWindowLong", SetLastError = true)]
        private static extern Int32 IntSetWindowLong(IntPtr hWnd, int nIndex, Int32 dwNewLong);

        private static int IntPtrToInt32(IntPtr intPtr)
        {
            return unchecked((int)intPtr.ToInt64());
        }

        [DllImport("kernel32.dll", EntryPoint = "SetLastError")]
        public static extern void SetLastError(int dwErrorCode);

        private void hidefromlist()
        {
            WindowInteropHelper wndHelper = new WindowInteropHelper(this);

            int exStyle = (int)GetWindowLong(wndHelper.Handle, (int)GetWindowLongFields.GWL_EXSTYLE);

            exStyle |= (int)ExtendedWindowStyles.WS_EX_TOOLWINDOW;
            SetWindowLong(wndHelper.Handle, (int)GetWindowLongFields.GWL_EXSTYLE, (IntPtr)exStyle);
        }

        #endregion

        public bool critical = false;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            hidefromlist();
        }

        public void ShowSign()
        {
            media.Position = TimeSpan.FromMilliseconds(0);
            critical = true;
            this.BeginAnimation(Window.OpacityProperty,null);
            this.Opacity = 1;
            this.Visibility = Visibility.Visible;
            Critical.Visibility = Visibility.Visible;
            Safe.Visibility = Visibility.Hidden;
            media.Play();
        }

        public void HideSign()
        {
            critical = false;
            Critical.Visibility = Visibility.Hidden;
            Safe.Visibility = Visibility.Visible;
            media.Stop();

            System.Windows.Media.Animation.DoubleAnimation animate = new System.Windows.Media.Animation.DoubleAnimation();

            animate.BeginTime = TimeSpan.FromMilliseconds(1000);
            animate.Duration = TimeSpan.FromMilliseconds(500);
            animate.DecelerationRatio = 0;
            animate.AccelerationRatio =1;
            animate.To = 0;

            animate.Completed += new EventHandler(completed);

            this.BeginAnimation(Window.OpacityProperty , animate);
        }

        private void completed(object sender, EventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }

        private void media_MediaEnded(object sender, RoutedEventArgs e)
        {
            media.Position = TimeSpan.FromMilliseconds(0);
            media.Play();
        }
    }
}
