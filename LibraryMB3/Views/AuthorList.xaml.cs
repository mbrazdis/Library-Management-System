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
    /// Interaction logic for AuthorList.xaml
    /// </summary>
    public partial class AuthorList : UserControl
    {
        public AuthorList()
        {
            InitializeComponent();
            using (DbLibraryMbContext db = new DbLibraryMbContext())
            {
                List<Author> list = db.Authors.ToList();
                GridAuthor.ItemsSource = list;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AuthorAddUpdate Page = new AuthorAddUpdate();
            Page.ShowDialog();
            using (DbLibraryMbContext db = new DbLibraryMbContext())
            {
                List<Author> list = db.Authors.ToList();
                GridAuthor.ItemsSource = list;
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Author at = (Author)GridAuthor.SelectedItem;
            AuthorAddUpdate page = new AuthorAddUpdate();
            page.author = at;
            page.ShowDialog();
            using (DbLibraryMbContext db = new DbLibraryMbContext())
            {
                GridAuthor.ItemsSource = db.Authors.OrderBy(x => x.AuthorId).ToList();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure to delete", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning)
                == MessageBoxResult.Yes)
            {
                Author at = (Author)GridAuthor.SelectedItem;
                using (DbLibraryMbContext db = new DbLibraryMbContext())
                {
                    at.Active = false;
                    db.Authors.Update(at);
                    db.SaveChanges();
                    GridAuthor.ItemsSource = db.Authors.OrderBy(x => x.AuthorId).ToList();
                }
            }
        }

        
    }
}
