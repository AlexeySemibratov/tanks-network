namespace Game.Code.Tanks
{
	public interface INetTankUnit
	{
		void CmdShoot();

		void CmdMoveAxisInput(float value);
		void CmdRotateAxisInput(float value);
	}
}