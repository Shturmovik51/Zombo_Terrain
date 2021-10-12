using System.Collections.Generic;

namespace ZomboTerrain
{
    public sealed class OnSceneObjectsController : IInitialisible, IController
    {
        public List<IOnSceneObject> OnSceneObjects;

        public OnSceneObjectsController(List<IOnSceneObject> onSceneObjects)
        {
            OnSceneObjects = onSceneObjects;
        }       

        public void Initialization()
        {
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
