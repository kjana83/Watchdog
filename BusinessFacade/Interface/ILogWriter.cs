
namespace BusinessFacade.Interface
{
    /// <summary>
    ///   <see cref="BusinessFacade.Interface.ILogWriter" />
    /// </summary>
    public interface ILogWriter
    {
        /// <summary>
        /// Writes the log.
        /// </summary>
        /// <param name="text">The text.</param>
        void WriteLog(string text);
    }
}
