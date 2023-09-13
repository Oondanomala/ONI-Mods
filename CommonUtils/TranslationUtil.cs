using System;
using System.IO;
using System.Reflection;

namespace CommonUtils
{
	public class TranslationUtil
	{
		public static void Translate(Type root)
		{
			Localization.RegisterForTranslation(root);
			LoadStrings();
			LocString.CreateLocStringKeys(root, null);
			//Localization.GenerateStringsTemplate(root, Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "translations"));
		}

		private static void LoadStrings()
		{
			string locale = Localization.GetLocale()?.Code;
			if (locale.IsNullOrWhiteSpace())
			{
				locale = Localization.GetCurrentLanguageCode();
				if (locale.IsNullOrWhiteSpace()) { return; }
			}

			string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "translations");
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}

			string filePath = Path.Combine(path, locale + ".po");
			if (File.Exists(filePath))
			{
				Localization.OverloadStrings(Localization.LoadStringsFile(filePath, false));
			}
		}
	}
}
