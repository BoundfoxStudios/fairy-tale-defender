using Cysharp.Threading.Tasks;

namespace BoundfoxStudios.CommunityProject.Infrastructure.FileManager
{
	public interface IFileManager
	{
		UniTask<bool> ExistsAsync(string key);
		UniTask WriteAsync<T>(string key, T serializable);
		UniTask<T> ReadAsync<T>(string key);
	}
}
