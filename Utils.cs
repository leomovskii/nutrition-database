internal static class Utils {

	private const double JConstant = 4.184;

	public static string FormatFileSize(long bytes) {
		const long KB = 1024;
		const long MB = 1024 * KB;
		const long GB = 1024 * MB;

		static string Format(double value, string unit) {
			return value.ToString("F2", Program.Culture) + unit;
		}

		if (bytes < KB)
			return $"{bytes} bytes";
		else if (bytes < MB)
			return $"{Format((double) bytes / KB, "KB")} ({bytes} bytes)";
		else if (bytes < GB)
			return $"{Format((double) bytes / MB, "MB")} ({bytes} bytes)";
		else
			return $"{Format((double) bytes / GB, "GB")} ({bytes} bytes)";
	}

	public static bool CompateArg(string str, out string value, string[] variants) {
		foreach (var variant in variants) {
			if (str.StartsWith(variant, StringComparison.OrdinalIgnoreCase)) {
				value = str[variant.Length..];

				if (!variant.EndsWith(':') && value.StartsWith(':'))
					value = value[1..];

				return true;
			}
		}
		value = "";
		return false;
	}

	public static string DoubleToString(double? d) {
		return d.HasValue ? d.Value.ToString("0.##", Program.Culture) : "?";
	}

	public static double ConvertCcalToKj(double ccal) {
		return ccal * JConstant;
	}

	public static double ConvertKjToCcal(double kj) {
		return kj / JConstant;
	}

	public static string DateToStr(DateTime dateTime) {
		return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
	}
}