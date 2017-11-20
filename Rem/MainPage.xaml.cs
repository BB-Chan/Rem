using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.ViewManagement;
using Rem.Models;
using System.Collections.ObjectModel;
using SQLitePCL;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Rem
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage
    {
        //Reference classes for x:bind
        Passwords pw = new Passwords();
        Mail mail = new Mail();
        Wallet wallet = new Wallet();

        ObservableCollection<Passwords> NewPassword = new ObservableCollection<Passwords>();
        ObservableCollection<Mail> NewMail = new ObservableCollection<Mail>();
        ObservableCollection<Wallet> NewWallet = new ObservableCollection<Wallet>();

        //SQL setup
        SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.
            SQLitePlatformWinRT(), Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "RemAccounts.sqlite"));
        int _id { get; set; }

        public MainPage()
        {
            this.InitializeComponent();
            
            //catches current titlebar and sets custom shape as titlebar
            CoreApplicationViewTitleBar coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;
            Window.Current.SetTitleBar(titlebar);

            //set cur titlebar as var titleBar
            var titleBar = ApplicationView.GetForCurrentView().TitleBar;
            //Titlebar colors
            titleBar.ButtonBackgroundColor = Colors.Transparent;
            titleBar.ButtonForegroundColor = Colors.White;
            titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;

            //create array for info textboxes
            TextBox[] infoBoxArr = { usernameTextbox, cvcTextbox, passwordTextbox, accountTextbox, sq1_Textbox, sq2_Textbox, sqa1_Textbox, sqa2_Textbox, accNoTextbox, codeTextbox };

            //loop through array to manipulate properties
            for (int i = 0; i < infoBoxArr.Length; i++)
            {
                infoBoxArr[i].IsReadOnly = true;
                infoBoxArr[i].IsEnabled = false;
            }

            //create new tables
            conn.CreateTable<Passwords>();
            conn.CreateTable<Mail>();
            conn.CreateTable<Wallet>();
            //select first category
            CategoryListbox.SelectedIndex = 0;

            

        }


        
        private void searchBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }



        //Add & Settings click events
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            //Show & hide different controls
            deleteButton.Visibility = Visibility.Collapsed;
            editButton.Visibility = Visibility.Collapsed;
            createButton.Visibility = Visibility.Visible;
            cancelButton.Visibility = Visibility.Visible;

            //Create array for info textboxes
            TextBox[] infoBoxArr = { usernameTextbox, cvcTextbox, accountTextbox, passwordTextbox, sq1_Textbox, sq2_Textbox, sqa1_Textbox, sqa2_Textbox, accNoTextbox, codeTextbox };

            //Loop through array to manipulate properties
            for (int i = 0; i < infoBoxArr.Length; i++)
            {
                infoBoxArr[i].IsReadOnly = false;
                infoBoxArr[i].IsEnabled = true;
                infoBoxArr[i].Text = "";
            }
        }



        private void settingsBtn_Click(object sender, RoutedEventArgs e)
        {
            
        }



        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            CategorySplitview.IsPaneOpen = !CategorySplitview.IsPaneOpen;
        }



        ///Category change
        private void CategoryListbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int lbindex = 0;

            //Hide accept & cancel button if they were previously visible
            hideInfoBtns();

            //Clear lbitems
            while (AccountListBox.Items.Count > 0)
            {
                AccountListBox.SelectedIndex = 0;
                
                //passwords
                if (lbindex == 0)
                {
                    NewPassword.Remove((Passwords)AccountListBox.Items[0]);
                }
                //mail
                else if (lbindex == 1)
                {
                    NewMail.Remove((Mail)AccountListBox.Items[0]);
                }
                //wallet
                else if (lbindex == 2)
                {
                    NewWallet.Remove((Wallet)AccountListBox.Items[0]);
                }
                
            }

            lbindex = AccountListBox.SelectedIndex;

            ///Load lbitems into currently selected category
            //passwords
            if (CategoryListbox.SelectedIndex == 0)
            {
                //load lbitems alphabetically
                var query = conn.Query<Passwords>(
                "SELECT * FROM Passwords ORDER BY Passwords.Account ASC;");

                foreach (var item in query)
                {
                    NewPassword.Add(new Passwords
                    {
                        Account = item.Account,
                        Username = item.Username,
                        Password = item.Password,
                        SQ1 = item.SQ1,
                        SQA1 = item.SQA1,
                        SQ2 = item.SQ2,
                        SQA2 = item.SQA2,
                        Code = item.Code,
                        Accnumber = item.Accnumber,
                    });
                }


                PWUI();
            }
            //mail
            else if (CategoryListbox.SelectedIndex == 1)
            {
                var query = conn.Query<Mail>(
                "SELECT * FROM Mail ORDER BY Mail.Account ASC;");

                foreach (var item in query)
                {
                    NewPassword.Add(new Passwords
                    {
                        Account = item.Account,
                        Username = item.Username,
                        Password = item.Password
                    });
                }
                
                MailUI();
            }
            //wallet
            else if (CategoryListbox.SelectedIndex == 2)
            {
                var query = conn.Query<Wallet>(
                "SELECT * FROM Wallet ORDER BY Wallet.Account ASC;");
                foreach (var item in query)
                {
                    NewPassword.Add(new Passwords
                    {
                        Account = item.Account,
                        Username = item.CardNo
                    });
                }
                WalletUI();
            }
        }



        private void searchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            searchBox.SelectionStart = 0;
            searchBox.SelectionLength = searchBox.Text.Length;
        }

        private void AccountListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Show & hide different controls
            deleteButton.Visibility = Visibility.Visible;
            editButton.Visibility = Visibility.Visible;
            createButton.Visibility = Visibility.Collapsed;
            cancelButton.Visibility = Visibility.Collapsed;

            //Create array for info textboxes
            TextBox [] infoBoxArr = { usernameTextbox, cvcTextbox, passwordTextbox, accountTextbox, sq1_Textbox, sq2_Textbox, sqa1_Textbox, sqa2_Textbox, accNoTextbox, codeTextbox };

            //Loop through array to manipulate properties
            for (int i = 0; i < infoBoxArr.Length; i++)
            {
                infoBoxArr[i].IsReadOnly = true;
                infoBoxArr[i].IsEnabled = false;
            }

            //passwords
            if (CategoryListbox.SelectedIndex == 0)
            {

                var query = conn.Query<Passwords>(
                    "SELECT * FROM Passwords ORDER BY Passwords.Account ASC;");

                int counter = 0;
                foreach (var item in query)
                {
                    if (counter == AccountListBox.SelectedIndex)
                    {
                        accountTextbox.Text = item.Account;
                        usernameTextbox.Text = item.Username;
                        passwordTextbox.Text = item.Password;
                        sq1_Textbox.Text = item.SQ1;
                        sqa1_Textbox.Text = item.SQA1;
                        sq2_Textbox.Text = item.SQ2;
                        sqa2_Textbox.Text = item.SQA2;
                        codeTextbox.Text = item.Code;
                        accNoTextbox.Text = item.Accnumber;
                    }
                    counter++;
                }
                
            }
            //mail
            else if (CategoryListbox.SelectedIndex == 1)
            {
                var query = conn.Query<Mail>(
                    "SELECT * FROM Mail ORDER BY Mail.Account ASC;");

                int counter = 0;
                foreach (var item in query)
                {
                    if (counter == AccountListBox.SelectedIndex)
                    {
                        accountTextbox.Text = item.Account;
                        usernameTextbox.Text = item.Username;
                        passwordTextbox.Text = item.Password;
                    }
                    counter++;
                }
            }
            //wallet
            else if (CategoryListbox.SelectedIndex == 2)
            {
                var query = conn.Query<Wallet>(
                    "SELECT * FROM Wallet ORDER BY Wallet.Account ASC;");

                int counter = 0;
                foreach (var item in query)
                {
                    if (counter == AccountListBox.SelectedIndex)
                    {
                        accountTextbox.Text = item.Account;
                        passwordTextbox.Text = item.ExpDate;
                        usernameTextbox.Text = item.CardNo;
                        cvcTextbox.Text = item.CVC;
                    }
                    counter++;
                }
            }
        }



        private void createButton_Click(object sender, RoutedEventArgs e)
        {
            //Create arr for infoboxes
            TextBox[] infoBoxArr = { usernameTextbox, cvcTextbox, passwordTextbox, accountTextbox, sq1_Textbox, sq2_Textbox, sqa1_Textbox, sqa2_Textbox, accNoTextbox, codeTextbox };

            //Set account & username text to textbox text
            pw.Account = accountTextbox.Text;
            pw.Username = usernameTextbox.Text;
            mail.Account = accountTextbox.Text;
            mail.Username = usernameTextbox.Text;
            wallet.Account = accountTextbox.Text;
            wallet.CardNo = usernameTextbox.Text;

            //passwords
            if (CategoryListbox.SelectedIndex == 0)
            {
                AddPWAcc();
            }
            //mail
            else if (CategoryListbox.SelectedIndex == 1)
            {
                AddMailAcc();
            }
            //wallet
            else if (CategoryListbox.SelectedIndex == 2)
            {
                AddWalletAcc();
            }
                
            //Select last created listboxitem   
            AccountListBox.SelectedIndex = AccountListBox.Items.Count - 1;

            //Clear textboxes
            for (int i =0; i < infoBoxArr.Length; i++)
            {
                infoBoxArr[i].Text = "";
            }

            //Sort account list alphabetically
        }

        

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            //textbox array
            TextBox[] infoBoxArr = { usernameTextbox, cvcTextbox, passwordTextbox, accountTextbox, sq1_Textbox, sq2_Textbox, sqa1_Textbox, sqa2_Textbox, accNoTextbox, codeTextbox };

            //Clear textboxes
            for (int i = 0; i < infoBoxArr.Length; i++)
            {
                infoBoxArr[i].Text = "";
            }

            //Delete lbitem
            //passwords
            if (CategoryListbox.SelectedIndex == 0)
            {
                NewPassword.Remove((Passwords)AccountListBox.SelectedItem);

                var query = conn.Query<Passwords>(
                    "SELECT * FROM Passwords ORDER BY Passwords.Account ASC;");

                int counter = 0;

                
                foreach (var item in query)
                {
                    if (counter == AccountListBox.SelectedIndex)
                    {
                        conn.Delete<Passwords>(
                            "DELETE FROM Passwords WHERE Passwords.Account = " + item.Account + ";");
                    }
                    counter++;
                }
            }
            //mail
            else if (CategoryListbox.SelectedIndex == 1)
            {
                NewPassword.Remove((Passwords)AccountListBox.SelectedItem);
            }
            //wallet
            else if (CategoryListbox.SelectedIndex == 2)
            {
                NewPassword.Remove((Passwords)AccountListBox.SelectedItem);
            }
            
        }



        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            TextBox[] infoBoxArr = { usernameTextbox, cvcTextbox, passwordTextbox, accountTextbox, sq1_Textbox, sq2_Textbox, sqa1_Textbox, sqa2_Textbox, accNoTextbox, codeTextbox };

            //Show & hide different controls
            deleteButton.Visibility = Visibility.Collapsed;
            editButton.Visibility = Visibility.Collapsed;
            cancelButton.Visibility = Visibility.Visible;
            acceptButton.Visibility = Visibility.Visible;

            //Loop through array to manipulate properties
            for (int i = 0; i < infoBoxArr.Length; i++)
            {
                infoBoxArr[i].IsReadOnly = false;
                infoBoxArr[i].IsEnabled = true;
            }
        }



        private void acceptButton_Click(object sender, RoutedEventArgs e)
        {
            TextBox[] infoBoxArr = { usernameTextbox, cvcTextbox, passwordTextbox, accountTextbox, sq1_Textbox, sq2_Textbox, sqa1_Textbox, sqa2_Textbox, accNoTextbox, codeTextbox };

            //Show & hide different controls
            deleteButton.Visibility = Visibility.Visible;
            editButton.Visibility = Visibility.Visible;
            cancelButton.Visibility = Visibility.Collapsed;
            acceptButton.Visibility = Visibility.Collapsed;

            //Loop through array to manipulate properties
            for (int i = 0; i < infoBoxArr.Length; i++)
            {
                infoBoxArr[i].IsReadOnly = true;
                infoBoxArr[i].IsEnabled = false;
            }
        }

        public void hideInfoBtns()
        {
            acceptButton.Visibility = Visibility.Collapsed;
            cancelButton.Visibility = Visibility.Collapsed;
        }

        ///Add new account methods
        public void AddPWAcc()
        {
            NewPassword.Add(new Passwords
            {
                Account = accountTextbox.Text,
                Username = usernameTextbox.Text,
                Password = passwordTextbox.Text,
                SQ1 = sq1_Textbox.Text,
                SQA1 = sqa1_Textbox.Text,
                SQ2 = sq2_Textbox.Text,
                SQA2 = sqa2_Textbox.Text,
                Code = codeTextbox.Text,
                Accnumber = accNoTextbox.Text,
            });

            

            //Add curr text in texboxes to new row in SQL db
            conn.Insert(new Passwords()
            {
                Account = accountTextbox.Text,
                Username = usernameTextbox.Text,
                Password = passwordTextbox.Text,
                SQ1 = sq1_Textbox.Text,
                SQA1 = sqa1_Textbox.Text,
                SQ2 = sq2_Textbox.Text,
                SQA2 = sqa2_Textbox.Text,
                Code = codeTextbox.Text,
                Accnumber = accNoTextbox.Text
            });
        }
        public void AddMailAcc()
        {
            pw.Account = accountTextbox.Text;
            pw.Username = usernameTextbox.Text;

            NewPassword.Add(new Passwords
            {
                Account = accountTextbox.Text,
                Username = usernameTextbox.Text,
                Password = passwordTextbox.Text
            });


            //Add curr text in texboxes to new row in SQL db
            conn.Insert(new Mail()
            {
                Account = accountTextbox.Text,
                Username = usernameTextbox.Text,
                Password = passwordTextbox.Text
            });
        }
        public void AddWalletAcc()
        {
            pw.Account = accountTextbox.Text;
            pw.Username = usernameTextbox.Text;

            NewPassword.Add(new Passwords
            {
                Account = accountTextbox.Text,
                Username = usernameTextbox.Text,
                CardNo = usernameTextbox.Text,
                CVC = cvcTextbox.Text,
                ExpDate = passwordTextbox.Text
            });

            //Add curr text in texboxes to new row in SQL db
            conn.Insert(new Wallet()
            {
                Account = accountTextbox.Text,
                CardNo = usernameTextbox.Text,
                CVC = cvcTextbox.Text,
                ExpDate = passwordTextbox.Text
            });
        }

        ///Category info box visibility
        public void PWUI()
        {
            //show controls
            PWGrid.Visibility = Visibility.Visible;
            usernameTextbox.Visibility = Visibility.Visible;
            usernameTxtBlock.Visibility = Visibility.Visible;
            passwordTextbox.Visibility = Visibility.Visible;
            pwTxtBlock.Visibility = Visibility.Visible;
            //hide controls
            WalletGrid.Visibility = Visibility.Collapsed;
        }
        public void MailUI()
        {
            //show controls
            usernameTextbox.Visibility = Visibility.Visible;
            usernameTxtBlock.Visibility = Visibility.Visible;
            passwordTextbox.Visibility = Visibility.Visible;
            pwTxtBlock.Visibility = Visibility.Visible;
            //hide controls
            PWGrid.Visibility = Visibility.Collapsed;
            WalletGrid.Visibility = Visibility.Collapsed;
        }
        public void WalletUI()
        {
            //show controls
            WalletGrid.Visibility = Visibility.Visible;
            usernameTextbox.Visibility = Visibility.Visible;

            //hide controls
            PWGrid.Visibility = Visibility.Collapsed;
            usernameTxtBlock.Visibility = Visibility.Collapsed;
            pwTxtBlock.Visibility = Visibility.Collapsed;
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            TextBox[] infoBoxArr = { usernameTextbox, cvcTextbox, passwordTextbox, accountTextbox, sq1_Textbox, sq2_Textbox, sqa1_Textbox, sqa2_Textbox, accNoTextbox, codeTextbox };

            //Show & hide different controls
            deleteButton.Visibility = Visibility.Visible;
            editButton.Visibility = Visibility.Visible;
            cancelButton.Visibility = Visibility.Collapsed;
            acceptButton.Visibility = Visibility.Collapsed;
            createButton.Visibility = Visibility.Collapsed;

            //Loop through array to manipulate properties
            for (int i = 0; i < infoBoxArr.Length; i++)
            {
                infoBoxArr[i].IsReadOnly = true;
                infoBoxArr[i].IsEnabled = false;
            }
        }
    }
}
