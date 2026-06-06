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
    public partial class account : Window, IThemable
    {
        private bool isRoomView = true;

        public account()
        {
            InitializeComponent();
            UpdateTheme();
            InitializeEventHandlers();
            LoadRoomsData();
        }

        private void InitializeEventHandlers()
        {
            btnTheme.Click += BtnTheme_Click;
            btnRoom.Click += BtnRoom_Click;
            btnBooking.Click += BtnBooking_Click;
            btnAdd.Click += BtnAdd_Click;
            btnEdit.Click += BtnEdit_Click;
            btnDelete.Click += BtnDelete_Click;
            btnUpdate.Click += BtnUpdate_Click;
            lstRoom.MouseDoubleClick += LstRoom_MouseDoubleClick;
            lstBooking.MouseDoubleClick += LstBooking_MouseDoubleClick;
        }

        public void UpdateTheme()
        {
            string themePrefix = App.IsDarkTheme ? "DarkTheme" : "LightTheme";

            // Применяем стили к элементам окна
            this.Style = (Style)Application.Current.TryFindResource(themePrefix);
            btnTheme.Style = (Style)Application.Current.TryFindResource($"{themePrefix}BTN");
            btnAdd.Style = (Style)Application.Current.TryFindResource($"{themePrefix}BTN");
            btnEdit.Style = (Style)Application.Current.TryFindResource($"{themePrefix}BTN");
            btnDelete.Style = (Style)Application.Current.TryFindResource($"{themePrefix}BTN");
            btnUpdate.Style = (Style)Application.Current.TryFindResource($"{themePrefix}BTN");
            btnRoom.Style = (Style)Application.Current.TryFindResource($"{themePrefix}BTN");
            btnBooking.Style = (Style)Application.Current.TryFindResource($"{themePrefix}BTN");

            btnTheme.Content = App.IsDarkTheme ? "Светлая тема" : "Темная тема";
        }
        private void BtnTheme_Click(object sender, RoutedEventArgs e)
        {
            App.IsDarkTheme = !App.IsDarkTheme;
            App.ApplyTheme();
            btnTheme.Content = App.IsDarkTheme ? "Светлая тема" : "Темная тема";
        }

        private void BtnRoom_Click(object sender, RoutedEventArgs e)
        {
            isRoomView = true;
            lstRoom.Visibility = Visibility.Visible;
            lstBooking.Visibility = Visibility.Collapsed;
            btnBooking.Background = (Brush)new BrushConverter().ConvertFrom("#BBBBBB");
            btnBooking.Foreground = (Brush)new BrushConverter().ConvertFrom("#707070");
            btnRoom.Background = (Brush)new BrushConverter().ConvertFrom("#6060AA");
            btnRoom.Foreground = (Brush)new BrushConverter().ConvertFrom("#FFFFFF");
            LoadRoomsData();
        }

        private void BtnBooking_Click(object sender, RoutedEventArgs e)
        {
            isRoomView = false;
            lstRoom.Visibility = Visibility.Collapsed;
            lstBooking.Visibility = Visibility.Visible;
            btnRoom.Background = (Brush)new BrushConverter().ConvertFrom("#BBBBBB");
            btnRoom.Foreground = (Brush)new BrushConverter().ConvertFrom("#707070");
            btnBooking.Background = (Brush)new BrushConverter().ConvertFrom("#6060AA");
            btnBooking.Foreground = (Brush)new BrushConverter().ConvertFrom("#FFFFFF");
            LoadBookingsData();
        }

        private void LoadRoomsData()
        {
            lstRoom.ItemsSource = null;
            lstRoom.ItemsSource = RoomsData.roomData;
        }

        private void LoadBookingsData()
        {
            lstBooking.ItemsSource = null;
            lstBooking.ItemsSource = BookingData.bookingData;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (isRoomView)
            {
                var dialog = new RoomDialog();
                if (dialog.ShowDialog() == true)
                {
                    var newRoom = dialog.GetRoomData();
                    RoomsData.roomData.Add(newRoom);
                    LoadRoomsData();
                }
            }
            else
            {
                var dialog = new BookingDialog();
                if (dialog.ShowDialog() == true)
                {
                    var newBooking = dialog.GetBookingData();
                    BookingData.bookingData.Add(newBooking);
                    LoadBookingsData();
                }
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (isRoomView)
            {
                if (lstRoom.SelectedItem is RoomsData selectedRoom)
                {
                    var dialog = new RoomDialog(selectedRoom);
                    if (dialog.ShowDialog() == true)
                    {
                        var updatedRoom = dialog.GetRoomData();
                        int index = RoomsData.roomData.IndexOf(selectedRoom);
                        if (index >= 0)
                        {
                            RoomsData.roomData[index] = updatedRoom;
                        }
                        LoadRoomsData();
                    }
                }
                else
                {
                    MessageBox.Show("Выберите запись для редактирования", "Внимание",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                if (lstBooking.SelectedItem is BookingData selectedBooking)
                {
                    var dialog = new BookingDialog(selectedBooking);
                    if (dialog.ShowDialog() == true)
                    {
                        var updatedBooking = dialog.GetBookingData();
                        int index = BookingData.bookingData.IndexOf(selectedBooking);
                        if (index >= 0)
                        {
                            BookingData.bookingData[index] = updatedBooking;
                        }
                        LoadBookingsData();
                    }
                }
                else
                {
                    MessageBox.Show("Выберите запись для редактирования", "Внимание",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (isRoomView)
            {
                if (lstRoom.SelectedItem is RoomsData selectedRoom)
                {
                    var result = MessageBox.Show(
                        $"Удалить запись о номере {selectedRoom.RoomNumber}?",
                        "Подтверждение удаления",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        RoomsData.roomData.Remove(selectedRoom);
                        LoadRoomsData();
                    }
                }
                else
                {
                    MessageBox.Show("Выберите запись для удаления", "Внимание",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                if (lstBooking.SelectedItem is BookingData selectedBooking)
                {
                    var result = MessageBox.Show(
                        $"Удалить бронирование для {selectedBooking.BookingFullNameLodger}?",
                        "Подтверждение удаления",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        BookingData.bookingData.Remove(selectedBooking);
                        LoadBookingsData();
                    }
                }
                else
                {
                    MessageBox.Show("Выберите запись для удаления", "Внимание",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            LoadRoomsData();
            LoadBookingsData();
            MessageBox.Show("Данные обновлены", "Информация",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void LstRoom_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            BtnEdit_Click(sender, e);
        }

        private void LstBooking_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            BtnEdit_Click(sender, e);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы точно хотите закрыть приложение?", "Подтверждение закрытия", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}
