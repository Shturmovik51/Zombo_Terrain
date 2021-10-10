using System.Data;
using System.IO;
using UnityEngine;

namespace ZomboTerrain
{
    public sealed class SaveDataRepository : ISaveDataRepository, IInitialisible, IController
    {
        private IData<SavedData> _data;
        private string _path;
        private InputController _inputController;

        private const string _folderName = "dataSave";
        private const string _fileName = "data.bat";

        public SaveDataRepository(InputController inputController)
        {
            _inputController = inputController;
        }

        public void Initialization()
        {
            _data = new JsonData<SavedData>();
            _path = Path.Combine(Application.dataPath, _folderName);
            _inputController.OnClickSaveGameButton += Save;
            _inputController.OnClickLoadGameButton += Load;
        }

        public void Save()
        {
            if (!Directory.Exists(Path.Combine(_path)))
            {
                Directory.CreateDirectory(_path);
            }
            var savePlayer = new SavedData
            {
                //Position = player.transform.position,
                Name = "Roman",
                IsEnabled = true
            };

            _data.Save(savePlayer, Path.Combine(_path, _fileName));
            Debug.Log("Save");
        }

        public void Load()
        {
            var file = Path.Combine(_path, _fileName);
            if (!File.Exists(file))
            {
                throw new DataException($"File {file} not found");
            }
            var newPlayerModel = _data.Load(file);
           // playerModel.transform.position = newPlayerModel.Position;
           // playerModel.name = newPlayerModel.Name;
           //playerModel.gameObject.SetActive(newPlayerModel.IsEnabled);

            Debug.Log(newPlayerModel);
        }
    }
}