using Core;
using System.Windows;

namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Settings.Instance.Load();
            TextBox.Text = Settings.Instance.Text ?? "";
        }

        private void SetButton_Click(object sender, RoutedEventArgs e)
        {
            Settings.Instance.Text = TextBox.Text;
            Settings.Instance.Save();
            Main.Update();
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            Main.Reset();
        }
    }
}
