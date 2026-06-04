using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HotelAppWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            btnLogin.Click += BtnLogin_Click;
            btnExit.Click += BtnExit_Click;
            btnTheme.Click += BtnTheme_Click;
        }

        public void UpdateTheme()
        {
            if (App.IsDarkTheme)
            {
                this.Background = Brushes.DarkSlateGray;
                btnTheme.Background = Brushes.SlateGray;
                btnLogin.Background = Brushes.SlateGray;
                btnExit.Background = Brushes.SlateGray;
                btnTheme.Foreground = Brushes.White;
                btnLogin.Foreground = Brushes.White;
                btnExit.Foreground = Brushes.White;
                txtLogin.Background = Brushes.DarkGray;
                txtLogin.Foreground = Brushes.White;
                txtPassword.Background = Brushes.DarkGray;
                txtPassword.Foreground = Brushes.White;
            }
            else
            {
                this.Background = Brushes.LightBlue;
                btnTheme.Background = Brushes.LightBlue;
                btnLogin.Background = Brushes.LightBlue;
                btnExit.Background = Brushes.LightBlue;
                btnTheme.Foreground = Brushes.White;
                btnLogin.Foreground = Brushes.White;
                btnExit.Foreground = Brushes.White;
                txtLogin.Background = Brushes.White;
                txtLogin.Foreground = Brushes.Black;
                txtPassword.Background = Brushes.White;
                txtPassword.Foreground = Brushes.Black;
            }
        }
        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string login = txtLogin.Text.Trim();
                string password = txtPassword.Password;  

                if (UsersData.Users.ContainsKey(login) && UsersData.Users[login] == password)
                {
                    // Успешная авторизация
                    account mainAccount = new account();
                    mainAccount.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Ошибка авторизации. Проверьте логин и пароль",
                        "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    txtLogin.Clear();
                    txtPassword.Clear();  
                    txtLogin.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка. Пожалуйста, попробуйте снова.",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BtnTheme_Click(object sender, RoutedEventArgs e)
        {
            App.IsDarkTheme = !App.IsDarkTheme;
            App.ApplyTheme();

            foreach (Window window in Application.Current.Windows)
            {
                if (window is MainWindow)
                {
                    window.Dispatcher.Invoke(() =>
                    {
                        btnTheme.Content = App.IsDarkTheme ? "Светлая тема" : "Темная тема";
                    });
                }
            }
        }
    }
}