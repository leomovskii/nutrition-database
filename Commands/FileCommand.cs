internal class FileCommand : ICommand {

	public string Key => "file";
	public string Help => Localization.Localize(HelpCommandArg);
	public string? DetailedHelp => Localization.Localize(DetailedHelpCommandArg);

	public string? Run(string[] _) {
		string dbPath = Path.GetFullPath(SQLiteDatabase.FileName);

		if (!File.Exists(dbPath))
			return Localization.Localize(DetailedHelpCommandArg, dbPath);

		try {
			if (OperatingSystem.IsWindows()) {
				System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo {
					FileName = "explorer.exe",
					Arguments = $"/select, \"{dbPath}\"",
					UseShellExecute = true
				});
			} else if (OperatingSystem.IsMacOS()) {
				System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo {
					FileName = "open",
					Arguments = $"-R \"{dbPath}\"",
					UseShellExecute = true
				});
			} else if (OperatingSystem.IsLinux()) {
				System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo {
					FileName = "xdg-open",
					Arguments = Path.GetDirectoryName(dbPath),
					UseShellExecute = true
				});
			}
			return null;

		} catch (Exception e) {
			return e.Message;
		}
	}

	#region Localization

	private const string HelpCommandArg = "file-help";
	private const string DetailedHelpCommandArg = "file-detailed";
	private const string FileNotFoundArg = "file-not-found";

	public static void FillLocalization(Dictionary<string, string> dict) {
		dict.Add(HelpCommandArg, "Open database file location.");
		dict.Add(DetailedHelpCommandArg, "");
		dict.Add(FileNotFoundArg, "Database file not found on path '{dbPath}'.");
	}

	#endregion
}