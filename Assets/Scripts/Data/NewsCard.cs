using System.Collections.Generic;
using UnityEngine;

namespace EpidemicJamm.Data
{
	[CreateAssetMenu(fileName = "NewsCard", menuName = "EpidemicJamm/News Card")]
	public class NewsCard : ScriptableObject
	{
		[Header("Identity")]
		public string id;
		public List<string> tags = new List<string>();

		[Header("Texts")]
		[TextArea] public string textPanic;
		[TextArea] public string textNeutral;
		[TextArea] public string textCalm;

		[Header("Effects")]
		public EffectSet effectsPanic = new EffectSet();
		public EffectSet effectsNeutral = new EffectSet();
		public EffectSet effectsCalm = new EffectSet();

		[Header("Unlocks")]
		public List<string> unlocks = new List<string>();
	}
}
