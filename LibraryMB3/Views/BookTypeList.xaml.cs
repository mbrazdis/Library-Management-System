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
    /// Interaction logic for BookTypeList.xaml
    /// </summary>
    public partial class BookTypeList : UserControl
    {
        public BookTypeList()
        {

            InitializeComponent();
            using (DbLibraryMbContext db = new DbLibraryMbContext())
            {
                List<BookType> list = db.BookTypes.ToList();
                GridBookType.ItemsSource = list;
            }
        }

        public BookType bookType;
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            BookTypeAddUpdate Page = new BookTypeAddUpdate();
            Page.ShowDialog();
            using (DbLibraryMbContext db = new DbLibraryMbContext())
            {
                List<BookType> list = db.BookTypes.ToList();
                GridBookType.ItemsSource = list;
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            BookType btt = (BookType)GridBookType.SelectedItem;
            BookTypeAddUpdate page = new BookTypeAddUpdate();
            page.bookType = btt;
            page.ShowDialog();
            using (DbLibraryMbContext db = new DbLibraryMbContext())
            {
                GridBookType.ItemsSource = db.BookTypes.OrderBy(x => x.BookTypeId).ToList();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure to delete", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning)
                == MessageBoxResult.Yes)
            { 
                BookType btt = (BookType)GridBookType.SelectedItem;
                using (DbLibraryMbContext db = new DbLibraryMbContext())
                {
                    btt.Active = false;
                    db.BookTypes.Update(btt);
                    db.SaveChanges();
                    GridBookType.ItemsSource = db.BookTypes.OrderBy(x => x.BookTypeId).ToList();

                }
            }
        }
    }
}
