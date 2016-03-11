namespace GdNet.Data.EF.Strategies
{
    public interface ISavingStrategy
    {
        /// <summary>
        /// Before saving the entity(ies)
        /// </summary>
        void OnSaving();

        /// <summary>
        /// After saved the entity(ies)
        /// </summary>
        void OnSaved();
    }
}