using System.Collections.Generic;

namespace ZomboTerrain
{
    public sealed class BuffBehavior : IInitialisible, IController
    {
        private PlayerController _playerController;
        private List<IOnSceneObject> _onSceneObjects;
        private BuffTimerController _buffTimerController;
        private Dictionary<BuffType, BuffMethods> _buffMethods;

        public BuffBehavior(PlayerController playerController, List<IOnSceneObject> onSceneObjects, BuffTimerController buffTimerController)
        {
            _playerController = playerController;
            _onSceneObjects = onSceneObjects;
            _buffTimerController = buffTimerController;
        }

        public void Initialization()
        {
            _buffMethods = new Dictionary<BuffType, BuffMethods>            //Designed By Aleksey Skvortsov
            {
                [BuffType.Speed] = _playerController.ChangeMoveSpeed,
                [BuffType.Jump] = _playerController.ChangeJumpForce
            };

            foreach (CollectableObject collectableObject in _onSceneObjects)
            {
                collectableObject.OnApplyBuff += ApplyBuff;
            }
        }

        private void ApplyBuff(Buff buff)
        {
            buff.Method = _buffMethods[buff.Type];
            buff.Method(buff.BonusValue);
            _buffTimerController.AddBuffToTimer(buff);
        }
    }
}
