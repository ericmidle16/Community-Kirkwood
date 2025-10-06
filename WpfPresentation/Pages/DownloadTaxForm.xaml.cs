using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WpfPresentation.Pages
{
    /// <summary>
    /// Interaction logic for DownloadTaxForm.xaml
    /// </summary>
    public partial class DownloadTaxForm : Page
    {
        MainWindow main = MainWindow.GetMainWindow(); // Main window Reference

        public DownloadTaxForm()
        {
            InitializeComponent();
        }

        private void btnDownload_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://www.irs.gov/pub/irs-pdf/f8283.pdf",
                UseShellExecute = true
            });
        }

        /// <summary>
        /// Last Upaded By: Syler Bushlack
        /// Last Updated: 2025/04/30
        /// What Was Changed: added the btnExit_Click method
        /// </summary>
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
