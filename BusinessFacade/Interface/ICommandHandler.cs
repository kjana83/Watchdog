namespace BusinessFacade.Interface
{
    /// <summary>
    /// Command handler.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICommandHandler<in T>
    {
        /// <summary>
        /// Executes the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        void Execute(T input);
    }
}