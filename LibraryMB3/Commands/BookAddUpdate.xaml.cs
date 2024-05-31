using LibraryMB3.Models;
using LibraryMB3.ViewModel;
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
using System.Windows.Shapes;
using System.Xml.Linq;

namespace LibraryMB3.Commands
{
    /// <summary>
    /// Interaction logic for BookAddUpdate.xaml
    /// </summary>
    public partial class BookAddUpdate : Window
    {
        public BookAddUpdate()
        {
            InitializeComponent();
        }

        DbLibraryMbContext db = new DbLibraryMbContext();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var list = db.BookTypes.ToList().OrderBy(x=>x.BookTypeId);
            cmbBookTypeName.ItemsSource = list;
            cmbBookTypeName.DisplayMemberPath = "Name";
            cmbBookTypeName.SelectedValuePath = "BookTypeId";
            cmbBookTypeName.SelectedIndex = -1;

            var list1 = db.Publishers.ToList().OrderBy(x=>x.PublisherId);
            cmbPublisherName.ItemsSource = list1;
            cmbPublisherName.DisplayMemberPath = "Name";
            cmbPublisherName.SelectedValuePath = "PublisherId";
            cmbPublisherName.SelectedIndex = -1;

            if (model != null && model.BookId != 0)
            {
                cmbBookTypeName.SelectedValue = model.BookTypeId;
                cmbPublisherName.SelectedValue = model.PublisherId;
                txtTitle.Text = model.Title;
                txtPublishYear.Text = model.PublishYear.ToString();
                txtStock.Text = model.Stock.ToString();

            }
        }

        public BookModel model;
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if( cmbBookTypeName.SelectedIndex == -1 || txtTitle.Text.Trim() == "")
            {
                MessageBox.Show("Please fill all areas!");
            } else { 

                if (model !=null && model.BookId != 0)
                {
                    Book bt = new Book();
                    bt.BookId = model.BookId; 
                    bt.BookTypeId = (int)cmbBookTypeName.SelectedValue;
                    bt.PublisherId = (int)cmbPublisherName.SelectedValue;
                    bt.PublishYear = Convert.ToInt32(txtPublishYear.Text);
                    bt.Title = txtTitle.Text;
                    bt.Stock = Convert.ToInt32(txtStock.Text);
                    bt.Active = true;
                    db.Books.Update(bt); 
                    db.SaveChanges();
                    MessageBox.Show("Book was updated!");
                    this.Close();
                } 
                else
                {
                    Book book = new Book();
                    book.BookTypeId = Convert.ToInt32(cmbBookTypeName.SelectedValue);
                    book.PublisherId = Convert.ToInt32(cmbPublisherName.SelectedValue);
                    book.Title = txtTitle.Text;
                    book.PublishYear = Convert.ToInt32(txtPublishYear.Text);
                    book.Stock = Convert.ToInt32(txtStock.Text);
                    db.Books.Add(book);
                    db.SaveChanges();
                    cmbBookTypeName.SelectedIndex = -1;
                    cmbPublisherName.SelectedIndex = -1; 
                    new List<TextBox> { txtTitle, txtPublishYear, txtStock }.ForEach(tb => tb.Clear());
                    MessageBox.Show("Book was added succesfully!");
                    this.Close();
                }

                
            }

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
