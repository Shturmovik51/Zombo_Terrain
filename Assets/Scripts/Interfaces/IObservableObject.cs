using UnityEngine.UI;

namespace ZomboTerrain
{
	public interface IObservableObject
	{
		public Image RadarIcon { get; set; }
		public void AddObjectToRadar();
		public void RemoveObjectFromRadar();
	}
}