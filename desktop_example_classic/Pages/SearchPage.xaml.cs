using System.Windows.Controls;
using desktop_example.Pages;

namespace desktop_example_classic.Pages
{
    /// <summary>
    /// Interaction logic for SearchPage.xaml
    /// </summary>
    public partial class SearchPage : UserControl
    {
        private string _lastSearchToken;

        public SearchPage()
        {
            InitializeComponent();
            SearchBox.TextChanged += OnSearchTextChanged;
        }

        private async void OnSearchTextChanged(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            var canditateSearchTerm = SearchBox.Text;
            if (canditateSearchTerm.Length < 3)
            {
                return;
            }

            if (_lastSearchToken == canditateSearchTerm)
            {
                return;
            }

            try
            {
                _lastSearchToken = canditateSearchTerm;
                var (products, stamp) = await SearchService.SearchAsync(_lastSearchToken);
                ProductsList.ItemsSource = products;
                TimeBlock.Text = stamp.ToString();
            }
            catch
            {

            }
        }
    }
}
