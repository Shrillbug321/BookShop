using System.Threading.Tasks;

namespace BookShop.Utils
{
	public static class Utils
	{
		public static Task Wait(int milliseconds)
		{
			return Task.Delay(milliseconds);
		}
	}
}
