using STRINGS;

namespace AutomatedCanistersReloaded
{
	public static class STRINGS
	{
		public static class BUILDINGS
		{
			public static class PREFABS
			{
				public static class SOLIDTOGASCONVERTER
				{
					public static LocString NAME = UI.FormatAsLink("Conveyor Gas Unloader", nameof(SOLIDTOGASCONVERTER));
					public static LocString DESC = "Can take any solid as input, so be careful not to send anything unwanted in it.";
					public static LocString EFFECT = "Unloads " + UI.FormatAsLink("Gas", "ELEMENTS_GAS") + " canisters from a " + UI.FormatAsLink("Conveyor Rail", "SOLIDCONDUIT") + " into a " + UI.FormatAsLink("Gas Pipe", "GASPIPING");
				}
				public static class SOLIDTOLIQUIDCONVERTER
				{
					public static LocString NAME = UI.FormatAsLink("Conveyor Liquid Unloader", nameof(SOLIDTOLIQUIDCONVERTER));
					public static LocString DESC = "Can take any solid as input, so be careful not to send anything unwanted in it.";
					public static LocString EFFECT = "Unloads " + UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID") + " bottles from a " + UI.FormatAsLink("Conveyor Rail", "SOLIDCONDUIT") + " into a " + UI.FormatAsLink("Liquid Pipe", "LIQUIDPIPING");
				}
				public static class GASTOSOLIDCONVERTER
				{
					public static LocString NAME = UI.FormatAsLink("Conveyor Gas Loader", nameof(GASTOSOLIDCONVERTER));
					public static LocString DESC = "Useful for those annoying to automate buildings like the " + UI.FormatAsLink("Super Computer", "ADVANCEDRESEARCHCENTER");
					public static LocString EFFECT = "Loads " + UI.FormatAsLink("Gases", "ELEMENTS_GAS") + " from a " + UI.FormatAsLink("Gas Pipe", "GASPIPING") + " into a " + UI.FormatAsLink("Conveyor Rail", "SOLIDCONDUIT");
				}
				public static class LIQUIDTOSOLIDCONVERTER
				{
					public static LocString NAME = UI.FormatAsLink("Conveyor Liquid Loader", nameof(LIQUIDTOSOLIDCONVERTER));
					public static LocString DESC = "Useful for those annoying to automate buildings like the " + UI.FormatAsLink("Molecular Forge", "SUPERMATERIALREFINERY");
					public static LocString EFFECT = "Loads " + UI.FormatAsLink("Liquids", "ELEMENTS_LIQUID") + " from a " + UI.FormatAsLink("Liquid Pipe", "LIQUIDPIPING") + " into a " + UI.FormatAsLink("Conveyor Rail", "SOLIDCONDUIT");
				}
			}
		}
	}
}
