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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Rem
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
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

        }

        private void searchBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


        //Add & Settings click events
        private void newItemBtn_Click(object sender, RoutedEventArgs e)
        {

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

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordLBI.IsSelected)
            {

            }
            else if (MailLBI.IsSelected)
            {

            }
            else if (CardsLBI.IsSelected)
            {

            }
            else if (BankLBI.IsSelected)
            {

            }
        }
    }

}
