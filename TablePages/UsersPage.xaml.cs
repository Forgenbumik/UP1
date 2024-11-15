using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UP1.AddWindows;

namespace UP1.TablePages
{
    /// <summary>
    /// Логика взаимодействия для UsersPage.xaml
    /// </summary>
    public partial class UsersPage : Page
    {
        UP1Entities UP = UP1Entities.GetContext();
        AddUserWindow addUserWindow;
        public UsersPage()
        {
            InitializeComponent();
            GridUsers.Items.Clear();
            GridUsers.ItemsSource = UP.Users.ToList();
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            UP.ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            UP.ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var selectedItems = GridUsers.SelectedItems.Cast<User>().ToList();
            if (selectedItems.Any())
            {
                foreach (var item in selectedItems.Select(itm => itm))
                {
                    UP.Users.Remove(item);
                }
                UP.SaveChanges();
            }
        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            addUserWindow = new AddUserWindow(null);
            addUserWindow.Show();
        }

        private void bEdit_Click(object sender, RoutedEventArgs e)
        {
            addUserWindow = new AddUserWindow((sender as Button).DataContext as User);
            addUserWindow.Show();
        }
    }
}
