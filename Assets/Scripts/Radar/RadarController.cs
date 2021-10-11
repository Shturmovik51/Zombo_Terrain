using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ZomboTerrain
{	
	public sealed class RadarController : IUpdatable, IInitialisible, IController
	{
		private GameObject _radarView;
		private RawImage _radarBackGround;
		private Transform _playerPos;
		private readonly float _mapScale = 2;
		private Transform _radarPosition;
		public static List<RadarObject> RadObjects = new List<RadarObject>();
		
		public RadarController(Camera mainCamera, Transform radarPosition)
        {
			_playerPos = mainCamera.transform;
			_radarPosition = radarPosition;
		}

		public void LocalUpdate(float deltaTime)
		{
			if (Time.frameCount % 2 == 0)
			{
				DrawRadarDots();
			}
		}
		
		public void Initialization()
        {
			var _radarViewPref = Resources.Load<GameObject>("RadarView");

			_radarView = Object.Instantiate(_radarViewPref, _radarPosition);

			var texture = Resources.Load<Texture>("RadarBackGround/RadarBG");

			_radarBackGround = new GameObject(name: "RadarBackGround").AddComponent<RawImage>();
			_radarBackGround.transform.parent = _radarView.transform;
			_radarBackGround.transform.position = _radarView.transform.position;
			_radarBackGround.texture = texture;
			_radarBackGround.SetNativeSize();
		}

		public void RegisterRadarObject(GameObject radarObject, Image radarObjectImage)
		{		
			Image image = Object.Instantiate(radarObjectImage);
			RadObjects.Add(new RadarObject { Owner = radarObject, Icon = image });
		}
		
		public void RemoveRadarObject(GameObject radarObject)
		{
			List<RadarObject> newList = new List<RadarObject>();
			foreach (RadarObject radObject in RadObjects)
			{
				if (radObject.Owner == radarObject)
				{
					Object.Destroy(radObject.Icon);
					continue;
				}
				newList.Add(radObject);
			}
			RadObjects.RemoveRange(0, RadObjects.Count);
			RadObjects.AddRange(newList);
		}
		
		private void DrawRadarDots()
		{
			foreach (RadarObject radObject in RadObjects)
			{
				Vector3 radarPos = (radObject.Owner.transform.position -
				                    _playerPos.position);
				float distToObject = Vector3.Distance(_playerPos.position,
					                     radObject.Owner.transform.position) * _mapScale;
				float deltay = Mathf.Atan2(radarPos.x, radarPos.z) * Mathf.Rad2Deg -
				               270 - _playerPos.eulerAngles.y;
				radarPos.x = distToObject* Mathf.Cos(deltay* Mathf.Deg2Rad) * -1;
				radarPos.z = distToObject* Mathf.Sin(deltay* Mathf.Deg2Rad);

				//_radarBackGround.gameObject.transform.SetParent(_radarPosition.transform);
				_radarBackGround.gameObject.transform.position = new Vector3(radarPos.x, radarPos.z, 0) + _radarPosition.transform.position;

				radObject.Icon.transform.SetParent(_radarView.transform);
				radObject.Icon.transform.position = new Vector3(radarPos.x, radarPos.z, 0) + _radarPosition.transform.position;
			}
		}
		
	}
}