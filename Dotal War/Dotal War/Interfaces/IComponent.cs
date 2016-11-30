namespace Dotal_War.Interfaces
{
    public interface IComponent
    {
        void AddComponent(Entity target, object InitialValue);

        void RemoveComponent(int entityID);

    }
}
