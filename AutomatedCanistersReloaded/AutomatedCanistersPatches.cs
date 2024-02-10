using CommonUtils;
using HarmonyLib;
using KMod;
using PeterHan.PLib.Core;
using PeterHan.PLib.Options;
using System.Linq;
using TUNING;
using UnityEngine;

namespace AutomatedCanistersReloaded
{
	public class AutomatedCanistersReloadedPatches : UserMod2
	{
		public override void OnLoad(Harmony harmony)
		{
			PUtil.InitLibrary(false);
			new POptions().RegisterOptions(this, typeof(Options));
			base.OnLoad(harmony);
			STORAGEFILTERS.SOLID_TRANSFER_ARM_CONVEYABLE = STORAGEFILTERS.SOLID_TRANSFER_ARM_CONVEYABLE.Concat(STORAGEFILTERS.GASES).Concat(STORAGEFILTERS.LIQUIDS).ToArray();
		}

		public static void AddBuildingToTechnology(string techId, string buildingId)
		{
			Tech tech = Db.Get().Techs.Get(techId);
			if (tech != null)
			{
				tech.unlockedItemIDs.Add(buildingId);
			}
			else
			{
				Debug.LogWarning($"Unable to add '{buildingId}' to technology as tech '{techId}' does not exist");
			}
		}

		[HarmonyPatch(typeof(Db), "Initialize")]
		public static class AddBuildingsToPlanScreenAndTechnology
		{
			public static void Postfix()
			{
				ModUtil.AddBuildingToPlanScreen("Conveyance", "SolidToGasConverter", "conveyancestructures");
				ModUtil.AddBuildingToPlanScreen("Conveyance", "SolidToLiquidConverter", "conveyancestructures");
				ModUtil.AddBuildingToPlanScreen("Conveyance", "GasToSolidConverter", "conveyancestructures");
				ModUtil.AddBuildingToPlanScreen("Conveyance", "LiquidToSolidConverter", "conveyancestructures");
				AddBuildingToTechnology("SolidSpace", "SolidToGasConverter");
				AddBuildingToTechnology("SolidSpace", "SolidToLiquidConverter");
				AddBuildingToTechnology("SolidSpace", "GasToSolidConverter");
				AddBuildingToTechnology("SolidSpace", "LiquidToSolidConverter");
			}
		}

		[HarmonyPatch(typeof(Localization), "Initialize")]
		public static class LocalizationSupport
		{
			public static void Postfix()
			{
				TranslationUtil.Translate(typeof(STRINGS));
			}
		}

		[HarmonyPatch(typeof(SolidConduitInboxConfig), "DoPostConfigureComplete")]
		public static class AddFluidsToSolidConduitInboxConfig
		{
			public static void Postfix(GameObject go)
			{
				Storage storage = go.AddOrGet<Storage>();
				storage.storageFilters.AddRange(STORAGEFILTERS.GASES);
				storage.storageFilters.AddRange(STORAGEFILTERS.LIQUIDS);
			}
		}

		[HarmonyPatch(typeof(WarpConduitReceiver.ConduitPort), "SetPortInfo")]
		public static class MakeWarpConduitReceiverOutputFluids
		{
			public static void Postfix(GameObject parent, ConduitPortInfo info)
			{
				if (info.conduitType == ConduitType.Solid)
				{
					SolidConduitDispenser conduitDispenser = parent.AddOrGet<SolidConduitDispenser>();
					conduitDispenser.solidOnly = false;
				}
			}
		}
	}
}
