using System.Collections.Generic;

namespace ZomboTerrain
{
    public class OnSceneObjectsController : IInitialisible, IController
    {
        public List<IOnSceneObject> _onSceneObjects;

        public OnSceneObjectsController(List<IOnSceneObject> onSceneObjects)
        {
            _onSceneObjects = onSceneObjects;
        }       

        public void Initialization()
        {
            AddRadarObjectsToRadar();
        }

        private void AddRadarObjectsToRadar()
        {
            foreach (IObservableObject observableObject in _onSceneObjects)
            {
                observableObject.AddObjectToRadar();
            }
        }

    }
}
