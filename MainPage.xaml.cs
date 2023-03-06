using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.ApplicationModel.DataTransfer;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;

using FPText.Views;

namespace FPText
{
    public sealed partial class MainPage : Page
    {
        public bool statics = false;
        private StorageFile awayfile;
        DataPackage dataPackage = new DataPackage();
        ProgressBar loading = new ProgressBar();
        public MainPage()
        {
            this.InitializeComponent();
            loading.Width = 100;
            loading.Value = 0;
            loading.HorizontalAlignment = HorizontalAlignment.Center;
            loading.VerticalAlignment = VerticalAlignment.Center;
        }
        private async void Open_Click(object sender, RoutedEventArgs e)
        {
            BoxEditorGrid.Children.Add(loading); loading.Value = 0;
            var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView(); loading.Value = 10;
            var openPicker = new Windows.Storage.Pickers.FileOpenPicker(); loading.Value = 20;
            openPicker.FileTypeFilter.Add(".txt");
            openPicker.FileTypeFilter.Add(".doc");
            openPicker.FileTypeFilter.Add(".docx");
            var file = await openPicker.PickSingleFileAsync(); loading.Value = 30;
            if (file != null)
            {
                string text = await FileIO.ReadTextAsync(file); loading.Value = 40;
                Editor.Text = text; loading.Value = 50;

                //await new Windows.UI.Popups.MessageDialog(resourceLoader.GetString("OpenFile")).ShowAsync();
                statics = true; awayfile = file; loading.Value = 70;
            }
            loading.Value = 100; BoxEditorGrid.Children.Remove(loading);
        }
        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            if (Editor.Text != null)
            {
                BoxEditorGrid.Children.Add(loading); loading.Value = 0;
                if (statics == true)
                {
                    var file = awayfile; loading.Value = 30;
                    await FileIO.WriteTextAsync(file, Editor.Text); loading.Value = 60;
                }
                else
                    SaveAS_Click(sender, e);
                loading.Value = 60; loading.Value = 100; BoxEditorGrid.Children.Remove(loading);
            }
            else
            {
                var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView();
                await new Windows.UI.Popups.MessageDialog(resourceLoader.GetString("ErrorSave")).ShowAsync();
            }
        }
        private async void SaveAS_Click(object sender, RoutedEventArgs e)
        {
            if (Editor.Text != null)
            {
                BoxEditorGrid.Children.Add(loading); loading.Value = 0;
                var savePicker = new FileSavePicker(); loading.Value = 10;
                savePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary; loading.Value = 30;
                savePicker.FileTypeChoices.Add("New File", new List<string>() { ".txt" }); loading.Value = 50;
                savePicker.SuggestedFileName = "New Document"; loading.Value = 60;
                var file = await savePicker.PickSaveFileAsync(); loading.Value = 70;
                if (file != null)
                {
                    await FileIO.WriteTextAsync(file, Editor.Text); loading.Value = 90;
                }
                loading.Value = 100; BoxEditorGrid.Children.Remove(loading);
            }
            else
            {
                var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView();
                await new Windows.UI.Popups.MessageDialog(resourceLoader.GetString("ErrorSave")).ShowAsync();
            }
        }
        private void As_Click(object sender, RoutedEventArgs e)
        {
            //var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView();
            //await new Windows.UI.Popups.MessageDialog(resourceLoader.GetString("About")).ShowAsync();
            Frame.Navigate(typeof(SettingsPage));
        }
        private void Copy_Click(object sender, RoutedEventArgs e)
        {
            dataPackage.RequestedOperation = DataPackageOperation.Copy;
            Clipboard.SetContent(dataPackage);
        }
        private async void Paste_Click(object sender, RoutedEventArgs e)
        {
            DataPackageView dataPackageView = Clipboard.GetContent();
            if (dataPackageView.Contains(StandardDataFormats.Text))
            {
                string text = await dataPackageView.GetTextAsync();
                Editor.Text = Editor.Text + text;
            }
        }
        private void Cut_Click(object sender, RoutedEventArgs e)
        {
            dataPackage.RequestedOperation = DataPackageOperation.Move;
            Clipboard.SetContent(dataPackage);
        }

        private void TwoTen_Click(object sender, RoutedEventArgs e)
        {
            Editor.FontSize = 12;
        }
        private void ThreeTen_Click(object sender, RoutedEventArgs e)
        {
            Editor.FontSize = 13;
        }
        private void FourTen_Click(object sender, RoutedEventArgs e)
        {
            Editor.FontSize = 14;
        }
        private void FiveTen_Click(object sender, RoutedEventArgs e)
        {
            Editor.FontSize = 15;
        }
        private void SixTen_Click(object sender, RoutedEventArgs e)
        {
            Editor.FontSize = 16;
        }
        private void SevenTen_Click(object sender, RoutedEventArgs e)
        {
            Editor.FontSize = 17;
        }
        private void EightTen_Click(object sender, RoutedEventArgs e)
        {
            Editor.FontSize = 18;

        }
        private void NineTen_Click(object sender, RoutedEventArgs e)
        {
            Editor.FontSize = 19;

        }
        private void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            Editor.FontStyle = Windows.UI.Text.FontStyle.Italic;
        }

        private void MenuFlyoutItem_Click_1(object sender, RoutedEventArgs e)
        {
            Editor.FontStyle = Windows.UI.Text.FontStyle.Normal;
        }
    }
}
