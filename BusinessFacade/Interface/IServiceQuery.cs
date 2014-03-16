namespace BusinessFacade.Interface
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TInput">The type of the input.</typeparam>
    /// <typeparam name="TOutput">The type of the output.</typeparam>
    public interface IQueryFor<in TInput, out TOutput>
    {
        /// <summary>
        /// Executes the query with.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        TOutput ExecuteQueryWith(TInput input);
    }
}