using System.Collections.Generic;
using UnityEngine;
using EpidemicJamm.Data;

namespace EpidemicJamm.Core
{
	public class GameState : MonoBehaviour
	{
		[Header("Config")]
		[Min(1)] public int totalDays = 10;

		[Header("State (Runtime)")]
		public int day = 1;
		[Range(0, 100)] public int trust = 50;
		[Range(0, 100)] public int infection = 20;
		[Range(0, 100)] public int healthcare = 50;
		[Range(0, 100)] public int economy = 60;
		[Range(0, 100)] public int panic = 0;

		public HashSet<string> unlocked = new HashSet<string>();

		public delegate void StateChanged(GameStateSnapshot snapshot);
		public event StateChanged OnStateChanged;

		public void ApplyEffect(EffectSet effect)
		{
			var snapshot = Capture();
			effect.ApplyTo(snapshot);
			Commit(snapshot);
		}

		public void EndDay()
		{
			var snapshot = Capture();
			// Базовый дрейф дня: рост инфекции
			snapshot.day += 1;
			snapshot.infection += 3;

			Commit(snapshot);
		}

		public bool IsGameOver(out string reason)
		{
			if (trust <= 0) { reason = "Провал: доверие упало до 0"; return true; }
			if (economy <= 0) { reason = "Провал: экономика коллапсировала"; return true; }
			if (healthcare <= 0) { reason = "Провал: медицина не справилась"; return true; }
			if (infection >= 90) { reason = "Провал: инфекция вышла из-под контроля"; return true; }
			if (day > totalDays) { reason = "Победа: город продержался до конца срока"; return true; }
			reason = null;
			return false;
		}

		public GameStateSnapshot Capture()
		{
			return new GameStateSnapshot
			{
				day = day,
				trust = trust,
				infection = infection,
				healthcare = healthcare,
				economy = economy,
				panic = panic
			};
		}

		private void Commit(GameStateSnapshot snapshot)
		{
			day = Mathf.Max(1, snapshot.day);
			trust = Mathf.Clamp(snapshot.trust, 0, 100);
			infection = Mathf.Clamp(snapshot.infection, 0, 100);
			healthcare = Mathf.Clamp(snapshot.healthcare, 0, 100);
			economy = Mathf.Clamp(snapshot.economy, 0, 100);
			panic = Mathf.Clamp(snapshot.panic, 0, 100);

			OnStateChanged?.Invoke(Capture());
		}
	}
}
