using System.Collections.Generic;

namespace Dotal_War.Interfaces
{
    public interface ISystem
    {
        void Subscribe(int entityID);

        void UnSubscribe(int entityID);

    }
}
