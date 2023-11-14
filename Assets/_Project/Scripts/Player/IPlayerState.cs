using System;

namespace _Project.Scripts.Player
{
   public interface IPlayerState
    {
        event Action OnStart;
        event Action OnUpdate;
        event Action OnExit;
        
        void Enter();
        void Update(float deltaTime);
        void Exit();
    }
}