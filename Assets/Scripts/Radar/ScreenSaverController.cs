using System;
using System.Collections;
using System.IO;
using UnityEngine;

namespace ZomboTerrain
{
    public sealed class ScreenSaverController : IInitialisible, IController
    {
        private Camera[] _cameras;
        private string _path;
        private int _resolution = 5;
        public ScreenSaverController(Camera[] cameras)
        {
            _cameras = cameras;          
        }

        [SerializeField] private Camera _camera;
        public void Initialization()
        {
            _path = Application.dataPath + "/Resources/RadarBackGround";
           
            DoTapExampleAsync().SaimonSaidStartCoroutine();            
        }
        public IEnumerator DoTapExampleAsync()
        {
            foreach (var camera in _cameras)
            {
                if (camera.CompareTag("RadarBackGroundCamera"))
                    camera.enabled = true;
                else
                    camera.enabled = false;
            }

            var sw = Screen.width;
            var sh = Screen.height;
            yield return new WaitForEndOfFrame();
            var sc = new Texture2D(sw, sh, TextureFormat.RGB24, true);
            sc.ReadPixels(new Rect(0, 0, sw, sh), 0, 0);
            var bytes = sc.EncodeToPNG();
            var filename = "RadarBG.png";    /*String.Format("{0:ddMMyyyy_HHmmssfff}.png", DateTime.Now)*/
            File.WriteAllBytes(Path.Combine(_path, filename), bytes);
            yield return new WaitForSeconds(2.3f);


            foreach (var camera in _cameras)
            {
                if (camera.CompareTag("RadarBackGroundCamera"))
                    camera.enabled = false;
                else
                    camera.enabled = true;
            }
        }

        private void Photo()
        {
            var filename = string.Format("{0:ddMMyyyy_HHmmssfff}.png", DateTime.Now);
            ScreenCapture.CaptureScreenshot(Path.Combine(_path, filename), _resolution);
        }
    }
}
