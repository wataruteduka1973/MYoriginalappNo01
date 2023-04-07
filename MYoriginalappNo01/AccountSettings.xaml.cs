using System;
using System.Windows;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.IO;

namespace MYoriginalappNo01
{
    /// <summary>
    /// AccountSettings.xaml の相互作用ロジック
    /// </summary>
    public partial class AccountSettings : Window
    {
        public AccountSettings()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
        #region 読み込み時の処理
        private void loadExample3(object sender, RoutedEventArgs e)
        {
            DateTime dt = DateTime.Now;
            string filePath = @"TsubasaMailAddress.txt";
            MicrosoftMailAdress.Text = String.Join("\r\n", File.ReadAllLines(filePath));
            string filePath2 = @"JobcanPass.txt";
            JobcanPassword.Password = String.Join("\r\n", File.ReadAllLines(filePath2));
            string filePath3 = @"MicrosoftPass.txt";
            MicrosoftPassword.Password = String.Join("\r\n", File.ReadAllLines(filePath3));
        }
        #endregion

        #region ページを閉じるときの処理
        private void Finish(object sender, EventArgs e)
        {
            system fileIo = new system();

            fileIo.TsubasaMailAddress(MicrosoftMailAdress.Text);
            fileIo.JobcanPass(JobcanPassword.Password);
            fileIo.MicrosoftPass(MicrosoftPassword.Password);
            if (String.IsNullOrWhiteSpace(MicrosoftMailAdress.Text) || String.IsNullOrWhiteSpace(JobcanPassword.Password) || String.IsNullOrWhiteSpace(MicrosoftPassword.Password))
            {
                MessageBox.Show("アカウント情報を入力してください。", ":⚠:警告:⚠:", MessageBoxButton.OK);
            }
            else
            {
                this.Close();
            }

        }
        #endregion

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
