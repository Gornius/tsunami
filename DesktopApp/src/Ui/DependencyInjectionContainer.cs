using System;
using System.Collections.Generic;
using DesktopApp.Data;
using DesktopApp.News;
using DesktopApp.Service;
using DesktopApp.Util;

namespace DesktopApp.Ui
{
    public class DependencyInjectionContainer
    {
        private readonly Dictionary<Type, Func<object>> _factories = new();

        public DependencyInjectionContainer()
        {
            Build();
        }

        private void Build()
        {
            _factories[typeof(IErrorHandler)] = () => new ConsoleErrorHandler();
            _factories[typeof(IVideoRepository)] = () => new YoutubePopularVideosRepository();
            _factories[typeof(ICategoryService)] = () => new YoutubeCategoryService();
            _factories[typeof(INewsRepository)] = () => new RandomNewsService(40, 12000);
            _factories[typeof(ITrendRepository)] = () => new LocalRepository(Get<Database>());
            _factories[typeof(ICategoryRepository)] = () => new LocalRepository(Get<Database>());
            _factories[typeof(DataService)] = () => new DataService(
                Get<IVideoRepository>(),
                Get<ICategoryService>(),
                Get<ICategoryRepository>(),
                Get<INewsRepository>(),
                Get<ITrendRepository>()
            );

            // Singletons
            var database = new Database(Get<IErrorHandler>());
            _factories[typeof(Database)] = () => database;
        }

        public T Get<T>()
        {
            var factory = _factories[typeof(T)];
            return (T) factory();
        }
    }
}