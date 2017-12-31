using UnityEngine;
using ColossalFramework;
using ICities;
using System;
using System.Linq;
using System.Collections.Generic;

namespace TaiwaneseTrafficLights
{
	public class TWTL
	{

		private static readonly TaiwaneseTrafficLightsConfiguration config;
        private static readonly PropInfo Type2Main, Type2Ped, Type3LMain, Type3LPed, TypeTPMain, TypeTPPed, Type3SMain, Type3SPed;
		private enum style { none, type2, type3_s, type3_l, type_tp, type2_awl, type3_s_awl, type3_l_awl, type_tp_awl }

		static TWTL()
		{
            Type2Main  = PrefabCollection<PropInfo>.FindLoaded("1233091415.TWTL_TYPE2_Main_Data");

            if (Type2Main == null)
            {
                Log.Display(Log.mode.error, "Prop Not Found", true);
                return;
            }


            Type2Ped   = PrefabCollection<PropInfo>.FindLoaded("1233091415.TWTL_TYPE2_Ped_Data");

            if (Type2Ped == null)
            {
                Log.Display(Log.mode.error, "Prop Not Found", true);
                return;
            }

            Type3LMain  = PrefabCollection<PropInfo>.FindLoaded("1233091415.TWTL_TYPE3_Main_Data");

            if (Type3LMain == null)
            {
                Log.Display(Log.mode.error, "Prop Not Found", true);
                return;
            }


            Type3LPed   = PrefabCollection<PropInfo>.FindLoaded("1233091415.TWTL_TYPE3_Ped_Data");

            if (Type3LPed == null)
            {
                Log.Display(Log.mode.error, "Prop Not Found", true);
                return;
            }


            Type3SMain = PrefabCollection<PropInfo>.FindLoaded("1233091415.TWTL_TYPE3_Sub_Data");

            if (Type3SMain == null)
            {
                Log.Display(Log.mode.error, "Prop Not Found", true);
                return;
            }

            Type3SPed = PrefabCollection<PropInfo>.FindLoaded("1233091415.TWTL_TYPE3_Ped_Data");

            if (Type3SPed == null)
            {
                Log.Display(Log.mode.error, "Prop Not Found", true);
                return;
            }

            TypeTPMain = PrefabCollection<PropInfo>.FindLoaded("1233091415.TWTL_TP_Main_Data");

            if (TypeTPMain == null)
            {
                Log.Display(Log.mode.error, "Prop Not Found", true);
                return;
            }

            TypeTPPed = PrefabCollection<PropInfo>.FindLoaded("1233091415.TWTL_TP_Ped_Data");

            if (TypeTPPed == null)
            {
                Log.Display(Log.mode.error, "Prop Not Found", true);
                return;
            }

            config = Configuration<TaiwaneseTrafficLightsConfiguration>.Load();
		}


		public static void ReplaceAllTL()
		{
			Log.Display(Log.mode.warning, $"ReplaceAllTL Init");

			NetInfo[] roads = GetRegisteredNetInfos();

			if (roads == null || roads.Length == 0)
			{
				Log.Display(Log.mode.error, "ReplaceAllTL - NetInfo[] is null.");
				return;
			}

			ReplaceTL(roads);
		}


		public static NetInfo[] GetRegisteredNetInfos()
		{
			var allNetinfos = new List<NetInfo>();
			for (uint i = 0; i < PrefabCollection<NetInfo>.PrefabCount(); i++)
			{
				NetInfo info = PrefabCollection<NetInfo>.GetPrefab(i);
				if (info == null) continue;

				allNetinfos.Add(info);
			}
			return allNetinfos.ToArray();
		}

