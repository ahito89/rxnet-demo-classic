using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace desktop_example.Pages
{
    public static class SearchService
    {
        private static int _attemptNumber = 0;
        private static Random _random = new Random();

        public static Task<(IEnumerable<Product>, SearchStamp)> SearchAsync(
            string searchToken,
            CancellationToken token = default)
        {
            _attemptNumber++;
            var lowercaseToken = searchToken.ToLower();
            return Task.Delay(_random.Next(500, 1000), token)
                .ContinueWith(_ =>
                (
                ProductProvider.All.Where(p =>
                p.Make.ToLower().Contains(lowercaseToken) ||
                p.Model.ToLower().Contains(lowercaseToken)
                ),
                new SearchStamp(_attemptNumber, searchToken)
                ), token);
        }
    }

    public class SearchStamp
    {
        private readonly int _attempt;
        private readonly string _term;
        private readonly DateTime _when;

        public SearchStamp(int attempt, string term)
        {
            _attempt = attempt;
            _term = term;
            _when = DateTime.Now;
        }

        public override string ToString() => $"Attempt #{_attempt} was executed at {_when:T} for search token:[{_term}]";
    }
}
