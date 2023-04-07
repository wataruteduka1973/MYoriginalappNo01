using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.IO;
using System.Net.Mail;

namespace MYoriginalappNo01
{
    /// <summary>
    /// SMTPMails.xaml の相互作用ロジック
    /// </summary>
    public partial class SMTPMails : Window
    {
        public SMTPMails()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        #region ウィンドウを閉じる
        private void FinishWorkExit(object sender, EventArgs e)
        {
            this.Close();
            system fileIo = new system();
            ComboBoxItem selectedItem = (ComboBoxItem)MailsBox.SelectedItem;
            fileIo.CC(CC.Text);
            if (selectedItem != null)
            {
                if (selectedItem.Name == "Dailyreport")
                {
                    fileIo.Dailyreport(Mailtext.Text);
                    MessageBox.Show("入力内容を保存しました");
                }
                else if (selectedItem.Name == "Leavingwork")
                {
                    fileIo.Leavingwork(Mailtext.Text);
                    MessageBox.Show("入力内容を保存しました");
                }
            }
            else
            {
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

        #region ドロップダウンの設定
        private void MailTemplate(object sender, SelectionChangedEventArgs e)
        {

            string username = null;
            using (var reader = new System.IO.StreamReader(@"C:UserName.txt"))
            {
                username = reader.ReadLine();
            }
            string leavingwork = System.IO.File.ReadAllText(@"C:Leavingwork.txt");
            string dailyreport = System.IO.File.ReadAllText(@"C:Dailyreport.txt");

            ComboBoxItem selectedItem = (ComboBoxItem)MailsBox.SelectedItem;


            if (selectedItem.Name == "Attendance")
            {
                DateTime dt = DateTime.Now;
                Adress.Text = "attendance@world-wing.com";
                Subject.Text = "出勤: " + dt.ToString("yyyy年MM月dd日 ") + "(" + dt.ToString("ddd") + ")" + username;
                Mailtext.Text = "";
            }
            if(selectedItem.Name == "Leavingwork")
            {
                DateTime dt = DateTime.Now;
                Adress.Text = "attendance@world-wing.com";
                
                Subject.Text = "退勤: " + dt.ToString("yyyy年MM月dd日 ") + "(" + dt.ToString("ddd") + ")" + username;
                Mailtext.Text = leavingwork;
            }
            if(selectedItem.Name == "Dailyreport")
            {
                DateTime dt = DateTime.Now;
                Adress.Text = "freshers-iar@world-wing.com";

                Subject.Text = "[日報] " + username + dt.ToString("yyyy年MM月dd日 ") + "(" + dt.ToString("ddd") + ")";
                Mailtext.Text = dailyreport;
            }
            if (selectedItem.Name == "BehindTime")
            {
                DateTime dt = DateTime.Now;
                Adress.Text = "attendance@world-wing.com";
                Subject.Text = "遅刻: " + dt.ToString("yyyy年MM月dd日 ") + "(" + dt.ToString("ddd") + ")" + username;
                Mailtext.Text = "原因";
            }
        }
        #endregion


        private void HiddenLoad(object sender, RoutedEventArgs e)
        {
            string filePath = @"TsubasaMailAddress.txt";
            TsubasaMailAddress.Text = String.Join("\r\n", File.ReadAllLines(filePath));
            string filePath2 = @"CC.txt";
            CC.Text = String.Join("\r\n", File.ReadAllLines(filePath2));
            string filePath3 = @"MicrosoftPass.txt";
            MicrosoftPassword.Text = String.Join("\r\n", File.ReadAllLines(filePath3));


        }
        #region メール送信
        private void SendMail(object sender,EventArgs e) 
        {
            try
            {

                string from = TsubasaMailAddress.Text;
                string to = Adress.Text;
                string CCtext = CC.Text;
                string subject = Subject.Text;
                string body = Mailtext.Text;
                string cc = CCtext;
                string smtpServer = "smtp-mail.outlook.com";
                int port = 587;
                string username = TsubasaMailAddress.Text;
                string password = MicrosoftPassword.Text;

                SmtpClient client = new SmtpClient(smtpServer, port);
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(username, password);
                client.EnableSsl = true;

                MailMessage message = new MailMessage(from, to, subject, body);
                if (!string.IsNullOrEmpty(cc))
                {
                    message.CC.Add(cc);
                }
                client.Send(message);

                MessageBox.Show("メールが送信されました。");

                system fileIo = new system();
                ComboBoxItem selectedItem = (ComboBoxItem)MailsBox.SelectedItem;
                if (selectedItem != null)
                {
                    if (selectedItem.Name == "Dailyreport")
                    {
                        fileIo.Dailyreport(Mailtext.Text);
                        MessageBox.Show("入力内容を保存しました");
                    }
                    else if (selectedItem.Name == "Leavingwork")
                    {
                        fileIo.Leavingwork(Mailtext.Text);
                        MessageBox.Show("入力内容を保存しました");
                    }
                    fileIo.CC(CC.Text);
                    this.Close();
                }
            }
            catch (Exception)
            {
               MessageBox.Show("メールを送信することが出来ませんでした。\r\nアカウントの設定が正しく行われているか確認してください", "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion
    }
}
