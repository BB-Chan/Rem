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


        //Category button pointer events

        private void CardsButton_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            CardsHoverBG.Visibility = Visibility.Visible;
        }

        private void CardsButton_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            CardsHoverBG.Visibility = Visibility.Collapsed;
        }

        private void PasswordsButton_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            PasswordsHoverBG.Visibility = Visibility.Collapsed;
        }

        private void PasswordsButton_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            PasswordsHoverBG.Visibility = Visibility.Visible;
        }

        private void MailButton_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            MailHoverBG.Visibility = Visibility.Visible;
        }

        private void MailButton_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            MailHoverBG.Visibility = Visibility.Collapsed;
        }

        private void BankButton_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            BankHoverBG.Visibility = Visibility.Visible;
        }

        private void BankButton_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            BankHoverBG.Visibility = Visibility.Collapsed;
        }

        private void UtilitiesButton_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            UtilHoverBG.Visibility = Visibility.Visible;
        }

        private void UtilitiesButton_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            UtilHoverBG.Visibility = Visibility.Collapsed;
        }



        //Category button click events
        private void PasswordsButton_Click(object sender, RoutedEventArgs e)
        {
            PasswordsPressBG.Visibility = Visibility.Visible;

            UtilPressBG.Visibility = Visibility.Collapsed;
            BankPressBG.Visibility = Visibility.Collapsed;
            MailPressBG.Visibility = Visibility.Collapsed;
            CardsPressBG.Visibility = Visibility.Collapsed;
        }

        private void MailButton_Click(object sender, RoutedEventArgs e)
        {
            MailPressBG.Visibility = Visibility.Visible;

            PasswordsPressBG.Visibility = Visibility.Collapsed;
            BankPressBG.Visibility = Visibility.Collapsed;
            UtilPressBG.Visibility = Visibility.Collapsed;
            CardsPressBG.Visibility = Visibility.Collapsed;
        }

        private void UtilitiesButton_Click(object sender, RoutedEventArgs e)
        {
            UtilPressBG.Visibility = Visibility.Visible;

            PasswordsPressBG.Visibility = Visibility.Collapsed;
            BankPressBG.Visibility = Visibility.Collapsed;
            MailPressBG.Visibility = Visibility.Collapsed;
            CardsPressBG.Visibility = Visibility.Collapsed;
        }

        private void BankButton_Click(object sender, RoutedEventArgs e)
        {
            BankPressBG.Visibility = Visibility.Visible;

            PasswordsPressBG.Visibility = Visibility.Collapsed;
            CardsPressBG.Visibility = Visibility.Collapsed;
            MailPressBG.Visibility = Visibility.Collapsed;
            UtilPressBG.Visibility = Visibility.Collapsed;
        }

        private void CardsButton_Click(object sender, RoutedEventArgs e)
        {
            CardsPressBG.Visibility = Visibility.Visible;

            PasswordsPressBG.Visibility = Visibility.Collapsed;
            BankPressBG.Visibility = Visibility.Collapsed;
            MailPressBG.Visibility = Visibility.Collapsed;
            UtilPressBG.Visibility = Visibility.Collapsed;
        }
    }

}
