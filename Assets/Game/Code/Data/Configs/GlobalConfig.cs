﻿using UnityEngine;

namespace Game.Code.Data.Configs
{
	[CreateAssetMenu(fileName = "Global Config", menuName = AssetMenus.Configs + "Global Config", order = -1)]
	public class GlobalConfig : ScriptableObject
	{
		public GameConfig GameConfig;
	}
}