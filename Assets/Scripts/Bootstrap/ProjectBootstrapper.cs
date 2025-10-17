using Cysharp.Threading.Tasks;
using StateMachine.App;
using Zenject;

namespace Bootstrap
{
    public class ProjectBootstrapper : IInitializable
    {
        private readonly AppStateMachine _stateMachine;

        public ProjectBootstrapper(AppStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Initialize()
        {
            _stateMachine.GoToState<MainMenuAppState>().Forget();
        }
    }
}