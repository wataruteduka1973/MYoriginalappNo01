using System;
using System.Windows;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.IO;



namespace MYoriginalappNo01
{
    /// <summary>
    /// UserSetting.xaml の相互作用ロジック
    /// </summary>
    public partial class UserSetting : Window
    {
        public UserSetting()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.IsEnabled = true;
            foreach (Window window in Application.Current.Windows)
            {
                if (window != this)
                {
                    window.IsEnabled = false;
                }
            }

        }
        #region 閉じるボタンのイベント
        private void FinishWindow(object sender, EventArgs e)
        {
            system fileIo = new system();
            fileIo.UserName(nameset.Text);
            if (String.IsNullOrWhiteSpace(nameset.Text))
            {
                MessageBox.Show("名前を入力してください。", ":⚠:警告:⚠:", MessageBoxButton.OK);
            }
            else
            {
                this.Close();
                AccountSettings newWindow = new AccountSettings();
                newWindow.Show();
                foreach (Window window in Application.Current.Windows)
                {
                    if (window != this)
                    {
                        window.IsEnabled = true;
                    }
                }
            }
        }
        #endregion
        private void Namebox(object sender, EventArgs e)
        {
            string filePath = @"UserName.txt";
            nameset.Text = String.Join("", File.ReadAllLines(filePath));
            if (!String.IsNullOrWhiteSpace(nameset.Text))
            {
                this.Close();
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
