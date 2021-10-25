using System.Collections.Generic;

namespace ZomboTerrain
{
    public sealed class OnSceneObjectsController : IInitialisible, IController
    {
        public OnSceneObjectInitializator _onSceneObjectInitializator;
        public List<IOnSceneObject> OnSceneObjects;

        public OnSceneObjectsController(OnSceneObjectInitializator onSceneObjectInitializator)
        {
            _onSceneObjectInitializator = onSceneObjectInitializator;
        }       

        public void Initialization()
        {
            OnSceneObjects = _onSceneObjectInitializator.InitObjects();
            AddRadarObjectsToRadar();
        }

        private void AddRadarObjectsToRadar()
        {
            foreach (IObservableObject observableObject in OnSceneObjects)
            {
                observableObject.AddObjectToRadar();
            }
        }
    }
}
