using UnityEngine;
using ColossalFramework;
using ICities;
using System;

namespace TaiwaneseTrafficLights
{
	public class Mod : IUserMod
    {
        public string Name => "Taiwanese Traffic Lights";
        public string Description => "Replaces traffic lights with more Taiwanese-looking versions";

        public void OnSettingsUI(UIHelperBase helper)
        {
            var config = Configuration<TaiwaneseTrafficLightsConfiguration>.Load();

            Globalization globalText = new Globalization();
            SavedString currentLang = new SavedString("localeID", "gameSettings");

            string displayLang;
            if (currentLang.value.Contains("zh")) { displayLang = "zh"; }
            else { displayLang = currentLang.value; }

            string style = globalText.GetString(displayLang, Globalization.StringKeys.OptionStyleText);
            string enable = globalText.GetString(displayLang, Globalization.StringKeys.OptionEnableText);

            string[] styles = new string[] {
                globalText.GetString(displayLang, Globalization.StringKeys.type2),
                globalText.GetString(displayLang, Globalization.StringKeys.type3_l),
                globalText.GetString(displayLang, Globalization.StringKeys.type3_s),
                globalText.GetString(displayLang, Globalization.StringKeys.type_tp),
                globalText.GetString(displayLang, Globalization.StringKeys.type2_awl),
                globalText.GetString(displayLang, Globalization.StringKeys.type3_l_awl),
                globalText.GetString(displayLang, Globalization.StringKeys.type3_s_awl),
                globalText.GetString(displayLang, Globalization.StringKeys.type_tp_awl),
            };

            UIHelperBase GroupHeader = helper.AddGroup(globalText.GetString(displayLang, Globalization.StringKeys.HeaderText));

            UIHelperBase GroupGlobal = helper.AddGroup(globalText.GetString(displayLang, Globalization.StringKeys.GlobalText));
            GroupGlobal.AddDropdown(style, styles, config.Global, Global);

            UIHelperBase GroupTinyRoads = helper.AddGroup(globalText.GetString(displayLang, Globalization.StringKeys.TinyRoadsText));
            GroupTinyRoads.AddDropdown(style, styles, config.TinyRoads, TinyRoads);

            UIHelperBase GroupSmallRoads = helper.AddGroup(globalText.GetString(displayLang, Globalization.StringKeys.SmallRoadsText));
            GroupSmallRoads.AddDropdown(style, styles, config.SmallRoads, SmallRoads);

            UIHelperBase GroupSmallHeavyRoads = helper.AddGroup(globalText.GetString(displayLang, Globalization.StringKeys.SmallHeavyRoadsText));
            GroupSmallHeavyRoads.AddDropdown(style, styles, config.SmallHeavyRoads, SmallHeavyRoads);

            UIHelperBase GroupMediumRoads = helper.AddGroup(globalText.GetString(displayLang, Globalization.StringKeys.MediumRoadsText));
            GroupMediumRoads.AddDropdown(style, styles, config.MediumRoads, MediumRoads);

            UIHelperBase GroupAvenueLargeWithGrass = helper.AddGroup(globalText.GetString(displayLang, Globalization.StringKeys.AvenueLargeWithGrassText));
            GroupAvenueLargeWithGrass.AddDropdown(style, styles, config.AvenueLargeWithGrass, AvenueLargeWithGrass);

            UIHelperBase GroupLargeRoads = helper.AddGroup(globalText.GetString(displayLang, Globalization.StringKeys.LargeRoadsText));
            GroupLargeRoads.AddDropdown(style, styles, config.LargeRoads, LargeRoads);

            UIHelperBase GroupWideRoads = helper.AddGroup(globalText.GetString(displayLang, Globalization.StringKeys.WideRoadsText));
            GroupWideRoads.AddDropdown(style, styles, config.WideRoads, WideRoads);

            UIHelperBase GroupHighways = helper.AddGroup(globalText.GetString(displayLang, Globalization.StringKeys.HighwaysText));
            GroupHighways.AddDropdown(style, styles, config.Highways, Highways);

            UIHelperBase GroupPedestrianRoads = helper.AddGroup(globalText.GetString(displayLang, Globalization.StringKeys.PedestrianRoadsText));
            GroupPedestrianRoads.AddDropdown(style, styles, config.PedestrianRoads, PedestrianRoads);
            GroupPedestrianRoads.AddCheckbox(globalText.GetString(displayLang, Globalization.StringKeys.OptionPedRoadsText), config.HidePedRoadsSignal, IsPedRoadsSignal);
            GroupPedestrianRoads.AddCheckbox(globalText.GetString(displayLang, Globalization.StringKeys.OptionPromenadeText), config.HidePromenadeSignal, IsPromenadeSignal);

			/*UIHelperBase GroupBus = helper.AddGroup("[Beta]Road with Bus Lanes(except some roads)");
			GroupBus.AddDropdown("Style", styles, config.Bus, Bus);
			GroupBus.AddCheckbox("Enable", config.EnableBus, IsBusOption);*/

             UIHelperBase GroupMonorail = helper.AddGroup(globalText.GetString(displayLang, Globalization.StringKeys.MonorailText));
            GroupMonorail.AddDropdown(style, styles, config.Monorail, Monorail);
            GroupMonorail.AddCheckbox(enable, config.EnableMonorail, IsMonorailOption);

            UIHelperBase GroupGrass = helper.AddGroup(globalText.GetString(displayLang, Globalization.StringKeys.GrassText));
            GroupGrass.AddDropdown(style, styles, config.Grass, Grass);
            GroupGrass.AddCheckbox(enable, config.EnableGrass, IsGrassOption);

            UIHelperBase GroupTrees = helper.AddGroup(globalText.GetString(displayLang, Globalization.StringKeys.TreesText));
            GroupTrees.AddDropdown(style, styles, config.Trees, Trees);
            GroupTrees.AddCheckbox(enable, config.EnableTrees, IsTreesOption);
        }


