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
    /// Логика взаимодействия для BookingDialog.xaml
    /// </summary>
    public partial class BookingDialog : Window
    {
        private BookingData _editingBooking;
        private bool _isEditMode;

        public BookingDialog() : this(null)
        {
        }

        public BookingDialog(BookingData booking)
        {
            InitializeComponent();
            _editingBooking = booking;
            _isEditMode = booking != null;

            btnSave.Click += BtnSave_Click;
            btnCancel.Click += BtnCancel_Click;
            dpCheckIn.SelectedDateChanged += ValidateInputs;
            dpCheckOut.SelectedDateChanged += ValidateInputs;
            txtFullName.TextChanged += ValidateInputs;

            // Заполняем список номеров
            foreach (var room in RoomsData.roomData)
            {
                cmbRoomNumber.Items.Add(new ComboBoxItem
                {
                    Content = $"№{room.RoomNumber} ({room.RoomClass})",
                    Tag = room.RoomNumber
                });
            }

            if (_isEditMode)
            {
                Title = "Редактирование бронирования";
                txtFullName.Text = booking.BookingFullNameLodger;

                // Находим нужный номер
                for (int i = 0; i < cmbRoomNumber.Items.Count; i++)
                {
                    var item = (ComboBoxItem)cmbRoomNumber.Items[i];
                    if ((int)item.Tag == booking.BookingNumberRoom)
                    {
                        cmbRoomNumber.SelectedIndex = i;
                        break;
                    }
                }

                dpCheckIn.SelectedDate = booking.BookingDateStart.ToDateTime(TimeOnly.MinValue);
                dpCheckOut.SelectedDate = booking.BookingDateStop.ToDateTime(TimeOnly.MinValue);
            }
            else
            {
                Title = "Добавление бронирования";
                cmbRoomNumber.SelectedIndex = 0;
                dpCheckIn.SelectedDate = DateTime.Today;
                dpCheckOut.SelectedDate = DateTime.Today.AddDays(1);
            }
        }

        private void ValidateInputs(object sender, RoutedEventArgs e)
        {
            txtError.Visibility = Visibility.Collapsed;
            btnSave.IsEnabled = true;

            txtFullName.BorderBrush = Brushes.Gray;

            // Валидация ФИО
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                txtFullName.BorderBrush = Brushes.Red;
                txtError.Text = "Введите ФИО гостя";
                txtError.Visibility = Visibility.Visible;
                btnSave.IsEnabled = false;
                return;
            }

            // Валидация дат
            if (!dpCheckIn.SelectedDate.HasValue || !dpCheckOut.SelectedDate.HasValue)
            {
                txtError.Text = "Выберите даты заезда и выезда";
                txtError.Visibility = Visibility.Visible;
                btnSave.IsEnabled = false;
                return;
            }

            if (dpCheckOut.SelectedDate.Value <= dpCheckIn.SelectedDate.Value)
            {
                txtError.Text = "Дата выезда должна быть позже даты заезда";
                txtError.Visibility = Visibility.Visible;
                btnSave.IsEnabled = false;
                return;
            }

            if (cmbRoomNumber.SelectedItem == null)
            {
                txtError.Text = "Выберите номер комнаты";
                txtError.Visibility = Visibility.Visible;
                btnSave.IsEnabled = false;
                return;
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при сохранении данных", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool ValidateAllInputs()
        {
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
                return false;

            if (!dpCheckIn.SelectedDate.HasValue || !dpCheckOut.SelectedDate.HasValue)
                return false;

            if (dpCheckOut.SelectedDate.Value <= dpCheckIn.SelectedDate.Value)
                return false;

            if (cmbRoomNumber.SelectedItem == null)
                return false;

            return true;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        public BookingData GetBookingData()
        {
            var selectedItem = (ComboBoxItem)cmbRoomNumber.SelectedItem;
            int roomNumber = (int)selectedItem.Tag;

            DateOnly checkIn = DateOnly.FromDateTime(dpCheckIn.SelectedDate.Value);
            DateOnly checkOut = DateOnly.FromDateTime(dpCheckOut.SelectedDate.Value);

            return new BookingData(
                txtFullName.Text.Trim(),
                roomNumber,
                checkIn,
                checkOut);
        }
    }
}
