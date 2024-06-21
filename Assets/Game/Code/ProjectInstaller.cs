﻿using Game.Code.Data.Configs;
using Game.Code.Data.Providers;
using Game.Code.Network;
using UnityEngine;
using Zenject;

namespace Game.Code
{
	public class ProjectInstaller : MonoInstaller
	{
		[SerializeField] private GlobalConfig _globalConfig;
		
		public override void InstallBindings()
		{
			Container
				.BindInterfacesTo<GameDataProvider>()
				.AsSingle()
				.WithArguments(_globalConfig);

			Container
				.BindInterfacesTo<NetModeProvider>()
				.AsSingle();
		}
	}
}