        private void IsPedRoadsSignal(bool b)
		{
			var config = Configuration<TaiwaneseTrafficLightsConfiguration>.Load();

			config.HidePedRoadsSignal = b;
			Configuration<TaiwaneseTrafficLightsConfiguration>.Save();
		}

		private void IsPromenadeSignal(bool b)
		{
			var config = Configuration<TaiwaneseTrafficLightsConfiguration>.Load();

			config.HidePromenadeSignal = b;
			Configuration<TaiwaneseTrafficLightsConfiguration>.Save();
		}

		/*private void IsBusOption(bool b)
		{
			var config = Configuration<TaiwaneseTrafficLightsConfiguration>.Load();

			config.EnableBus = b;
			Configuration<TaiwaneseTrafficLightsConfiguration>.Save();
		}*/

		private void IsMonorailOption(bool b)
		{
			var config = Configuration<TaiwaneseTrafficLightsConfiguration>.Load();

			config.EnableMonorail = b;
			Configuration<TaiwaneseTrafficLightsConfiguration>.Save();
		}

		private void IsGrassOption(bool b)
		{
			var config = Configuration<TaiwaneseTrafficLightsConfiguration>.Load();

			config.EnableGrass = b;
			Configuration<TaiwaneseTrafficLightsConfiguration>.Save();
		}

		private void IsTreesOption(bool b)
		{
			var config = Configuration<TaiwaneseTrafficLightsConfiguration>.Load();

			config.EnableTrees = b;
			Configuration<TaiwaneseTrafficLightsConfiguration>.Save();
		}

		private void Global(int i)
		{
			var config = Configuration<TaiwaneseTrafficLightsConfiguration>.Load();

			config.Global = i;
			Configuration<TaiwaneseTrafficLightsConfiguration>.Save();
		}

		private void TinyRoads(int i)
		{
			var config = Configuration<TaiwaneseTrafficLightsConfiguration>.Load();

			config.TinyRoads = i;
			Configuration<TaiwaneseTrafficLightsConfiguration>.Save();
		}

