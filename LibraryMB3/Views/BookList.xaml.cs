using LibraryMB3.Models;
using LibraryMB3.Commands;
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
using Microsoft.EntityFrameworkCore;
using LibraryMB3.Views;
using LibraryMB3.ViewModel;

namespace LibraryMB3.Views
{
    /// <summary>
    /// Interaction logic for BookList.xaml
    /// </summary>
    public partial class BookList : UserControl
    {
        public BookList()
        {
            InitializeComponent();

        }
        DbLibraryMbContext db = new DbLibraryMbContext();

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            fillGrid();
        }

        void fillGrid()
        {
            var list = db.Books
               .Include(x => x.BookType)
               .Include(x => x.Publisher)
               .Select(a => new
               {
                   bookId = a.BookId,
                   bookTypeId = a.BookTypeId,
                   bookTypeName = a.BookType.Name,
                   publisherId = a.PublisherId,
                   publishName = a.Publisher.Name,
                   title = a.Title,
                   publishYear = a.PublishYear,
                   stock = a.Stock,
                   active = a.Active
               }).OrderBy(x => x.bookId).ToList();
            List<BookModel> modellist = new List<BookModel>();
            foreach (var item in list)
            {
                BookModel model = new BookModel();
                model.BookId = item.bookId;
                model.BookTypeId = item.bookTypeId;
                model.BookTypeName = item.bookTypeName;
                model.PublisherId = item.publisherId;
                model.PublishName = item.publishName;
                model.Title = item.title;
                model.PublishYear = item.publishYear;
                model.Stock = item.stock;
                model.Active = item.active;
                modellist.Add(model);

            }
            GridBooks.ItemsSource = modellist;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            BookAddUpdate page = new BookAddUpdate();
            page.ShowDialog();
            fillGrid();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            BookModel model = (BookModel)GridBooks.SelectedItem;
            if (model != null && model.BookId != 0)
            {
                BookAddUpdate page = new BookAddUpdate();
                page.model = model;
                page.ShowDialog();
                fillGrid();
            }

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Are you sure to delete","Question",MessageBoxButton.YesNo,MessageBoxImage.Warning) 
                == MessageBoxResult.Yes)
            {
                BookModel model = (BookModel)GridBooks.SelectedItem;
                if (model != null && model.BookId != 0)
                {
                    using (DbLibraryMbContext db = new DbLibraryMbContext())
                    {
                        var bookToDelete = db.Books.Find(model.BookId);
                        if (bookToDelete != null)
                        {
                            bookToDelete.Active = false;
                            db.Update(bookToDelete);
                            db.SaveChanges();
                            fillGrid();
                        }
                    }
                }
            }

            
        }

        private void GridBooks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }










        /*private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            BookAddUpdate Page = new BookAddUpdate();
            Page.ShowDialog();
            using (DbLibraryMbContext db = new DbLibraryMbContext())
            {
                List<Book> list = db.Books.ToList();
                GridBooks.ItemsSource = list;
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Book bt = (Book)GridBooks.SelectedItem;
            BookAddUpdate page = new BookAddUpdate();
            page.book = bt;
            page.ShowDialog();
            using (DbLibraryMbContext db = new DbLibraryMbContext())
            {
                GridBooks.ItemsSource = db.Books.OrderBy(x => x.BookId).ToList();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Book bt = (Book )GridBooks.SelectedItem;
            using(DbLibraryMbContext db = new DbLibraryMbContext())
            {
                bt.Active = false;
                db.Books.Update(bt);
                db.SaveChanges();
                GridBooks.ItemsSource = db.Books.OrderBy(x => x.BookId).ToList();

            }
        }*/


    }
    }

