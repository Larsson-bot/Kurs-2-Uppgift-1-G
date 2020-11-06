using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Uppgift_1_Kurs_2_Simple
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }
        public async Task PickAFile()
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.List;
            openPicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            openPicker.FileTypeFilter.Add(".json");
            openPicker.FileTypeFilter.Add(".csv");
            openPicker.FileTypeFilter.Add(".xml");
            StorageFile file = await openPicker.PickSingleFileAsync();
            if (file != null)
            {
                ChosenFile.Text = "Selected File: " + file.Name;
                LaunchQuerySupportStatus status = await Launcher.QueryFileSupportAsync(file);
                if (status == LaunchQuerySupportStatus.Available)
                {
                    await Launcher.LaunchFileAsync(file);
                }
                else
                {
                    MessageDialog dialog = new MessageDialog($"Status value was {status}. Unable to launch.");
                    await dialog.ShowAsync();
                }
                    var text = await FileIO.ReadTextAsync(file);
                    lwJson.Items.Add(text);               
           }
            else
            {
                ChosenFile.Text = "Wrong input.";
            }
        }
        private void btnMessage_Click(object sender, RoutedEventArgs e)
        {
            PickAFile().GetAwaiter();
        }
    }
}
