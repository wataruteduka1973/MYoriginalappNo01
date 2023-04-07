using System;
using System.Windows;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Windows.Controls;

namespace MYoriginalappNo01
{
    /// <summary>
    /// ReadMe.xaml の相互作用ロジック
    /// </summary>
    public partial class ReadMe : Window
    {
        public ReadMe()
        {
            InitializeComponent(); 
        }
        private void Readmee(object sender, EventArgs e)
        {
            string filePath1 = @"readme.txt";
            string filePath2 = @"UserName.txt";
            Subject.Text = String.Join(Environment.NewLine, File.ReadAllLines(filePath1));
            hidden.Text = String.Join("", File.ReadAllLines(filePath2));
        }

        private void close(object sender, EventArgs e)
        {
            this.Close();
            if (String.IsNullOrWhiteSpace(hidden.Text))
            {
                UserSetting newWindow = new UserSetting();
                newWindow.Show();
            }


        }
        
        #region "最大化・最小化・閉じるボタンの非表示設定"

            [DllImport("user32.dll")]
            private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

            [DllImport("user32.dll")]
            private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

            const int GWL_STYLE = -16;
            const int WS_SYSMENU = 0x80000;

            protected override void OnSourceInitialized(EventArgs e)
            {
                base.OnSourceInitialized(e);
                IntPtr handle = new WindowInteropHelper(this).Handle;
                int style = GetWindowLong(handle, GWL_STYLE);
                style = style & (~WS_SYSMENU);
                SetWindowLong(handle, GWL_STYLE, style);
            }

        #endregion

    }
}
