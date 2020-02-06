namespace Managers
{
    public interface Manager
    {
        /// <summary>
        /// Will be called once whenever the Manager itself says is ready or whenever the Manager Owner forces to be so.
        /// </summary>
        void OnReady();

        /// <summary>
        /// Will be called after the Manager Owner tries to Init the manager and all managers of the Manager Owner are ready.
        /// </summary>
        void OnInit();

        /// <summary>
        /// Will be called once per frame after the Manager has been inited.
        /// </summary>
        void UpdateManager();
    }
}