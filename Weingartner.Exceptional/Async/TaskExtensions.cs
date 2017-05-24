using System;
using System.Threading.Tasks;

namespace Weingartner.Async
{
    public static class TaskExtensions
    {
        public static async Task<IExceptional<T>> ToTaskOfExceptional<T>(this Task<T> task)
        {
            try
            {
                return Exceptional.Ok(await task);
            }
            catch (Exception e)
            {
                return Exceptional.Fail<T>(e);
            }
        }
    }
}