		private void PedestrianRoads(int i)
		{
			var config = Configuration<TaiwaneseTrafficLightsConfiguration>.Load();

			config.PedestrianRoads = i;
			Configuration<TaiwaneseTrafficLightsConfiguration>.Save();
		}

		private void SmallRoads(int i)
		{
			var config = Configuration<TaiwaneseTrafficLightsConfiguration>.Load();

			config.SmallRoads = i;
			Configuration<TaiwaneseTrafficLightsConfiguration>.Save();
		}

		private void SmallHeavyRoads(int i)
		{
			var config = Configuration<TaiwaneseTrafficLightsConfiguration>.Load();

			config.SmallHeavyRoads = i;
			Configuration<TaiwaneseTrafficLightsConfiguration>.Save();
		}

		private void MediumRoads(int i)
		{
			var config = Configuration<TaiwaneseTrafficLightsConfiguration>.Load();

			config.MediumRoads = i;
			Configuration<TaiwaneseTrafficLightsConfiguration>.Save();
		}
        private void AvenueLargeWithGrass(int i)
        {
            var config = Configuration<TaiwaneseTrafficLightsConfiguration>.Load();

            config.AvenueLargeWithGrass = i;
            Configuration<TaiwaneseTrafficLightsConfiguration>.Save();
        }


        private void LargeRoads(int i)
		{
			var config = Configuration<TaiwaneseTrafficLightsConfiguration>.Load();

			config.LargeRoads = i;
			Configuration<TaiwaneseTrafficLightsConfiguration>.Save();
		}

		private void WideRoads(int i)
		{
			var config = Configuration<TaiwaneseTrafficLightsConfiguration>.Load();

			config.WideRoads = i;
			Configuration<TaiwaneseTrafficLightsConfiguration>.Save();
		}

		private void Highways(int i)
		{
			var config = Configuration<TaiwaneseTrafficLightsConfiguration>.Load();

			config.Highways = i;
			Configuration<TaiwaneseTrafficLightsConfiguration>.Save();
		}

		/*private void Bus(int i)
		{
			var config = Configuration<TaiwaneseTrafficLightsConfiguration>.Load();

			config.Bus = i;
			Configuration<TaiwaneseTrafficLightsConfiguration>.Save();
		}*/

		private void Monorail(int i)
		{
			var config = Configuration<TaiwaneseTrafficLightsConfiguration>.Load();

			config.Monorail = i;
			Configuration<TaiwaneseTrafficLightsConfiguration>.Save();
		}

		private void Grass(int i)
		{
			var config = Configuration<TaiwaneseTrafficLightsConfiguration>.Load();

			config.Grass = i;
			Configuration<TaiwaneseTrafficLightsConfiguration>.Save();
		}

		private void Trees(int i)
		{
			var config = Configuration<TaiwaneseTrafficLightsConfiguration>.Load();

			config.Trees = i;
			Configuration<TaiwaneseTrafficLightsConfiguration>.Save();
		}
	}

	[ConfigurationPath("TaiwaneseTrafficLights.xml")]
	public class TaiwaneseTrafficLightsConfiguration
	{
		public bool HidePedRoadsSignal { get; set; } = true;
		public bool HidePromenadeSignal { get; set; } = false;
		public bool EnableBus { get; set; } = false;
		public bool EnableMonorail { get; set; } = true;
		public bool EnableGrass { get; set; } = true;
		public bool EnableTrees { get; set; } = true;

		public int Global { get; set; } = 0;
		public int TinyRoads { get; set; } = 0;
		public int PedestrianRoads { get; set; } = 0;
		public int SmallRoads { get; set; } = 0;
		public int SmallHeavyRoads { get; set; } = 0;
		public int MediumRoads { get; set; } = 0;
        public int AvenueLargeWithGrass { get; set; } = 0;
        public int LargeRoads { get; set; } = 0;
		public int WideRoads { get; set; } = 0;
		public int Highways { get; set; } = 0;
		/*public int Bus { get; set; } = 0;*/
		public int Monorail { get; set; } = 0;
		public int Grass { get; set; } = 0;
		public int Trees { get; set; } = 0;
	}
}
