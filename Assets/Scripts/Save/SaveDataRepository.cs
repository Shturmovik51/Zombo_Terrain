using System.Collections.Generic;
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
        private List<IOnSceneObject> _onSceneObjects;

        private const string _folderName = "dataSave";
        private const string _fileName = "data.bat";

        public SaveDataRepository(InputController inputController, OnSceneObjectsController onSceneObjectsController)
        {
            _inputController = inputController;
            _onSceneObjects = onSceneObjectsController.OnSceneObjects;
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
            
            var objectsActiveCondition = new List<bool>(_onSceneObjects.Count);

            for (int i = 0; i < _onSceneObjects.Count; i++)
            {
                objectsActiveCondition.Add(_onSceneObjects[i].IsActive);
            }

            var saveObjects = new SavedData()
            {
                ObjectsActiveCondition = objectsActiveCondition
            };            

            _data.Save(saveObjects, Path.Combine(_path, _fileName));
            Debug.Log("Save");
        }

        public void Load()
        {
            var file = Path.Combine(_path, _fileName);

            if (!File.Exists(file))
            {
                throw new DataException($"File {file} not found");
            }

            var savedObjects = _data.Load(file).ObjectsActiveCondition;

            for (int i = 0; i < savedObjects.Count; i++)
            {
                if (_onSceneObjects[i].IsActive != savedObjects[i])
                    _onSceneObjects[i].ObjectActivation(savedObjects[i]);
            }

            Debug.Log("Load");
        }
    }
}