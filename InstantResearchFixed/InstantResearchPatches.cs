using HarmonyLib;
using KMod;
using PeterHan.PLib.Core;
using PeterHan.PLib.Options;

namespace InstantResearchFixed
{
	public class InstantResearchFixedPatches : UserMod2
	{
		public override void OnLoad(Harmony harmony)
		{
			PUtil.InitLibrary(false);
			new POptions().RegisterOptions(this, typeof(Options));
			base.OnLoad(harmony);
		}

		[HarmonyPatch(typeof(ResearchEntry), "OnResearchClicked")]
		public static class MakeResearchInstant
		{
			public static void Postfix()
			{
				if (!DebugHandler.InstantBuildMode && (Game.Instance.SandboxModeActive || !Options.Instance.OnlyInSandbox))
					Research.Instance.CompleteQueue();
			}
		}
	}
}
