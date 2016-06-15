using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weingartner.Exceptional.Async
{
    public static class TaskExtensions
    {
        public static async Task<IExceptional<T>> ToTaskOfExceptional<T>(this Task<T> task)
        {
            try
            {
                return Weingartner.Exceptional.Exceptional.Ok(await task);
            }
            catch (Exception e)
            {
                return Exceptional.Fail<T>(e);
            }
        }
    }
}
