using System;

namespace EpidemicJamm.Data
{
	[Serializable]
	public class EffectSet
	{
		public int trust;
		public int infection;
		public int healthcare;
		public int economy;
		public int panic;

		public void ApplyTo(GameStateSnapshot snapshot)
		{
			snapshot.trust += trust;
			snapshot.infection += infection;
			snapshot.healthcare += healthcare;
			snapshot.economy += economy;
			snapshot.panic += panic;
		}
	}

	[Serializable]
	public class GameStateSnapshot
	{
		public int day;
		public int trust;
		public int infection;
		public int healthcare;
		public int economy;
		public int panic;
	}
}
