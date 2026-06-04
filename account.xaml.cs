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
    /// Логика взаимодействия для account.xaml
    /// </summary>
    public partial class account : Window
    {
        private bool isRoomView = true;

        public account()
        {
            InitializeComponent();
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
            if (App.IsDarkTheme)
            {
                this.Background = Brushes.DarkSlateGray;
                btnTheme.Background = Brushes.SlateGray;
                btnAdd.Background = Brushes.SlateGray;
                btnEdit.Background = Brushes.SlateGray;
                btnDelete.Background = Brushes.SlateGray;
                btnUpdate.Background = Brushes.SlateGray;
                btnRoom.Background = Brushes.SlateGray;
                btnBooking.Background = Brushes.Gray;

                btnTheme.Foreground = Brushes.White;
                btnAdd.Foreground = Brushes.White;
                btnEdit.Foreground = Brushes.White;
                btnDelete.Foreground = Brushes.White;
                btnUpdate.Foreground = Brushes.White;
                btnRoom.Foreground = Brushes.White;
                btnBooking.Foreground = Brushes.White;

                lstRoom.Background = Brushes.DarkGray;
                lstBooking.Background = Brushes.DarkGray;
            }
            else
            {
                this.Background = Brushes.LightBlue;
                btnTheme.Background = Brushes.LightBlue;
                btnAdd.Background = Brushes.LightBlue;
                btnEdit.Background = Brushes.LightBlue;
                btnDelete.Background = Brushes.LightBlue;
                btnUpdate.Background = Brushes.LightBlue;
                btnRoom.Background = Brushes.LightBlue;
                btnBooking.Background = Brushes.LightGray;

                btnTheme.Foreground = Brushes.White;
                btnAdd.Foreground = Brushes.White;
                btnEdit.Foreground = Brushes.White;
                btnDelete.Foreground = Brushes.White;
                btnUpdate.Foreground = Brushes.White;
                btnRoom.Foreground = Brushes.White;
                btnBooking.Foreground = Brushes.Black;

                lstRoom.Background = Brushes.LightBlue;
                lstBooking.Background = Brushes.LightBlue;
            }
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
            btnRoom.Background = System.Windows.Media.Brushes.LightBlue;
            btnBooking.Background = System.Windows.Media.Brushes.LightGray;
            LoadRoomsData();
        }

        private void BtnBooking_Click(object sender, RoutedEventArgs e)
        {
            isRoomView = false;
            lstRoom.Visibility = Visibility.Collapsed;
            lstBooking.Visibility = Visibility.Visible;
            btnRoom.Background = System.Windows.Media.Brushes.LightGray;
            btnBooking.Background = System.Windows.Media.Brushes.LightBlue;
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
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при добавлении записи.", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (isRoomView)
                {
                    if (lstRoom.SelectedItem is RoomsData selectedRoom)
                    {
                        var dialog = new RoomDialog(selectedRoom);
                        if (dialog.ShowDialog() == true)
                        {
                            // Обновляем данные
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
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при редактировании записи.", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при удалении записи.", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
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
    }
}
