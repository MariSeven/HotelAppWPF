using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HotelAppWPF
{
    /// <summary>
    /// 
    /// </summary>
    public partial class RoomDialog : Window, IThemable
    {
        private RoomsData _editingRoom;
        private bool _isEditMode;

        public RoomDialog() : this(null)
        {
        }
        
        public RoomDialog(RoomsData room)
        {
            InitializeComponent();
            UpdateTheme();
            _editingRoom = room;
            _isEditMode = room != null;

            btnSave.Click += BtnSave_Click;
            btnCancel.Click += BtnCancel_Click;

            if (_isEditMode)
            {
                Title = "Редактирование номера";
                txtRoomNumber.Text = room.RoomNumber.ToString();
                cmbRoomClass.SelectedIndex = GetClassIndex(room.RoomClass);
                txtCapacity.Text = room.RoomCapacity.ToString();
                txtPrice.Text = room.RoomPrice.ToString();
                cmbStatus.SelectedIndex = room.RoomStatus == "свободен" ? 0 : 1;
            }
            else
            {
                Title = "Добавление номера";
                cmbRoomClass.SelectedIndex = 0;
                cmbStatus.SelectedIndex = 0;
            }


            // Добавляем валидацию
            txtRoomNumber.TextChanged += ValidateInputs;
            txtCapacity.TextChanged += ValidateInputs;
            txtPrice.TextChanged += ValidateInputs;
        }
        public void UpdateTheme()
        {
            string themePrefix = App.IsDarkTheme ? "DarkTheme" : "LightTheme";

            // Применяем стили к элементам окна
            this.Style = (Style)Application.Current.TryFindResource(themePrefix);
            txt1.Style = (Style)Application.Current.TryFindResource($"{themePrefix}TXT");
            txt2.Style = (Style)Application.Current.TryFindResource($"{themePrefix}TXT");
            txt3.Style = (Style)Application.Current.TryFindResource($"{themePrefix}TXT");
            txt4.Style = (Style)Application.Current.TryFindResource($"{themePrefix}TXT");
            txt5.Style = (Style)Application.Current.TryFindResource($"{themePrefix}TXT");
            btnSave.Style = (Style)Application.Current.TryFindResource($"{themePrefix}BTN");
            btnCancel.Style = (Style)Application.Current.TryFindResource($"{themePrefix}BTN");
        }

        private int GetClassIndex(string className)
        {
            switch (className)
            {
                case "Стандарт": return 0;
                case "Полулюкс": return 1;
                case "Люкс": return 2;
                default: return 0;
            }
        }

        private void ValidateInputs(object sender, TextChangedEventArgs e)
        {
            txtError.Visibility = Visibility.Collapsed;
            btnSave.IsEnabled = true;

            // Сбрасываем цвета
            txtRoomNumber.BorderBrush = Brushes.Gray;
            txtCapacity.BorderBrush = Brushes.Gray;
            txtPrice.BorderBrush = Brushes.Gray;

            // Валидация номера комнаты
            if (!int.TryParse(txtRoomNumber.Text, out int roomNumber) || roomNumber < 1 || roomNumber > 999)
            {
                txtRoomNumber.BorderBrush = Brushes.Red;
                txtError.Text = "Номер комнаты должен быть от 1 до 999";
                txtError.Visibility = Visibility.Visible;
                btnSave.IsEnabled = false;
                return;
            }

            // Проверка на дубликат
            if (RoomsData.roomData.Any(r => r.RoomNumber == roomNumber &&
                (!(_isEditMode && _editingRoom != null) || r.RoomID != _editingRoom.RoomID)))
            {
                txtRoomNumber.BorderBrush = Brushes.Red;
                txtError.Text = $"Комната с номером {roomNumber} уже существует";
                txtError.Visibility = Visibility.Visible;
                btnSave.IsEnabled = false;
                return;
            }

            // Валидация вместимости
            if (!int.TryParse(txtCapacity.Text, out int capacity) || capacity < 1 || capacity > 4)
            {
                txtCapacity.BorderBrush = Brushes.Red;
                txtError.Text = "Количество мест должно быть от 1 до 4";
                txtError.Visibility = Visibility.Visible;
                btnSave.IsEnabled = false;
                return;
            }

            // Валидация цены
            if (!decimal.TryParse(txtPrice.Text, out decimal price) || price < 500 || price > 50000)
            {
                txtPrice.BorderBrush = Brushes.Red;
                txtError.Text = "Цена должна быть от 500 до 50000";
                txtError.Visibility = Visibility.Visible;
                btnSave.IsEnabled = false;
                return;
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateAllInputs())
            {
                MessageBox.Show("Исправьте ошибки в полях ввода", "Ошибка валидации",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DialogResult = true;
            Close();
        }

        private bool ValidateAllInputs()
        {
            if (!int.TryParse(txtRoomNumber.Text, out int roomNumber) || roomNumber < 1 || roomNumber > 999)
                return false;
            foreach (var room in RoomsData.roomData)
            {
                if (_isEditMode && _editingRoom != null && room.RoomID == _editingRoom.RoomID)
                    continue;

                if (room.RoomNumber == roomNumber)
                    return false;
            }

            if (!int.TryParse(txtCapacity.Text, out int capacity) || capacity < 1 || capacity > 4)
                return false;

            if (!decimal.TryParse(txtPrice.Text, out decimal price) || price < 500 || price > 50000)
                return false;

            return true;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        public RoomsData GetRoomData()
        {
            string roomClass = ((ComboBoxItem)cmbRoomClass.SelectedItem).Content.ToString();
            string status = ((ComboBoxItem)cmbStatus.SelectedItem).Content.ToString();

            return new RoomsData(
                int.Parse(txtRoomNumber.Text),
                roomClass,
                int.Parse(txtCapacity.Text),
                decimal.Parse(txtPrice.Text),
                status);
        }
    }
}
