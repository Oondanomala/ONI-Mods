using HarmonyLib;
using KMod;
using PeterHan.PLib.Core;
using PeterHan.PLib.Options;

namespace InstantStarmapAnalysis
{
	public class InstantStarmapAnalysisPatches : UserMod2
	{
		public override void OnLoad(Harmony harmony)
		{
			PUtil.InitLibrary(false);
			new POptions().RegisterOptions(this, typeof(Options));
			base.OnLoad(harmony);
		}
		// TODO: Support the Spaced Out! DLC?

		[HarmonyPatch(typeof(StarmapScreen), "RefreshAnalyzeButton")]
		public static class MakeStarmapAnalysisInstant
		{
			public static void Postfix(StarmapScreen __instance, MultiToggle ___analyzeButton, SpaceDestination ___selectedDestination)
			{
				if (___selectedDestination != null && ___selectedDestination.AnalysisState() != SpacecraftManager.DestinationAnalysisState.Complete && ___selectedDestination.id != SpacecraftManager.instance.GetStarmapAnalysisDestinationID())
				{
					___analyzeButton.onClick = delegate
					{
						if (DebugHandler.InstantBuildMode || Game.Instance.SandboxModeActive || !Options.Instance.OnlyInSandbox)
						{
							SpacecraftManager.instance.SetStarmapAnalysisDestinationID(___selectedDestination.id);
							SpacecraftManager.instance.EarnDestinationAnalysisPoints(___selectedDestination.id, 99999f);
							AccessTools.DeclaredMethod("StarmapScreen:ShowDestinationPanel").Invoke(__instance, null);
						}
						else
						{
							SpacecraftManager.instance.SetStarmapAnalysisDestinationID(___selectedDestination.id);
						}
					};
				}
			}
		}
	}
}
