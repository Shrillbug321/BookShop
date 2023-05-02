using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BookShop.DataBase;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace BookShop
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		private ServiceProvider _serviceProvider;
		public App()
		{
			var services = new ServiceCollection();
			services.AddDbContext<BooksContext>(options =>
			{
				options.UseSqlite("Data Source = Books.db");
			});

			services.AddSingleton<MainWindow>();
			_serviceProvider = services.BuildServiceProvider();
		}

		private void OnStartup(object s, StartupEventArgs e)
		{
			var mainWindow = _serviceProvider.GetService<MainWindow>();
			mainWindow.Show();
		}
	}
}
