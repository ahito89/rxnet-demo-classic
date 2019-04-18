using System;
using System.Windows.Controls;
using System.Windows.Threading;
using desktop_example.Pages;

namespace desktop_example_classic.Pages
{
    /// <summary>
    /// Interaction logic for SearchPage.xaml
    /// </summary>
    public partial class SearchPage : UserControl
    {
        private DispatcherTimer _waitTimer;
        private string _lastSearchToken;
        private string _searchCandidate;

        public SearchPage()
        {
            InitializeComponent();
            SearchBox.TextChanged += OnSearchTextChanged;

            _waitTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(0.5) };
            _waitTimer.Tick += OnWaitElapsed;
        }

        private void OnSearchTextChanged(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            var canditateSearchTerm = SearchBox.Text;
            if (canditateSearchTerm.Length < 3)
            {
                _waitTimer.Stop();
                return;
            }
            _searchCandidate = canditateSearchTerm;
            RestartWait();
        }

        private async void OnWaitElapsed(object sender, EventArgs elapsedEventArgs)
        {
            _waitTimer.Stop();
            if (_lastSearchToken == _searchCandidate)
            {
                return;
            }

            try
            {
                _lastSearchToken = _searchCandidate;
                var (products, stamp) = await SearchService.SearchAsync(_lastSearchToken);
                ProductsList.ItemsSource = products;
                TimeBlock.Text = stamp.ToString();
            }
            catch
            {

            }
        }

        private void RestartWait()
        {
            _waitTimer.Stop();
            _waitTimer.Start();

        }
    }
}