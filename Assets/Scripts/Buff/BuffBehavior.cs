using System.Collections.Generic;

namespace ZomboTerrain
{
    public sealed class BuffBehavior : IInitialisible, IController
    {
        private PlayerController _playerController;
        private CollectableObject[] _collectableObjects;
        private BuffTimerController _buffTimerController;
        private Dictionary<BuffType, BuffMethods> _buffMethods;

        public BuffBehavior(PlayerController playerController, CollectableObject[] collectableObjects, BuffTimerController buffTimerController)
        {
            _playerController = playerController;
            _collectableObjects = collectableObjects;
            _buffTimerController = buffTimerController;
        }

        public void Initialization()
        {
            _buffMethods = new Dictionary<BuffType, BuffMethods>            //Designed By Aleksey Skvortsov
            {
                [BuffType.Speed] = _playerController.ChangeMoveSpeed,
                [BuffType.Jump] = _playerController.ChangeJumpForce
            };

            foreach (var collectableObject in _collectableObjects)
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

        //public void ChangePlayerParameter(Buff buff)
        //{
        //     _buffMethods[buff.Type](buff);
        //}
    }
}
