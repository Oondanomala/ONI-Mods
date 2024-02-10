using Newtonsoft.Json;
using PeterHan.PLib.Options;
using System.Collections.Generic;

namespace AutomatedCanistersReloaded
{
	[RestartRequired]
	[ModInfo("https://github.com/Oondanomala/ONI-Mods")]
	public class Options : SingletonOptions<Options>, IOptions
	{
		[JsonProperty]
		[Option("STRINGS.CONFIG.AUTOMATEDCANISTERSRELOADED.ALWAYSACTIVE.TITLE", "STRINGS.CONFIG.AUTOMATEDCANISTERSRELOADED.ALWAYSACTIVE.TOOLTIP")]
		public bool AlwaysActive { get; set; }

		[Option("STRINGS.CONFIG.AUTOMATEDCANISTERSRELOADED.LIQUIDTOSOLIDSTORAGE.TITLE", "STRINGS.CONFIG.AUTOMATEDCANISTERSRELOADED.LIQUIDTOSOLIDSTORAGE.TOOLTIP", Format = "0 Kg")]
		[Limit(1, 100)]
		[JsonProperty]
		public int LiquidToSolidStorage { get; set; }

		[Option("STRINGS.CONFIG.AUTOMATEDCANISTERSRELOADED.GASTOSOLIDSTORAGE.TITLE", "STRINGS.CONFIG.AUTOMATEDCANISTERSRELOADED.GASTOSOLIDSTORAGE.TOOLTIP", Format = "0 Kg")]
		[Limit(1, 50)]
		[JsonProperty]
		public int GasToSolidStorage { get; set; }

		public Options()
		{
			AlwaysActive = true;
			LiquidToSolidStorage = 50;
			GasToSolidStorage = 5;
		}

		public void OnOptionsChanged()
		{
			Instance = this;
		}

		public IEnumerable<IOptionsEntry> CreateOptions()
		{
			return null;
		}
	}
}
