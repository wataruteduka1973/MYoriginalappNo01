using System.IO;

namespace MYoriginalappNo01
{
    internal class system
    {
        //退勤メールのテキスト設定
        public void Leavingwork(string TextContents)
        {
            //　using文を使ってファイルをクローズするコード
            string filePath = @"Leavingwork.txt";
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                // ファイルに書き込む
                writer.WriteLine(TextContents);
            }
        }
        //日報メールのテキスト設定
        public void Dailyreport(string TextContents)
        {
            string filePath = @"Dailyreport.txt";
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine(TextContents);
            }
        }
        //ユーザー名の設定
        public void UserName(string TextContents)
        {

            string filePath = @"UserName.txt";
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine(TextContents);
            }
        }

        public void TsubasaMailAddress(string TextContents)
        {

            string filePath = @"TsubasaMailAddress.txt";
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine(TextContents);
            }
        }

        public void JobcanPass(string TextContents)
        {

            string filePath = @"JobcanPass.txt";
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine(TextContents);
            }
        }

        public void MicrosoftPass(string TextContents)
        {

            string filePath = @"MicrosoftPass.txt";
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine(TextContents);
            }
        }
        public void CC(string TextContents)
        {

            string filePath = @"CC.txt";
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine(TextContents);
            }
        }

        public void AttendanceRecord(string TextContents)
        {

            string filePath = @"AttendanceRecord.txt";
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine(TextContents);
            }
        }

        public void FinishWork(string TextContents)
        {

            string filePath = @"FinishWork.txt";
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine(TextContents);
            }
        }
    }
}
