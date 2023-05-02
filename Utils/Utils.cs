using System.Threading.Tasks;

namespace BookShop.Utils
{
	public static class Utils
	{
		public static async Task Wait(int milliseconds)
		{
			await Task.Delay(milliseconds);
		}
	}
}
