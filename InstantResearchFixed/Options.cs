using Newtonsoft.Json;
using PeterHan.PLib.Options;
using System.Collections.Generic;

namespace InstantResearchFixed
{
	[ModInfo("https://github.com/Oondanomala/ONI-Mods")]
	public class Options : SingletonOptions<Options>, IOptions
	{
		[JsonProperty]
		[Option("Only in sandbox", "If the mod should only work in sandbox mode.")]
		public bool OnlyInSandbox { get; set; }

		public Options()
		{
			OnlyInSandbox = true;
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
