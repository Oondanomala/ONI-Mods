using TUNING;
using UnityEngine;

namespace AutomatedCanistersReloaded
{
	public class SolidToGasConverterConfig : IBuildingConfig
	{
		public override BuildingDef CreateBuildingDef()
		{
			BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef("SolidToGasConverter", 1, 2, "solidtogas_kanim", 30, 20f, BUILDINGS.CONSTRUCTION_MASS_KG.TIER2, MATERIALS.REFINED_METALS, 1600f, BuildLocationRule.Anywhere, BUILDINGS.DECOR.PENALTY.TIER0, NOISE_POLLUTION.NOISY.TIER0);
			buildingDef.InputConduitType = ConduitType.Solid;
			buildingDef.OutputConduitType = ConduitType.Gas;
			buildingDef.UtilityInputOffset = CellOffset.none;
			buildingDef.UtilityOutputOffset = new CellOffset(0, 1);
			buildingDef.PowerInputOffset = CellOffset.none;
			buildingDef.EnergyConsumptionWhenActive = 15f;
			buildingDef.ViewMode = OverlayModes.SolidConveyor.ID;
			buildingDef.PermittedRotations = PermittedRotations.R360;
			buildingDef.RequiresPowerInput = true;
			buildingDef.Floodable = false;
			buildingDef.Overheatable = false;
			return buildingDef;
		}

		public override void ConfigureBuildingTemplate(GameObject go, Tag prefabTag)
		{
			go.AddOrGet<DropAllWorkable>();
			go.AddOrGet<SolidConduitConsumer>();
			ConduitDispenser conduitDispenser = go.AddOrGet<ConduitDispenser>();
			conduitDispenser.conduitType = ConduitType.Gas;
			conduitDispenser.elementFilter = null;
			Storage storage = BuildingTemplates.CreateDefaultStorage(go);
			storage.capacityKg = 25f;
		}

		public override void DoPostConfigureUnderConstruction(GameObject go)
		{
			go.GetComponent<Constructable>().requiredSkillPerk = Db.Get().SkillPerks.ConveyorBuild.Id;
		}

		public override void DoPostConfigureComplete(GameObject go)
		{
			go.AddOrGet<SolidToGasConverter>();
		}
	}
}
