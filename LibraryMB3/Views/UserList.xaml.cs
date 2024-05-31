using LibraryMB3.Commands;
using LibraryMB3.Models;
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

namespace LibraryMB3.Views
{
    /// <summary>
    /// Interaction logic for UserList.xaml
    /// </summary>
    public partial class UserList : UserControl
    {
        public UserList()
        {
            InitializeComponent();
            using (DbLibraryMbContext db = new DbLibraryMbContext())
            {
                List<User> list = db.Users.ToList();
                GridUser.ItemsSource = list;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            UserAddUpdate Page = new UserAddUpdate();
            Page.ShowDialog();
            using (DbLibraryMbContext db = new DbLibraryMbContext())
            {
                List<User> list = db.Users.ToList();
                GridUser.ItemsSource = list;
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            User ut = (User)GridUser.SelectedItem;
            UserAddUpdate page = new UserAddUpdate();
            page.user = ut;
            page.ShowDialog();
            using (DbLibraryMbContext db = new DbLibraryMbContext())
            {
                GridUser.ItemsSource = db.Users.OrderBy(x => x.UserId).ToList();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure to delete", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning)
                == MessageBoxResult.Yes)
            { 
                User ut = (User)GridUser.SelectedItem;
                using (DbLibraryMbContext db = new DbLibraryMbContext())
                {
                    ut.Active = false;
                    db.Users.Update(ut);
                    db.SaveChanges();
                    GridUser.ItemsSource = db.Users.OrderBy(x => x.UserId).ToList();
                }
            }
        }
    }
}
