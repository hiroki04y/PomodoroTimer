using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace kojin_0H05042
{
    /// <summary>
    /// Page3.xaml の相互作用ロジック
    /// </summary>
    public partial class Page3 : Page
    {
        String flag1 = Properties.Settings.Default.auto1;
        String flag2 = Properties.Settings.Default.auto2;

        public Page3()
        {
            InitializeComponent();
            NumericScroll1.Value = double.Parse(Properties.Settings.Default.restMinute);
            NumericScroll2.Value = double.Parse(Properties.Settings.Default.lblMinute);
            ComboBox1.Text = Properties.Settings.Default.theme;


            ///switch
            if (flag1 == "False"){
                simage.Source = new BitmapImage(new Uri("pack://application:,,,/switch-off.png"));
            }
            else{
                simage.Source = new BitmapImage(new Uri("pack://application:,,,/switch-on.png"));
            }

            if (flag2 == "False"){
                rimage.Source = new BitmapImage(new Uri("pack://application:,,,/switch-off.png"));
            }
            else{
                rimage.Source = new BitmapImage(new Uri("pack://application:,,,/switch-on.png"));
            }


            ///変換ボタン
            switch (Properties.Settings.Default.theme){
                case "red":
                    changeButton.Source = new BitmapImage(new Uri("pack://application:,,,/changered.png"));
                    defButton.Source = new BitmapImage(new Uri("pack://application:,,,/defred.png"));
                    break;
                case "blue":
                    changeButton.Source = new BitmapImage(new Uri("pack://application:,,,/changeblue.png"));
                    defButton.Source = new BitmapImage(new Uri("pack://application:,,,/defblue.png"));
                    break;
                case "green":
                    changeButton.Source = new BitmapImage(new Uri("pack://application:,,,/changegreen.png"));
                    defButton.Source = new BitmapImage(new Uri("pack://application:,,,/defgreen.png"));
                    break;
                case "yellow":
                    changeButton.Source = new BitmapImage(new Uri("pack://application:,,,/changeyellow.png"));
                    defButton.Source = new BitmapImage(new Uri("pack://application:,,,/defyellow.png"));
                    break;
            }
        }

        private void simage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (flag1 == "False")
            {
                simage.Source = new BitmapImage(new Uri("pack://application:,,,/switch-on.png"));
                flag1 = "True";
            }
            else
            {
                simage.Source = new BitmapImage(new Uri("pack://application:,,,/switch-off.png"));
                flag1 = "False";
            }
        }

        private void rimage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (flag2 == "False")
            {
                rimage.Source = new BitmapImage(new Uri("pack://application:,,,/switch-on.png"));
                flag2 = "True";
            }
            else
            {
                rimage.Source = new BitmapImage(new Uri("pack://application:,,,/switch-off.png"));
                flag2 = "False";
            }
            Properties.Settings.Default.Save();
        }

        private void changeButton_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.restMinute= NumericScroll1.Value.ToString();
            Properties.Settings.Default.lblMinute = NumericScroll2.Value.ToString();
            Properties.Settings.Default.theme = ComboBox1.Text;
            Properties.Settings.Default.auto1 = flag1;
            Properties.Settings.Default.auto2 = flag2;
            Properties.Settings.Default.Save();


            var page1 = new Page1();
            NavigationService.Navigate(page1);
        }

        private void defButton_click(object sender, MouseButtonEventArgs e)
        {
            Properties.Settings.Default.restMinute = "5";
            Properties.Settings.Default.lblMinute = "25";
            Properties.Settings.Default.theme = "red";
            Properties.Settings.Default.auto1 = "False";
            Properties.Settings.Default.auto2 = "False";
            Properties.Settings.Default.Save();

            var page1 = new Page1();
            NavigationService.Navigate(page1);
        }
    }
}
