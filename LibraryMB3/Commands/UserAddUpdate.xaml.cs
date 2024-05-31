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
using System.Windows.Shapes;

namespace LibraryMB3.Commands
{
    /// <summary>
    /// Interaction logic for UserAddUpdate.xaml
    /// </summary>
    public partial class UserAddUpdate : Window
    {
        public UserAddUpdate()
        {
            InitializeComponent();
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public User user;
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (txtFirstName.Text.Trim() == "")
            {
                MessageBox.Show("Please fill the Book Type name area!");
            }
            else
            {
                using (DbLibraryMbContext db = new DbLibraryMbContext())
                {
                    if (user != null && user.UserId != 0)
                    {
                        User update = new User();
                        update.UserId = user.UserId;
                        update.FirstName = txtFirstName.Text;
                        update.LastName = txtLastName.Text;
                        update.Email = txtEmail.Text;
                        update.Phone = txtPhone.Text;
                        update.Active = true;
                        db.Users.Update(update);
                        db.SaveChanges();
                        MessageBox.Show("User was updated succesfully");
                        this.Close();

                    }
                    else
                    {
                        User ut = new User();
                        ut.FirstName = txtFirstName.Text;
                        ut.LastName = txtLastName.Text;
                        ut.Email = txtEmail.Text;
                        ut.Phone = txtPhone.Text;
                        ut.Active = true;
                        db.Users.Add(ut);
                        db.SaveChanges();
                        new List<TextBox> { txtFirstName, txtLastName, txtEmail, txtPhone }.ForEach(tb => tb.Clear());
                        MessageBox.Show("User was added succesfully!");
                        this.Close();
                    }
                }
            }
        }

        private void WindowUser_Loaded(object sender, RoutedEventArgs e)
        {
            if (user != null && user.UserId != 0)
            {
                txtFirstName.Text = user.FirstName.ToString();
                txtLastName.Text = user.LastName.ToString();
                txtEmail.Text = user.Email.ToString();
                txtPhone.Text = user.Phone.ToString();

            }
        }
    }
}
