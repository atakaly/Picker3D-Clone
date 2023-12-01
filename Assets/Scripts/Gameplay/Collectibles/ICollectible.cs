namespace Picker3D.Gameplay.Collectibles
{
    public interface ICollectible 
    {
        bool IsCollectible { get; }
        void OnCollected();
    }
}