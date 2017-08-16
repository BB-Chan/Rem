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
            TextBox[] infoBoxArr = { usernameTextbox, passwordTextbox, accountTextbox, sq1_Textbox, sq2_Textbox, sqa1_Textbox, sqa2_Textbox, accNoTextbox, codeTextbox };

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
            TextBox[] infoBoxArr = { usernameTextbox, accountTextbox, passwordTextbox, sq1_Textbox, sq2_Textbox, sqa1_Textbox, sqa2_Textbox, accNoTextbox, codeTextbox };

            //Loop through array to manipulate properties
            for (int i = 0; i < infoBoxArr.Length; i++)
            {
                infoBoxArr[i].IsReadOnly = false;
                infoBoxArr[i].IsEnabled = true;
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
                var query = conn.Table<Passwords>();
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
                        Accnumber = item.Accnumber
                    });
                }
            }
            //mail
            else if (CategoryListbox.SelectedIndex == 1)
            {
                var query = conn.Table<Mail>();
                foreach (var item in query)
                {
                    NewMail.Add(new Mail
                    {
                        Account = item.Account,
                        Username = item.Username,
                        Password = item.Password
                    });
                }
            }
            //wallet
            else if (CategoryListbox.SelectedIndex == 2)
            {
                var query = conn.Table<Wallet>();
                foreach (var item in query)
                {
                    NewWallet.Add(new Wallet
                    {
                        Account = item.Account,
                        CardNo = item.CardNo,
                        ExpDate = item.ExpDate,
                        CVC = item.CVC
                    });
                }
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
            TextBox [] infoBoxArr = { usernameTextbox, passwordTextbox, accountTextbox, sq1_Textbox, sq2_Textbox, sqa1_Textbox, sqa2_Textbox, accNoTextbox, codeTextbox };

            //Loop through array to manipulate properties
            for (int i = 0; i < infoBoxArr.Length; i++)
            {
                infoBoxArr[i].IsReadOnly = true;
                infoBoxArr[i].IsEnabled = false;
                
            }
        }



        private void createButton_Click(object sender, RoutedEventArgs e)
        {
            //Create arr for infoboxes
            TextBox[] infoBoxArr = { usernameTextbox, passwordTextbox, accountTextbox, sq1_Textbox, sq2_Textbox, sqa1_Textbox, sqa2_Textbox, accNoTextbox, codeTextbox };

            //Set account & username text to textbox text
            pw.Account = accountTextbox.Text;
            pw.Username = usernameTextbox.Text;

            //passwords
            if(CategoryListbox.SelectedIndex == 0)
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
        }



        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            TextBox[] infoBoxArr = { usernameTextbox, passwordTextbox, accountTextbox, sq1_Textbox, sq2_Textbox, sqa1_Textbox, sqa2_Textbox, accNoTextbox, codeTextbox };

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
            }
            //mail
            else if (CategoryListbox.SelectedIndex == 1)
            {
                NewMail.Remove((Mail)AccountListBox.SelectedItem);
            }
            //wallet
            else if (CategoryListbox.SelectedIndex == 2)
            {
                NewWallet.Remove((Wallet)AccountListBox.SelectedItem);
            }
            
        }



        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            TextBox[] infoBoxArr = { usernameTextbox, passwordTextbox, accountTextbox, sq1_Textbox, sq2_Textbox, sqa1_Textbox, sqa2_Textbox, accNoTextbox, codeTextbox };

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
            TextBox[] infoBoxArr = { usernameTextbox, passwordTextbox, accountTextbox, sq1_Textbox, sq2_Textbox, sqa1_Textbox, sqa2_Textbox, accNoTextbox, codeTextbox };

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
                Account = pw.Account,
                Username = pw.Username,
                Password = pw.Password,
                SQ1 = pw.SQ1,
                SQA1 = pw.SQA1,
                SQ2 = pw.SQ2,
                SQA2 = pw.SQA2,
                Code = pw.Code,
                Accnumber = pw.Accnumber,
            });
        }
        public void AddMailAcc()
        {
            NewMail.Add(new Mail
            {
                Account = accountTextbox.Text,
                Username = usernameTextbox.Text,
                Password = passwordTextbox.Text
            });

            //Add curr text in texboxes to new row in SQL db
            conn.Insert(new Mail()
            {
                Account = mail.Account,
                Username = mail.Username,
                Password = mail.Password
            });
        }
        public void AddWalletAcc()
        {
            NewWallet.Add(new Wallet
            {
                Account = accountTextbox.Text,
                
            });

            //Add curr text in texboxes to new row in SQL db
            conn.Insert(new Wallet()
            {
                Account = wallet.Account,
                CardNo = wallet.CardNo,
                ExpDate = wallet.ExpDate,
                CVC = wallet.CVC
            });
        }

        ///Category info box visibility
        public void PWUI()
        {
            //show controls

            //hide controls
        }
        public void MailUI()
        {
            //show controls

            //hide controls
        }
        public void WalletUI()
        {
            //show controls

            //hide controls
        }
    }
}
