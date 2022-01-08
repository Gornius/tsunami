using System;
using System.Collections.Generic;
using DesktopApp.Service;

namespace DesktopApp.News
{
    public class RandomNewsService : INewsRepository
    {
        private readonly int _minCount;
        private readonly int _maxCount;
        private readonly Random _random = new();

        public RandomNewsService(int minCount, int maxCount)
        {
            _maxCount = maxCount;
            _minCount = minCount;
        }

        public int GetArticlesCount(List<string> keywords)
        {
            return _random.Next(_minCount, _maxCount);
        }
    }
}