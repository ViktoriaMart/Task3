using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using BUS;

namespace SushiPay
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        System.Windows.Threading.DispatcherTimer timer;
        public Window1()
        {
            InitializeComponent();
            timer = new System.Windows.Threading.DispatcherTimer();

            timer.Tick += new EventHandler(timerTick);
            timer.Interval = new TimeSpan(0, 0, 2);
        }

        void timerTick(object sender, EventArgs e)
        {
            Messages.Text = String.Empty;
            MessagesLabel.Background = new SolidColorBrush(Colors.White);
            timer.Stop();
        }

        public void buttonOkForPay_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Rules.IsCreditDataValid(numberBox.Text, passwordBox.Password);
                DialogResult = true;
            }
            catch (Exception exeption)
            {

                MessagesLabel.Background = new SolidColorBrush(Color.FromRgb(255, 26, 26));
                Messages.Text = exeption.Message;
                timer.Start();
            }
        }

        public void buttonCancelForPay_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void TextBoxPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Rules.IsTextAllowed(e.Text);
        }
    }
}
