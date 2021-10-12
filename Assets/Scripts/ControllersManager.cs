using System.Collections.Generic;

namespace ZomboTerrain
{
    public sealed class ControllersManager : IInitialisible, IUpdatable, ILateUpdatable, IFixedUpdatable, ICleanable
    {
        private readonly List<IInitialisible> _initialisibleControllers;
        private readonly List<IUpdatable> _updatableControllers;
        private readonly List<ILateUpdatable> _lateUpdatableControllers;
        private readonly List<IFixedUpdatable> _fixedUpdatableControllers;
        private readonly List<ICleanable> _cleanableControllers;

        public ControllersManager()
        {
            _initialisibleControllers = new List<IInitialisible>(11);
            _updatableControllers = new List<IUpdatable>(11);
            _lateUpdatableControllers = new List<ILateUpdatable>(11);
            _fixedUpdatableControllers = new List<IFixedUpdatable>(11);
            _cleanableControllers = new List<ICleanable>(11);
        }

        public ControllersManager Add(IController controller)
        {
            if (controller is IInitialisible initializeController)
            {
                _initialisibleControllers.Add(initializeController);
            }

            if (controller is IUpdatable executeController)
            {
                _updatableControllers.Add(executeController);
            }

            if (controller is ILateUpdatable lateUpdatableControllers)
            {
                _lateUpdatableControllers.Add(lateUpdatableControllers);
            }

            if (controller is IFixedUpdatable fixedUpdatableControllers)
            {
                _fixedUpdatableControllers.Add(fixedUpdatableControllers);
            }

            if (controller is ICleanable cleanupController)
            {
                _cleanableControllers.Add(cleanupController);
            }

            return this;
        }

        public void Initialization()
        {
            for (var index = 0; index < _initialisibleControllers.Count; ++index)
            {
                _initialisibleControllers[index].Initialization();
            }
        }

        public void LocalUpdate(float deltaTime)
        {
            for (var index = 0; index < _updatableControllers.Count; ++index)
            {
                _updatableControllers[index].LocalUpdate(deltaTime);
            }
        }

        public void LocalLateUpdate(float deltaTime)
        {
            for (var index = 0; index < _lateUpdatableControllers.Count; ++index)
            {
                _lateUpdatableControllers[index].LocalLateUpdate(deltaTime);
            }
        }

        public void LocalFixedUpdate(float fixedDeltaTime)
        {
            for (var index = 0; index < _fixedUpdatableControllers.Count; ++index)
            {
                _fixedUpdatableControllers[index].LocalFixedUpdate(fixedDeltaTime);
            }
        }

        public void CleanUp()
        {
            for (var index = 0; index < _cleanableControllers.Count; ++index)
            {
                _cleanableControllers[index].CleanUp();
            }
        }
    }
}