		private static void ReplaceTL(IEnumerable<NetInfo> roads)
		{
			Log.Display(Log.mode.warning, "ReplaceTL Init");

			foreach (var road in roads)
			{
				foreach (var lane in road.m_lanes)
				{
					if (lane?.m_laneProps?.m_props == null)
					{
						continue;
					}

					foreach (var laneProp in lane.m_laneProps.m_props)
					{
						var prop = laneProp.m_finalProp;

						if (prop == null)
						{
							continue;
						}

						style style = ReturnStyleFromRoadname(road.name);

						switch (prop.name)
						{
                            case "Traffic Light 02 Mirror":
                            case "Traffic Light European 02 Mirror":
                                switch(style)
                                {
                                    case style.none:
                                        laneProp.m_finalProp = null;
                                        laneProp.m_prop = null;
                                        break;

                                    case style.type2:
                                        laneProp.m_finalProp = Type2Ped;
                                        laneProp.m_prop = Type2Ped;
                                        break;

                                    case style.type3_l:
                                        laneProp.m_finalProp = Type3LPed;
                                        laneProp.m_prop = Type3LPed;
                                        break;

                                    case style.type3_s:
                                        laneProp.m_finalProp = Type3SPed;
                                        laneProp.m_prop = Type3SPed;
                                        break;

                                    case style.type_tp:
                                        laneProp.m_finalProp = TypeTPPed;
                                        laneProp.m_prop = TypeTPPed;
                                        break;

                                    case style.type2_awl:
                                        laneProp.m_angle = laneProp.m_angle + 180;
                                        laneProp.m_finalProp = Type2Main;
                                        laneProp.m_prop = Type2Main;
                                        break;

                                    case style.type3_l_awl:
                                        laneProp.m_angle = laneProp.m_angle + 180;
                                        laneProp.m_finalProp = Type3LMain;
                                        laneProp.m_prop = Type3LMain;
                                    break;

                                    case style.type3_s_awl:
                                        laneProp.m_angle = laneProp.m_angle + 180;
                                        laneProp.m_finalProp = Type3SMain;
                                        laneProp.m_prop = Type3SMain;
                                        break;

                                    case style.type_tp_awl:
                                        laneProp.m_angle = laneProp.m_angle + 180;
                                        laneProp.m_finalProp = TypeTPMain;
                                        laneProp.m_prop = TypeTPMain;
                                        break;

                                }
                                break;
                                
                            case "Traffic Light 01":
							case "Traffic Light European 01":							
								switch(style)
								{
									case style.none:
										laneProp.m_finalProp = null;
										laneProp.m_prop = null;
										break;

									case style.type2:
										laneProp.m_finalProp = Type2Ped;
										laneProp.m_prop = Type2Ped;
										break;

									case style.type3_l:
										laneProp.m_finalProp = Type3LPed;
										laneProp.m_prop = Type3LPed;
										break;

                                    case style.type3_s:
                                        laneProp.m_finalProp = Type3SPed;
                                        laneProp.m_prop = Type3SPed;
                                        break;

                                    case style.type_tp:
                                        laneProp.m_finalProp = TypeTPPed;
                                        laneProp.m_prop = TypeTPPed;
                                        break;

                                    case style.type2_awl:
                                        laneProp.m_finalProp = null;
                                        laneProp.m_prop = null;
                                        break;

                                    case style.type3_l_awl:
                                        laneProp.m_finalProp = null;
                                        laneProp.m_prop = null;
                                        break;

                                    case style.type3_s_awl:
                                        laneProp.m_finalProp = null;
                                        laneProp.m_prop = null;
                                        break;

                                    case style.type_tp_awl:
                                        laneProp.m_finalProp = null;
                                        laneProp.m_prop = null;
                                        break;

                                }
								break;

							case "Traffic Light 01 Mirror":
							case "Traffic Light European 01 Mirror":
                                switch (style)
                                {
                                    case style.none:
                                        laneProp.m_finalProp = null;
                                        laneProp.m_prop = null;
                                        break;

                                    case style.type2:
                                        laneProp.m_finalProp = Type2Main;
                                        laneProp.m_prop = Type2Main;
                                        break;

                                    case style.type3_l:
                                        laneProp.m_finalProp = Type3LMain;
                                        laneProp.m_prop = Type3LMain;
                                        break;

                                    case style.type3_s:
                                        laneProp.m_finalProp = Type3SMain;
                                        laneProp.m_prop = Type3SMain;
                                        break;

                                    case style.type_tp:
                                        laneProp.m_finalProp = TypeTPMain;
                                        laneProp.m_prop = TypeTPMain;
                                        break;

                                    case style.type2_awl:
                                        laneProp.m_finalProp = null;
                                        laneProp.m_prop = null;
                                        break;

                                    case style.type3_l_awl:
                                        laneProp.m_finalProp = null;
                                        laneProp.m_prop = null;
                                        break;

                                    case style.type3_s_awl:
                                        laneProp.m_finalProp = null;
                                        laneProp.m_prop = null;
                                        break;

                                    case style.type_tp_awl:
                                        laneProp.m_finalProp = null;
                                        laneProp.m_prop = null;
                                        break;
                                }
                                break;

                            case "Traffic Light 02":
							case "Traffic Light European 02":
								switch (style)
								{
									case style.none:
										laneProp.m_finalProp = null;
										laneProp.m_prop = null;
										break;

									case style.type2:
										laneProp.m_finalProp = Type2Main;
										laneProp.m_prop = Type2Main;
										break;

									case style.type3_l:
										laneProp.m_finalProp = Type3LMain;
										laneProp.m_prop = Type3LMain;
										break;

                                    case style.type3_s:
                                        laneProp.m_finalProp = Type3SMain;
                                        laneProp.m_prop = Type3SMain;
                                        break;

                                    case style.type_tp:
                                        laneProp.m_finalProp = TypeTPMain;
                                        laneProp.m_prop = TypeTPMain;
                                        break;


                                    case style.type2_awl:
                                        laneProp.m_finalProp = null;
                                        laneProp.m_prop = null;
                                        break;

                                    case style.type3_l_awl:
                                        laneProp.m_finalProp = null;
                                        laneProp.m_prop = null;
                                        break;

                                    case style.type3_s_awl:
                                        laneProp.m_finalProp = null;
                                        laneProp.m_prop = null;
                                        break;

                                    case style.type_tp_awl:
                                        laneProp.m_finalProp = null;
                                        laneProp.m_prop = null;
                                        break;
                                }
								break;

							case "Traffic Light Pedestrian":
							case "Traffic Light Pedestrian European":
								switch (style)
								{
									case style.none:
										laneProp.m_finalProp = null;
										laneProp.m_prop = null;
										break;

									case style.type2:
										laneProp.m_finalProp = Type2Ped;
										laneProp.m_prop = Type2Ped;
										break;

									case style.type3_l:
										laneProp.m_finalProp = Type3LPed;
										laneProp.m_prop = Type3LPed;
										break;
                                    case style.type3_s:
                                        laneProp.m_finalProp = Type3SPed;
                                        laneProp.m_prop = Type3SPed;
                                        break;

                                    case style.type_tp:
                                        laneProp.m_finalProp = TypeTPPed;
                                        laneProp.m_prop = TypeTPPed;
                                        break;


                                    case style.type2_awl:
                                        laneProp.m_finalProp = Type2Ped;
                                        laneProp.m_prop = Type2Ped;
                                        break;

                                    case style.type3_l_awl:
                                        laneProp.m_finalProp =Type3LPed;
                                        laneProp.m_prop = Type3LPed;
                                        break;

                                    case style.type3_s_awl:
                                        laneProp.m_finalProp = Type3SPed;
                                        laneProp.m_prop = Type3SPed;
                                        break;

                                    case style.type_tp_awl:
                                        laneProp.m_finalProp = TypeTPPed;
                                        laneProp.m_prop = TypeTPPed;
                                        break;
                                }
								break;
						}
					}
				}
			}
		}


