﻿using Endomondo.DataAccess;
using Endomondo.ViewModels;
using Endomondo.Views;
using Microsoft.EntityFrameworkCore;
using Prism;
using Prism.Ioc;
using Prism.Unity;

namespace Endomondo
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer) : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            using (var dataContext = new DataContext())
            {
                await dataContext.Database.MigrateAsync();
            }

            await NavigationService.NavigateAsync("HomePage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<DbContext, DataContext>();
            containerRegistry.Register<IRouteRepository, RouteRepository>();
            containerRegistry.RegisterForNavigation<HomePage, HomePageViewModel>();
        }
    }
}
