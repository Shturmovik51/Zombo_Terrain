using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace ZomboTerrain
{
    public class RadarBackGround : MonoBehaviour
    {
        private string _path;
        private int _resolution = 5;
        private void Start()
        {
            _path = Application.dataPath;
            Pfoto();
        }
        private void Pfoto()
        {
            var filename = string.Format("{0:ddMMyyyy_HHmmssfff}.png", DateTime.Now);
            ScreenCapture.CaptureScreenshot(Path.Combine(_path, filename), _resolution);
        }
    }
}
