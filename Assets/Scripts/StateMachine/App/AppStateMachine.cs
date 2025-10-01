using System.Collections.Generic;
using StateMachine.Core;

namespace StateMachine.App
{
    public class AppStateMachine : StateMachine<BaseAppState, AppStateMachine>
    {
        public AppStateMachine(IEnumerable<BaseAppState> states) : base(states)
        {
            
        }
    }
}