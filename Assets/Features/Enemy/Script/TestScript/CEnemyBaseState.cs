public abstract class CEnemyBaseState
{
    public CEnemyControllerTest controller;

    public CEnemyBaseState(CEnemyControllerTest cEnemyController)
    {
        controller = cEnemyController;
    }
    public virtual void ActionEnter()
    {

    }
    public virtual void ActionUpdate()
    {

    }
    public virtual void ActionExit()
    {

    }
}
