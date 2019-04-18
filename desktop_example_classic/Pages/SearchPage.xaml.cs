using System;
using System.Linq;
using System.Reactive.Linq;
using System.Windows.Controls;
using desktop_example.Pages;

namespace desktop_example_classic.Pages
{
    /// <summary>
    /// Interaction logic for SearchPage.xaml
    /// </summary>
    public partial class SearchPage : UserControl
    {
        public SearchPage()
        {
            InitializeComponent();
            Observable.FromEventPattern(SearchBox, nameof(SearchBox.TextChanged))
                .Select(_ => SearchBox.Text)
                .Where(newSearchToken => newSearchToken.Length > 2)
                .Throttle(TimeSpan.FromSeconds(0.5))
                .DistinctUntilChanged()
                .Select(searchToken => SearchService.SearchAsync(searchToken))
                .Switch()
                .ObserveOnDispatcher()
                .Subscribe(result =>
                {
                    var (products, stamp) = result;
                    ProductsList.ItemsSource = products;
                    TimeBlock.Text = stamp.ToString();
                });
        }
    }
}
