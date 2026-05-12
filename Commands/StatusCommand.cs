using System.Text;

internal class StatusCommand : ICommand {

	public string Key => "status";
	public string Help => Localization.Localize(HelpCommandArg);
	public string? DetailedHelp => Localization.Localize(DetailedHelpCommandArg);

	public string? Run(string[] args) {
		var sb = new StringBuilder();
		sb.Append(Localization.Localize(CaptionArg));

		string fPath = File.Exists(SQLiteDatabase.FileName) ? Path.GetFullPath(SQLiteDatabase.FileName) : Localization.Localize(NotExistsArg);
		sb.Append($"\n  {Localization.Localize(FileArg, fPath)}");

		if (File.Exists(SQLiteDatabase.FileName)) {
			var fileInfo = new FileInfo(SQLiteDatabase.FileName);
			sb.Append($"\n  {Localization.Localize(SizeArg, Utils.FormatFileSize(fileInfo.Length))}");
			sb.Append($"\n  {Localization.Localize(ModifiedArg, Utils.DateToStr(fileInfo.LastWriteTime))}");
		}

		var stats = Program.Database.GetDatabaseStats();
		sb.Append($"\n  {Localization.Localize(TotalArg, stats.ProductCount)}");

		if (stats.ProductCount > 0 && !string.IsNullOrEmpty(stats.OldestProduct) && !string.IsNullOrEmpty(stats.NewestProduct)) {
			sb.Append($"\n  {Localization.Localize(TotalArg, stats.OldestProduct)}");
			sb.Append($"\n  {Localization.Localize(TotalArg, stats.NewestProduct)}");
		}

		if (stats.IsValid)
			sb.Append($"\n  {Localization.Localize(OkArg)}");
		else
			sb.Append($"\n  {Localization.Localize(ErrorArg, stats.ErrorMessage)}");

		return sb.ToString();
	}

	#region Localization

	private const string HelpCommandArg = "status-help";
	private const string DetailedHelpCommandArg = "status-detailed";

	private const string CaptionArg = "status-caption";
	private const string FileArg = "status-file";
	private const string NotExistsArg = "status-file-not-exists";
	private const string SizeArg = "status-size";
	private const string ModifiedArg = "status-modified";
	private const string TotalArg = "status-total-products";
	private const string OldestArg = "status-oldest";
	private const string NewestArg = "status-newest";
	private const string OkArg = "status-ok";
	private const string ErrorArg = "status-error";

	public static void FillLocalization(Dictionary<string, string> dict) {
		dict.Add(HelpCommandArg, "Get info about plugin & db.");
		dict.Add(DetailedHelpCommandArg, "");

		dict.Add(CaptionArg, "Status Report:");
		dict.Add(FileArg, "Database File: {0}");
		dict.Add(NotExistsArg, "not exists");
		dict.Add(SizeArg, "File Size: {0}");
		dict.Add(ModifiedArg, "Last Modified: {0}");
		dict.Add(TotalArg, "Total Products: {0}");
		dict.Add(OldestArg, "Oldest Product: {0}");
		dict.Add(NewestArg, "Newest Product: {0}");
		dict.Add(OkArg, "Database Status: Ok");
		dict.Add(ErrorArg, "Database Status: Error - {0}");
	}

	#endregion
}