using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Input;
using System.Text.RegularExpressions;
using BUS;

namespace SushiPay
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        System.Windows.Threading.DispatcherTimer timer;

        public double Sum
        {
            get;
            set;
        }
        double cash = 0;

        public Window2()
        {
            InitializeComponent();
            timer = new System.Windows.Threading.DispatcherTimer();

            timer.Tick += new EventHandler(timerTick);
            timer.Interval = new TimeSpan(0, 0, 2);
        }

        private void timerTick(object sender, EventArgs e)
        {
            Messages.Text = String.Empty;
            MessagesLabel.Background = new SolidColorBrush(Colors.White);
            timer.Stop();
        }

        private void buttonOkForPay_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Rules.IsInputCashDataValid(inputCash.Text, Sum);
                DialogResult = true;
            }
            catch (Exception exeption)
            {
                MessagesLabel.Background = new SolidColorBrush(Color.FromRgb(255, 26, 26));
                Messages.Text = exeption.Message;
                timer.Start();
            }
        }

        private void buttonCancelForPay_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RestCash_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            cash = Convert.ToDouble(inputCash.Text);
            RestCash.Text = (cash - Sum).ToString();
        }

        private void TextBoxPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Rules.IsTextAllowed(e.Text);
        }
    }
}
