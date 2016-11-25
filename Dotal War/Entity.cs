using Dotal_War.Interfaces;
using System.Collections;


namespace Dotal_War
{
    public class Entity
    {
        #region Fields

        public int entityID;
        public Hashtable cBag;

        #endregion

        #region Constructor

        public Entity(int ID)
        {
            entityID = ID;
            cBag = new Hashtable();
        }

        #endregion

        #region Add&Remove

        public void AddComponent(IComponent component, object InitialValue)
        {
            component.AddComponent(this, InitialValue);
        }

        public void RemoveComponent(IComponent component)
        {
            component.RemoveComponent(this.entityID);
        }

        #endregion
    }
}
