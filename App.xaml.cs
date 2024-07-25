using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BookShop.DataBase;
using System.Windows;

namespace BookShop
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App
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
			_serviceProvider.GetService<MainWindow>().Show();
		}
	}
}