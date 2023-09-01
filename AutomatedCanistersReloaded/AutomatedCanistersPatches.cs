using HarmonyLib;
using KMod;
using System.Linq;
using TUNING;
using UnityEngine;

namespace AutomatedCanistersReloaded
{
	public class AutomatedCanistersReloadedPatches : UserMod2
	{
		public override void OnLoad(Harmony harmony)
		{
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
				LocString.CreateLocStringKeys(typeof(STRINGS.BUILDINGS));
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
	}
}
