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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Rem
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage
    {
        Accounts acc = new Accounts();
        ObservableCollection<Accounts> NewAccount = new ObservableCollection<Accounts>();

        public MainPage()
        {
            this.InitializeComponent();
        
            

            //Catches current titlebar and sets custom shape as titlebar
            CoreApplicationViewTitleBar coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;
            Window.Current.SetTitleBar(titlebar);

            //Catches the current title bar as a variable
            var titleBar = ApplicationView.GetForCurrentView().TitleBar;

            //Settings titlebar colors
            titleBar.ButtonBackgroundColor = Colors.Transparent;
            titleBar.ButtonForegroundColor = Colors.White;
            titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;



            //Create array for info textboxes
            TextBox[] infoBoxArr = { usernameTextbox, passwordTextbox, accountTextbox, sq1_Textbox, sq2_Textbox, sqa1_Textbox, sqa2_Textbox, accNoTextbox, codeTextbox };

            //Loop through array to manipulate properties
            for (int i = 0; i < infoBoxArr.Length; i++)
            {
                infoBoxArr[i].IsReadOnly = true;
                infoBoxArr[i].IsEnabled = false;
            }
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

        private void CategoryListbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
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
            TextBox[] infoBoxArr = { usernameTextbox, passwordTextbox, accountTextbox, sq1_Textbox, sq2_Textbox, sqa1_Textbox, sqa2_Textbox, accNoTextbox, codeTextbox };

            acc.Account = accountTextbox.Text;
            acc.Username = usernameTextbox.Text;

            //Create new account and select it
            NewAccount.Add(new Accounts
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
                
                
            AccountListBox.SelectedIndex = AccountListBox.Items.Count - 1;
            

            //Clear textboxes
            for(int i =0; i < infoBoxArr.Length; i++)
            {
                infoBoxArr[i].Text = "";
            }
            
        }
    }
}
