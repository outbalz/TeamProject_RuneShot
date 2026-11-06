using UnityEngine;

public interface IEnemyControllInterface
{ 
    public void ActionEnter(Transform PlayerTr);
    public void ActionUpdate(Transform PlayerTr);
    public void ActionExit();
}
