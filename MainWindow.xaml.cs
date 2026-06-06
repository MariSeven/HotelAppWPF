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
    ///
    /// </summary>
    public partial class MainWindow : Window, IThemable
    {
        public int podtv = 0;
        public MainWindow()
        {
            InitializeComponent();
            UpdateTheme();
            btnLogin.Click += BtnLogin_Click;
            btnExit.Click += BtnExit_Click;
            btnTheme.Click += BtnTheme_Click;
        }

        public void UpdateTheme()
        {
            string themePrefix = App.IsDarkTheme ? "DarkTheme" : "LightTheme";

            // Применяем стили к элементам окна
            this.Style = (Style)Application.Current.TryFindResource(themePrefix);
            btnTheme.Style = (Style)Application.Current.TryFindResource($"{themePrefix}BTN");
            btnLogin.Style = (Style)Application.Current.TryFindResource($"{themePrefix}BTN");
            btnExit.Style = (Style)Application.Current.TryFindResource($"{themePrefix}BTN");
            txtLogin.Style = (Style)Application.Current.TryFindResource($"{themePrefix}TXTB");
            txtPassword.Style = (Style)Application.Current.TryFindResource($"{themePrefix}TXTP");
            txtlogin.Style = (Style)Application.Current.TryFindResource($"{themePrefix}TXT");
            txtpassword.Style = (Style)Application.Current.TryFindResource($"{themePrefix}TXT");

            btnTheme.Content = App.IsDarkTheme ? "Светлая тема" : "Темная тема";
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
                    podtv = 1;
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
            Close();
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

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (podtv == 0)
            {
                MessageBoxResult result = MessageBox.Show("Вы точно хотите закрыть приложение?", "Подтверждение закрытия", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.No)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}