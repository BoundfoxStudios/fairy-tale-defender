using System.IO;
using System.Security.Cryptography;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Systems.SaveGameSystem
{
	public static class SaveGameHash
	{
		public static string Create(string saveName, string saveGameFilePath)
		{
			using var sha512 = SHA512.Create();
			using var fileStream = new FileStream(saveGameFilePath, FileMode.Open, FileAccess.Read);
			var fileHash = sha512.ComputeHash(fileStream);

			var hash = new Hash128();

			hash.Append(saveName);
			hash.Append(fileHash);

			return hash.ToString();
		}
	}
}
