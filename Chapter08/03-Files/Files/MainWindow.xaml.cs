using CommunityToolkit.WinUI.Helpers;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Files
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private async void OnGetFolderPath(object sender, RoutedEventArgs e)
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var folder = await StorageFolder.GetFolderFromPathAsync(path);
            txtFolderPath.Text = folder.Path;
        }

        private async void OnGetFolders(object sender, RoutedEventArgs e)
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var folder = await StorageFolder.GetFolderFromPathAsync(path);
            var folders = await folder.GetFoldersAsync();
            StringBuilder folderPaths = new StringBuilder();
            foreach (var folderItem in folders)
            {
                folderPaths.AppendLine(folderItem.Path);
            }
            txtFolderPath.Text = folderPaths.ToString();
        }

        private async void OnGetFiles(object sender, RoutedEventArgs e)
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var folder = await StorageFolder.GetFolderFromPathAsync(path);
            var files = await folder.GetFilesAsync();
            StringBuilder filesPaths = new StringBuilder();
            foreach (StorageFile file in files)
            {
                filesPaths.AppendLine(file.Path);
            }
            txtFilesPath.Text = filesPaths.ToString();
        }

        private async void OnGetFileFromPath(object sender, RoutedEventArgs e)
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string filePath = $@"{ path}\MyFile.txt";
            StorageFile file = await StorageFile.GetFileFromPathAsync(filePath);
        }

        private async void OnCreateFileRAS(object sender, RoutedEventArgs e)
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var folder = await StorageFolder.GetFolderFromPathAsync(path);

            StorageFile file = await folder.CreateFileAsync("file.txt", CreationCollisionOption.ReplaceExisting);

            IRandomAccessStream randomAccessStream = await file.OpenAsync(FileAccessMode.ReadWrite);

            using (DataWriter writer = new DataWriter(randomAccessStream.GetOutputStreamAt(0)))
            {
                writer.WriteString("Hello world!");
                await writer.StoreAsync();
                await writer.FlushAsync();
            }

            txtFilesPath.Text = file.Path;
        }

        private async void OnCreateFileStreamWriter(object sender, RoutedEventArgs e)
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var folder = await StorageFolder.GetFolderFromPathAsync(path);
            StorageFile file = await folder.CreateFileAsync("file.txt", CreationCollisionOption.ReplaceExisting);
            using (var stream = await file.OpenStreamForWriteAsync())
            {
                StreamWriter writer = new StreamWriter(stream);
                writer.WriteLine("Hello world");
                await writer.FlushAsync();
            }

            txtFilesPath.Text = file.Path;
        }

        private async void OnCreateFileWithFileIO(object sender, RoutedEventArgs e)
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var folder = await StorageFolder.GetFolderFromPathAsync(path);
            StorageFile file = await folder.CreateFileAsync("file.txt", CreationCollisionOption.ReplaceExisting);
            //write the content to the file
            await FileIO.WriteTextAsync(file, "Hello world");
            //read the content from the file
            string content = await FileIO.ReadTextAsync(file);
            txtFilesPath.Text = content;
        }

        private async void OnDownloadFile(object sender, RoutedEventArgs e)
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var folder = await StorageFolder.GetFolderFromPathAsync(path);
            bool isExisting = await folder.FileExistsAsync("image.jpg");
            if (!isExisting)
            {
                StorageFile file = await folder.CreateFileAsync("image.jpg", CreationCollisionOption.ReplaceExisting);
                await StreamHelper.GetHttpStreamToStorageFileAsync(new Uri("https://www.packtpub.com/media/logo/stores/1/logo.png"), file);
                txtFilesPath.Text = file.Path;
            }
        }

        private async void OnCreateFileLocalStorage(object sender, RoutedEventArgs e)
        {
            StorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync("file.txt", CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(file, "Hello world");
            txtLocalStoragePath.Text = file.Path;
        }

        private async void OnCreateFileLocalStorageWithHelpers(object sender, RoutedEventArgs e)
        {
            var file = await StorageFileHelper.WriteTextToLocalFileAsync("Hello world", "file.txt", CreationCollisionOption.ReplaceExisting);
            await StorageFileHelper.WriteTextToLocalCacheFileAsync("Hello world", "file.txt", CreationCollisionOption.ReplaceExisting);

            txtLocalStoragePath.Text = file.Path;
        }

        private void OnWriteSettings(object sender, RoutedEventArgs e)
        {
            ApplicationData.Current.LocalSettings.Values["Theme"] = "Dark";
            var value = ApplicationData.Current.LocalSettings.Values["Theme"].ToString();
            txtLocalStoragePath.Text = value;
        }

        private void OnWriteSettingsWithHelper(object sender, RoutedEventArgs e)
        {
            var helper = ApplicationDataStorageHelper.GetCurrent();
            helper.Save<string>("Theme", "Dark");
            var value = helper.Read<string>("Theme");
            txtLocalStoragePath.Text = value;
        }

        private async void OnPickFile(object sender, RoutedEventArgs e)
        {
            var filePicker = new FileOpenPicker();

            // Get the current window's HWND by passing in the Window object
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(this);

            // Associate e HWND with the file picker
            WinRT.Interop.InitializeWithWindow.Initialize(filePicker, hwnd);

            filePicker.FileTypeFilter.Add("*");
            StorageFile file = await filePicker.PickSingleFileAsync();
            if (file != null)
            {
                txtPickerPath.Text = file.Path;
            }
        }

        private async void OnSaveFile(object sender, RoutedEventArgs e)
        {
            var filePicker = new FileSavePicker();

            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(this);

            WinRT.Interop.InitializeWithWindow.Initialize(filePicker, hwnd);

            filePicker.SuggestedFileName = "file.txt";
            filePicker.FileTypeChoices.Add("Text", new List<string>() { ".txt" });
            StorageFile file = await filePicker.PickSaveFileAsync();

            if (file != null)
            {
                await FileIO.WriteTextAsync(file, "Hello world");
                txtPickerPath.Text = file.Path;
            }
        }

        private async void OnPickFolder(object sender, RoutedEventArgs e)
        {
            var folderPicker = new FolderPicker();

            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
            WinRT.Interop.InitializeWithWindow.Initialize(folderPicker, hwnd);

            StorageFolder folder = await folderPicker.PickSingleFolderAsync();

            if (folder != null)
            {
                txtPickerPath.Text = folder.Path;
            }
        }
    }
}
