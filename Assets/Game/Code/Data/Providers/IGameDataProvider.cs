﻿using Game.Code.Data.Configs;

namespace Game.Code.Data.Providers
{
	public interface IGameDataProvider
	{
		GameConfig GameConfig { get; }
	}
}