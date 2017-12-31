using System;
using UnityEngine;
using ColossalFramework;
using ICities;

namespace TaiwaneseTrafficLights
{
	public class LoadingExtension : LoadingExtensionBase
	{
		public override void OnLevelLoaded(LoadMode mode)
		{
			base.OnLevelLoaded(mode);

			if (mode != LoadMode.LoadGame && mode != LoadMode.NewGame)
			{
				return;
			}
			
			TWTL.ReplaceAllTL();
			//Log.OutputRegisteredNetInfos();
		}
	}
}
 