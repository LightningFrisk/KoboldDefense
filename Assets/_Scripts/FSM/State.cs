using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace R2
{
    public class State
    {
        bool forceExit;
        List<StateAction> fixedUpdateActions = new List<StateAction>();
        List<StateAction> updateActions = new List<StateAction>();
        List<StateAction> lateUpdateActions = new List<StateAction>();

        public State(List<StateAction> fixedUpdateActions, List<StateAction> updateActions, List<StateAction> lateUpdateActions)
        {
            this.fixedUpdateActions = fixedUpdateActions;
            this.updateActions = updateActions;
            this.lateUpdateActions = lateUpdateActions;
        }

        public void FixedTick() {
            ExecuteListofActions(fixedUpdateActions);
        }

        public void Tick()
        {
            ExecuteListofActions(updateActions);
        }
        
        public void LateTick()
        {
            ExecuteListofActions(lateUpdateActions);
            forceExit = false;
        }

        void ExecuteListofActions(List<StateAction> l)
        {
            for(int i = 0; i< l.Count; i++)
            {
                if (forceExit)
                    return;

                forceExit = l[i].Execute();
            }
        }
    }
}

