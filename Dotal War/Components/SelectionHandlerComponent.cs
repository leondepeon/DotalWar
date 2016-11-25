using Dotal_War.Interfaces;


namespace Dotal_War.Components
{
    public class SelectionHandlerComponent:IComponent
    {
        ISystem mySystem;
        SelectionType selectionType;

        #region Methodes

        public SelectionHandlerComponent(ISystem mySystem)
        {
            this.mySystem = mySystem;
        }

        void IComponent.AddComponent(Entity target, object InitialValue)
        {

            if (!target.cBag.ContainsKey(DataType.SelectionType))
            {
                target.cBag.Add(DataType.SelectionType, InitialValue);
            }

            if (!target.cBag.ContainsKey(DataType.IsSelected))
            {
                target.cBag.Add(DataType.IsSelected, false);
            }

            if (!target.cBag.ContainsKey(DataType.IsHoverOver))
            {
                target.cBag.Add(DataType.IsHoverOver, false);
            }

            mySystem.Subscribe(target.entityID);
        }

        void IComponent.RemoveComponent(int entityID)
        {
            mySystem.UnSubscribe(entityID);
        }

        #endregion
    }
}
