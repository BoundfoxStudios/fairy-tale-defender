namespace BoundfoxStudios.FairyTaleDefender.Systems.SaveGameSystem
{
	public class SaveGame
	{
		public SaveGameMeta Meta { get; }
		public SaveGameData Data { get; }

		public SaveGame(SaveGameMeta meta, SaveGameData data)
		{
			Meta = meta;
			Data = data;
		}

		public string MetaFilePath => Meta.MetaFilePath;
		public string DataFilePath => Meta.DataFilePath;
	}
}
