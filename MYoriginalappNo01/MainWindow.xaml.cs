using System;
using System.Windows;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Windows.Threading;
using System.Net;
using System.IO.Compression;





namespace MYoriginalappNo01
{
    
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;


        }

        #region ジョブカンのログイン
        //ジョブカンのオートログイン
        public void Jobcan(object sender, EventArgs e)
        {
            try
            {
                DateTime dt = DateTime.Now;
                string text1 = System.IO.File.ReadAllText(@"C:TsubasaMailAddress.txt");
                string text2 = System.IO.File.ReadAllText(@"C:JobcanPass.txt");
                IWebDriver driver = new ChromeDriver("C:\\webdriver");

                driver.Navigate().GoToUrl("https://id.jobcan.jp/users/sign_in");
                IWebElement searchtextbox = driver.FindElement(By.Id("user_email"));
                searchtextbox.SendKeys(text1);
                IWebElement searchtextbox2 = driver.FindElement(By.Id("user_password"));
                searchtextbox2.SendKeys(text2);

                driver.Navigate().GoToUrl("https://id.jobcan.jp/account/profile");
                driver.FindElement(By.Id("jbc-app-links")).Click();

                var handles = driver.WindowHandles;
                foreach (var handle in handles)
                {
                    driver.SwitchTo().Window(handle);
                    if (driver.Url == "https://ssl.jobcan.jp/employee")
                    {
                        break;
                    }
                }
                driver.FindElement(By.Id("adit-button")).Click();
                DispatcherTimer timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(5) };
                timer.Start();
                timer.Tick += (s, args) =>
                {
                    // タイマーの停止
                    timer.Stop();


                    driver.Quit();

                };
            }
            catch (Exception )
            {
                MessageBox.Show("seleniumの処理に問題が発生しました。\r\nドライバーが最新のブラウザに対応しているか確認してください", "エラー", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }
        #endregion

        #region レポートメールを作成
        public void FinishWork(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            string Username = System.IO.File.ReadAllText(@"C:Username.txt");
            //string subject = $"［レポート]{ResettingName.Text + dt.ToString("yyyy年MM月dd日 (ddd)")}";
            string subject = $"［レポート]{Username + dt.ToString("yyyy年MM月dd日 (ddd)")}";
            ProcessStartInfo pi = new ProcessStartInfo()
            {
                FileName = $"\"https://outlook.office.com/owa/?subject={Uri.EscapeDataString(subject)}&body=お疲れ様です。～のレポートが完成したので、ご確認ください。&to=manabiya-report@world-wing.com&path=/mail/action/compose\"",
                UseShellExecute = true,
            };
            Process.Start(pi);

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

        #region ドライバーのアップデート
        public void Selenium_Update(object sender, EventArgs e)
        {
            string path = @"C:\webdriver";
            if (!Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"フォルダの作成中にエラーが発生しました: {ex.Message}");
                    return;
                }
            }

            string driverPath = @"C:\webdriver\chromedriver.exe";
            if (File.Exists(driverPath))
            {
                try
                {
                    File.Delete(driverPath);
                    MessageBox.Show("既存のChromeドライバーを削除しました。");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Chromeドライバーの削除中にエラーが発生しました: {ex.Message}");
                    return;
                }
            }

            // 最新バージョンのChromeドライバーをダウンロードする
            string latestVersion = new WebClient().DownloadString("https://chromedriver.storage.googleapis.com/LATEST_RELEASE");
            string downloadUrl = $"https://chromedriver.storage.googleapis.com/{latestVersion}/chromedriver_win32.zip";
            string downloadPath = Path.Combine(Environment.CurrentDirectory, "chromedriver.zip");
            using (WebClient client = new WebClient())
            {
                try
                {
                    client.DownloadFile(downloadUrl, downloadPath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Chromeドライバーのダウンロード中にエラーが発生しました: {ex.Message}");
                    return;
                }
            }

            // ダウンロードしたファイルを解凍してインストールする
            string extractPath = Path.Combine(Environment.CurrentDirectory, "chromedriver");
            ZipFile.ExtractToDirectory(downloadPath, extractPath);
            string installPath = @"C:\webdriver";
            string driverInstallPath = Path.Combine(installPath, "chromedriver.exe");
            try
            {
                if (File.Exists(driverInstallPath))
                {
                    File.Delete(driverInstallPath);
                }
                File.Move(Path.Combine(extractPath, "chromedriver.exe"), driverInstallPath);
                MessageBox.Show($"Chromeドライバーをバージョン {latestVersion} に更新しました。");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Chromeドライバーのインストール中にエラーが発生しました: {ex.Message}");
                return;
            }
            finally
            {
                if (File.Exists(downloadPath))
                {
                    File.Delete(downloadPath);
                }
                if (Directory.Exists(extractPath))
                {
                    Directory.Delete(extractPath, true);
                }
            }


        }
        #endregion

        #region 起動時の処理
        private void User(object sender, RoutedEventArgs e)
        {
            string filePath = @"UserName.txt";
            ResettingName.Text = String.Join("", File.ReadAllLines(filePath));
            if (String.IsNullOrWhiteSpace(ResettingName.Text))
            {
                ReadMe sw = new ReadMe();
                sw.Show();
            }

        }
        #endregion

        #region 別ウィンドウ追加
        public void BeginWork(object sender, EventArgs e)
        {
            SMTPMails sw = new SMTPMails();
            sw.Show();

        }

        private void SettingWindow(object sender, RoutedEventArgs e)
        {
            AccountSettings sw = new AccountSettings();
            sw.Show();

        }

        private void test(object sender, RoutedEventArgs e)
        {
            ReadMe sw = new ReadMe();
            sw.Show();
        }

        #endregion

        #region 終了時の処理
        private void Close(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ResettingUser(object sender, RoutedEventArgs e)
        {
            system fileIo = new system();
           
            if (String.IsNullOrWhiteSpace(ResettingName.Text))
            {
                MessageBox.Show("名前を入力してください。", ":⚠:警告:⚠:", MessageBoxButton.OK);
            }
            else 
            {
            fileIo.UserName(ResettingName.Text);
            }
        }
        #endregion
    }
}

