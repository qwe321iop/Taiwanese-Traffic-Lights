// source code from
// https://github.com/gansaku/CSLMapView
// by Gansaku

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaiwaneseTrafficLights {

    internal class Globalization {
        internal const string DEFAULT_LANGUAGE = "en";
        private readonly string[] supportedLanguages = new[] { "en","zh",};
        private Dictionary<string, Dictionary<StringKeys, string>> strings;


        internal enum StringKeys {
			HeaderText,
			type1,
                        type2,
			type3_l,
                        type3_s,
                        type_tp,
                        type_ks,
			GlobalText,
			TinyRoadsText,
			SmallRoadsText,
			SmallHeavyRoadsText,
			MediumRoadsText,
			LargeRoadsText,
			WideRoadsText,
			HighwaysText,
                        AvenueLargeWithGrassText,
                        PedestrianRoadsText,
			/*BusText,*/
			MonorailText,
			GrassText,
			TreesText,

			OptionStyleText,
			OptionEnableText,
			OptionPedRoadsText,
			OptionPromenadeText
		}

        internal string Language { get; set; } = DEFAULT_LANGUAGE;

        internal Globalization() {
            strings = new Dictionary<string, Dictionary<StringKeys, string>>();

            strings.Add( "en", new Dictionary<StringKeys, string>() );
            initen( strings["en"]);
            strings.Add( "zh", new Dictionary<StringKeys, string>() );
            initzh( strings["zh"]);
        }
        private void initzh( Dictionary<StringKeys, string> dic ) {
			dic.Add(StringKeys.HeaderText, "臺灣風格紅綠燈(號誌)，所有更改將在下次載入地圖時生效");
			dic.Add(StringKeys.OptionStyleText, "風格");
			dic.Add(StringKeys.type2, "郊區型");
			dic.Add(StringKeys.type3_l, "市區型(長臂)");
        	        dic.Add(StringKeys.type3_s, "市區型(短臂)");
       		        dic.Add(StringKeys.type_tp, "台北共桿號誌");
			dic.Add(StringKeys.type_ks, "高雄共桿號誌");
     	   	        dic.Add(StringKeys.GlobalText, "全球（適用於新的道路）");
			dic.Add(StringKeys.TinyRoadsText, "單線道路");
			dic.Add(StringKeys.SmallRoadsText, "小型道路");
			dic.Add(StringKeys.SmallHeavyRoadsText, "承載量較大之小型道路");
			dic.Add(StringKeys.MediumRoadsText, "中型道路");
         	        dic.Add(StringKeys.AvenueLargeWithGrassText, "帶草坪的大街(無公車道)");
			dic.Add(StringKeys.LargeRoadsText, "大型道路");
			dic.Add(StringKeys.WideRoadsText, "Wide Roads");
			dic.Add(StringKeys.HighwaysText, "高速公路");
			dic.Add(StringKeys.PedestrianRoadsText, "人行道路(非道路兩旁之人行道)");
			dic.Add(StringKeys.OptionEnableText, "啟用");
			dic.Add(StringKeys.OptionPedRoadsText, "隱藏人行道路的號誌");
			dic.Add(StringKeys.OptionPromenadeText, "隱藏帶自行車道的人行道號誌");
			/*dic.Add(StringKeys.BusText, "[BETA]公車道(僅適用於'專用道')");*/
			dic.Add(StringKeys.MonorailText, "[BETA]帶有輕軌的道路");
			dic.Add(StringKeys.GrassText, "[BETA]有綠化帶的道路(有分隔島的中型道路除外)");
			dic.Add(StringKeys.TreesText, "[BETA]有行道樹的道路(有分隔島的中型道路除外)");
		}


        private void initen( Dictionary<StringKeys, string> dic ) {
            dic.Add( StringKeys.HeaderText, "Taiwanese Traffic Lights Options - Changes will apply after reload savedata.");
	    dic.Add( StringKeys.OptionStyleText, "Style");
	    dic.Add( StringKeys.type2, "Suburbs Style");
	    dic.Add( StringKeys.type3_l, "Downtown Style(Long)");
            dic.Add( StringKeys.type3_s, "Downtown Style(Short)");
            dic.Add( StringKeys.type_tp, "Taipei Special Style");
            dic.Add(StringKeys.type2_awl, "(Avenue Large With Grass ONLY)Suburbs Style");
            dic.Add(StringKeys.type3_l_awl, "(Avenue Large With Grass ONLY)Downtown Style(Long)");
            dic.Add(StringKeys.type3_s_awl, "(Avenue Large With Grass ONLY)Downtown Style(Short)");
            dic.Add(StringKeys.type_tp_awl, "(Avenue Large With Grass ONLY)Taipei Special Style");
            dic.Add( StringKeys.GlobalText, "Global(It applies to new roads)");
	    dic.Add( StringKeys.TinyRoadsText, "Tiny Roads");
	    dic.Add( StringKeys.SmallRoadsText, "Small Roads");
	    dic.Add( StringKeys.SmallHeavyRoadsText, "Small Heavy Roads");
            dic.Add( StringKeys.MediumRoadsText, "Medium Roads");
            dic.Add(StringKeys.AvenueLargeWithGrassText, "Avenue Large With Grass (No Buslines)");
            dic.Add( StringKeys.LargeRoadsText, "Large Roads");
	    dic.Add( StringKeys.WideRoadsText, "Wide Roads");
	    dic.Add( StringKeys.HighwaysText, "Highways");
	    dic.Add( StringKeys.PedestrianRoadsText, "Pedestrian Roads");
	    dic.Add( StringKeys.OptionEnableText, "Enable");
	    dic.Add( StringKeys.OptionPedRoadsText, "Hide signals on Pedestrian Roads");
	    dic.Add( StringKeys.OptionPromenadeText, "Hide signals on Promenade");
	  /*dic.Add( StringKeys.BusText, "[Beta]Road with Bus Lanes(except some roads)");*/
	    dic.Add( StringKeys.MonorailText, "[Beta]Road with Monorail Track");
            dic.Add( StringKeys.GrassText, "[Beta]Road with Grass(except for Medium Roads with median)");
	    dic.Add( StringKeys.TreesText, "[Beta]Road with Trees(except for Medium Roads with median)");
		}

        internal string GetString(StringKeys key)
        {
            return GetString(Language, key);
        }

        internal string GetString(string lang, StringKeys key)
        {
            if (!supportedLanguages.Contains(lang))
            {
                lang = DEFAULT_LANGUAGE;
            }
            Dictionary<StringKeys, string> dic = strings[lang];
            if (dic.ContainsKey(key))
            {
                return dic[key];
            }
            else
            {
                if (lang == DEFAULT_LANGUAGE)
                {
                    return null;
                }
                return GetString(DEFAULT_LANGUAGE, key);
            }
        }
    }
}