		private static style ReturnStyleFromConfig(int i)
		{
			switch (i)
			{
				case 0:
				default:
					return style.type2;
				case 1:
                    return style.type3_l ;
                case 2:
                    return style.type3_s;
                case 3:
                    return style.type_tp;
                case 4:
                    return style.type2_awl;
                case 5:
                    return style.type3_l_awl;
                case 6:
                    return style.type3_s_awl;
                case 7:
                    return style.type_tp_awl;
            }
		}


		private static style ReturnStyleFromRoadname(string name)
		{
			style style = ReturnStyleFromConfig(config.Global);

			/* Bus
			if (name.ToLower().Contains("bus") && config.EnableBus) { return style = ReturnStyleFromConfig(config.Bus); }*/

			// Monorail
			if (name.Contains("Monorail") && config.EnableMonorail) { return style = ReturnStyleFromConfig(config.Monorail); }

            // Grass
            if (name.Contains("Grass") && config.EnableGrass)
            {
                if (name.Contains("Medium Road"))
                    {
                        return style = ReturnStyleFromConfig(config.MediumRoads);
                    }
                if (name.Contains("Avenue Large With Grass"))
                    {
                        return style = ReturnStyleFromConfig(config.AvenueLargeWithGrass);
                    }
                else
                    {
                    return style = ReturnStyleFromConfig(config.Grass);
                    }               
			}

			// Trees
			if (name.Contains("Trees") && config.EnableTrees)
			{
				if (!name.Contains("Medium Road"))
				{
					return style = ReturnStyleFromConfig(config.Trees);
				}
			}

			// Tiny Roads
			if (
				name.Contains("Gravel Road") ||
				name.Contains("PlainStreet2L") ||
				name.Contains("Two-Lane Alley") ||
				name.Contains("Two-Lane Oneway") ||
				name.Contains("One-Lane Oneway With") ||
				name.Contains("One-Lane Oneway")
			) { return style = ReturnStyleFromConfig(config.TinyRoads); }

			// Small Roads
			if (
				name.Contains("Basic Road") ||
				name.Contains("BasicRoadPntMdn") ||
				name.Contains("BasicRoadMdn") ||
				name.Contains("Oneway Road") ||
				name.Contains("Asymmetrical Three Lane Road") ||
				name.Contains("One-Lane Oneway with") ||
				name.Contains("Small Busway") ||
				name.Contains("Harbor Road") ||
				name.Contains("Tram Depot Road") ||
				name.Contains("Small Road")
			) { return style = ReturnStyleFromConfig(config.SmallRoads); }

			// Small Heavy Roads
			if (
				name.Contains("BasicRoadTL") ||
				name.Contains("AsymRoadL1R2") ||
				name.Contains("Oneway3L") ||
				name.Contains("Small Avenue") ||
				name.Contains("AsymRoadL1R3") ||
				name.Contains("Oneway4L") ||
				name.Contains("OneWay3L")
			) { return style = ReturnStyleFromConfig(config.SmallHeavyRoads); }

			// Medium Roads
			if (
				name.Contains("Medium Road") ||
				name.Contains("Medium Avenue") ||
				name.Contains("FourDevidedLaneAvenue") ||
				name.Contains("AsymAvenueL2R4") ||
				name.Contains("AsymAvenueL2R3")
			) { return style = ReturnStyleFromConfig(config.MediumRoads); }

            //Avenue Large With Grass
            if (name.Contains("Avenue Large With Grass")
                )
            {
                return style = ReturnStyleFromConfig(config.AvenueLargeWithGrass);
            }

			// Large Roads
			if (
				name.Contains("Large Road") ||
				name.Contains("Large Oneway") ||
				name.Contains("Eight-Lane Avenue")
			) { return style = ReturnStyleFromConfig(config.LargeRoads); }

			// Wide Roads
			if (
				name.Contains("WideAvenue")
			) { return style = ReturnStyleFromConfig(config.WideRoads); }

			// Highway
			if (
				name.Contains("Highway")
			) { return style = ReturnStyleFromConfig(config.Highways); }

			// Pedestrian Roads
			if (name.Contains("Zonable Pedestrian"))
			{
				if (config.HidePedRoadsSignal) { return style = style.none; }
				else { return style = ReturnStyleFromConfig(config.PedestrianRoads); }
			}

			// Promenade
			if (name.Contains("Zonable Promenade"))
			{
				if (config.HidePromenadeSignal) { return style = style.none; }
				else { return style = ReturnStyleFromConfig(config.PedestrianRoads); }
			}

			return style;
		}
	}
}
