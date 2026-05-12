using Newtonsoft.Json;
using System.Globalization;

internal static class Localization {

	private readonly static Dictionary<string, string> DefaultLang = [];
	private static Dictionary<string, string>? LoadedLang;
	private readonly static string LocalesFolder = Path.Combine(AppContext.BaseDirectory, "Locales");

	public static void Init() {
		FillDefaultLang();
		TryLoadCurrentLang();
	}

	public static string Localize(string key, params object[] args) {
		if (LoadedLang == null || !LoadedLang.TryGetValue(key, out string? localized))
			DefaultLang.TryGetValue(key, out localized);

		if (string.IsNullOrEmpty(localized))
			return $"Localization error: Key '{key}' not found.";

		return localized.Contains("{0}") && args.Length > 0 ? string.Format(localized, args) : localized;
	}

	private static void FillDefaultLang() {
		DefaultLang.Clear();

		Program.FillLocalization(DefaultLang);
		AddCommand.FillLocalization(DefaultLang);
		FileCommand.FillLocalization(DefaultLang);
		FindCommand.FillLocalization(DefaultLang);
		HelpCommand.FillLocalization(DefaultLang);
		InfoCommand.FillLocalization(DefaultLang);
		RemoveCommand.FillLocalization(DefaultLang);
		StatusCommand.FillLocalization(DefaultLang);
		TodoCommand.FillLocalization(DefaultLang);
		UpdateCommand.FillLocalization(DefaultLang);

		if (!Directory.Exists(LocalesFolder))
			Directory.CreateDirectory(LocalesFolder);

		try {
			string filePath = Path.Combine(LocalesFolder, $"en.lang");
			string json = JsonConvert.SerializeObject(DefaultLang, Formatting.Indented);
			File.WriteAllText(filePath, json);
		
		} catch (Exception e) {
			// Console.WriteLine($"Error saving 'en.lang': {e.Message}");
		}
	}

	private static void TryLoadCurrentLang() {
		var culture = CultureInfo.CurrentCulture;
		string langCode = culture.TwoLetterISOLanguageName;

		string filePath = Path.Combine(LocalesFolder, $"{langCode}.lang");

		if (File.Exists(filePath)) {
			try {
				string json = File.ReadAllText(filePath);
				LoadedLang = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

			} catch (Exception e) {
				//Console.WriteLine($"Error loading '{langCode}': {e.Message}");
			}
		}
	}
}