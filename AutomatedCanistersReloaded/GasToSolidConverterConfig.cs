using TUNING;
using UnityEngine;

namespace AutomatedCanistersReloaded
{
	public class GasToSolidConverterConfig : IBuildingConfig
	{
		public override BuildingDef CreateBuildingDef()
		{
			BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef("GasToSolidConverter", 1, 2, "gastosolid_kanim", 30, 20f, BUILDINGS.CONSTRUCTION_MASS_KG.TIER2, MATERIALS.REFINED_METALS, 1600f, BuildLocationRule.Anywhere, BUILDINGS.DECOR.PENALTY.TIER0, NOISE_POLLUTION.NOISY.TIER0);
			buildingDef.InputConduitType = ConduitType.Gas;
			buildingDef.OutputConduitType = ConduitType.Solid;
			buildingDef.UtilityInputOffset = CellOffset.none;
			buildingDef.UtilityOutputOffset = new CellOffset(0, 1);
			buildingDef.PowerInputOffset = CellOffset.none;
			buildingDef.EnergyConsumptionWhenActive = 15f;
			buildingDef.ViewMode = OverlayModes.GasConduits.ID;
			buildingDef.PermittedRotations = PermittedRotations.R360;
			buildingDef.RequiresPowerInput = true;
			buildingDef.Floodable = false;
			buildingDef.Overheatable = false;
			return buildingDef;
		}

		public override void ConfigureBuildingTemplate(GameObject go, Tag prefabTag)
		{
			go.AddOrGet<SolidConduitDispenser>();
			ConduitConsumer conduitConsumer = go.AddOrGet<ConduitConsumer>();
			conduitConsumer.conduitType = ConduitType.Gas;
			conduitConsumer.forceAlwaysSatisfied = true;
			Storage storage = BuildingTemplates.CreateDefaultStorage(go);
			storage.capacityKg = Options.Instance.GasToSolidStorage;
		}

		public override void DoPostConfigureUnderConstruction(GameObject go)
		{
			go.GetComponent<Constructable>().requiredSkillPerk = Db.Get().SkillPerks.ConveyorBuild.Id;
		}

		public override void DoPostConfigureComplete(GameObject go)
		{
			go.AddOrGet<FluidToSolidConverter>();
		}
	}
}
