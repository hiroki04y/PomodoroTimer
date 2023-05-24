using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Threading;

namespace kojin_0H05042
{
    /// <summary>
    /// Page1.xaml の相互作用ロジック
    /// </summary>
    public partial class Page1 : Page
    {
        readonly Stopwatch stopwatch = new Stopwatch();
        readonly DispatcherTimer timer = new DispatcherTimer();
        readonly DispatcherTimer timer2 = new DispatcherTimer();
        int x = 0;
        int minutes = 0;
        int timerflag = 0;
        String rest;

        public Page1()
        {
            InitializeComponent();

            ///labelの設定
            minutes = int.Parse(Properties.Settings.Default.lblMinute);
            lblMinutes.Content = minutes.ToString("00");
            lblSecound.Content = "00";
            rest = Properties.Settings.Default.restMinute;
            ///System.Diagnostics.Trace.WriteLine(Properties.Settings.Default.auto1);

            //テーマの設定
            switch (Properties.Settings.Default.theme){
                case "red":
                    top.Source = new BitmapImage(new Uri("pack://application:,,,/red1.png"));
                    top2.Source = new BitmapImage(new Uri("pack://application:,,,/red2.png"));
                    break;
                case "blue":
                    top.Source = new BitmapImage(new Uri("pack://application:,,,/brue1.png"));
                    top2.Source = new BitmapImage(new Uri("pack://application:,,,/brue2.png"));
                    break;
                case "green":
                    top.Source = new BitmapImage(new Uri("pack://application:,,,/green1.png"));
                    top2.Source = new BitmapImage(new Uri("pack://application:,,,/green2.png"));
                    break;
                case "yellow":
                    top.Source = new BitmapImage(new Uri("pack://application:,,,/yellow1.png"));
                    top2.Source = new BitmapImage(new Uri("pack://application:,,,/yellow2.png"));
                    break;
            }



            timer.Interval = new TimeSpan(0, 0, 0, 0, 10);    //インターバルを10ミリ秒に設定
            timer.Tick += TimerMethod;  //インターバル毎に発生するイベントを設定
            timer2.Interval = new TimeSpan(0, 0, minutes, 0, 0);    //インターバルを1分に設定
            timer2.Tick += TimerMethod2;  //インターバル毎に発生するイベントを設定

            ///トースト通知ボタンの処理
            ToastNotificationManagerCompat.OnActivated += this.ToastNotificationManagerCompat_OnActivated;


            ///自動起動
            if (Properties.Settings.Default.auto1 == "True")
            {
                image.Visibility = Visibility.Hidden;
                image2.Visibility = Visibility.Visible;

                stopwatch.Start();
                timer.Start();
                timer2.Start();
                timerflag = 1;
            }

        }

        private void TimerMethod(object sender, EventArgs e)
        {
            ///10ミリ秒ごとに呼び出されて表示の時間を更新している
            var result = stopwatch.Elapsed;
            lblMinutes.Content = (minutes - 1 - result.Minutes).ToString("00");                   //分     更新
            lblSecound.Content = (59 - result.Seconds).ToString("00");                   //秒     更新
        }

        private void TimerMethod2(object sender, EventArgs e)
        {
             x += 1;
             new ToastContentBuilder()
                .AddText("☕休憩時間開始☕")
                .AddText("休憩時間は" + rest + "分です")
                .AddButton(new ToastButton("画面のロック", "rock"))
                .Show();
            if(x == 1)
            {
                timer.Stop();
                timer2.Stop();
                stopwatch.Reset();
                timerflag = 0;
                var page2 = new Page2();
                NavigationService.Navigate(page2);
            }
        }

        /// トースト通知のボタンが押された時の処理
        private void ToastNotificationManagerCompat_OnActivated(ToastNotificationActivatedEventArgsCompat e)
        {
            ///押されたボタンの確認
            var arg = e.Argument;
            if (arg == "rock")
            {
                Process.Start(@"C:\WINDOWS\system32\rundll32.exe", "user32.dll,LockWorkStation");
                ///System.Diagnostics.Trace.WriteLine("通過ポイントclick");
            }
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            image.Visibility = Visibility.Hidden;
            image2.Visibility = Visibility.Visible;

            stopwatch.Start();
            timer.Start();
            timer2.Start();
            timerflag = 1;
            ///System.Diagnostics.Trace.WriteLine("通過ポイント１");
        }

        private void Image2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            image.Visibility = Visibility.Visible;
            image2.Visibility = Visibility.Hidden;

            timer.Stop();
            timer2.Stop();
            stopwatch.Stop();
            timerflag = 0;
        }

        private void btnReset_Click(object sender, MouseButtonEventArgs e)
        {
            timer.Stop();
            timer2.Stop();
            stopwatch.Reset();
            timerflag = 0;
            lblMinutes.Content = minutes.ToString("00");       //分     初期化
            lblSecound.Content = "00";       //秒     初期化

            image.Visibility = Visibility.Visible;
            image2.Visibility = Visibility.Hidden;
        }

        private void setting_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (timerflag == 1)
            {
                timer.Stop();
                timer2.Stop();
                stopwatch.Stop();
                timerflag = 0;
            }
            NavigationService.Navigate(new Page3());
        }

        private void top_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(timerflag == 1) {
                timer.Stop();
                timer2.Stop();
                stopwatch.Stop();
                timerflag = 0;
            }
            NavigationService.Navigate(new Page2());
        }
    }
}
