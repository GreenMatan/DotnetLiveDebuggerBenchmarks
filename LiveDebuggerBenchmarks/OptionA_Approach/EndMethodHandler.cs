using LiveDebuggerPlayground.Common;
using System;

namespace LiveDebuggerPlayground.OptionA_Approach
{
    internal class EndMethodHandler<TIntegration, TTarget, TReturn>
    {
        internal static CallTargetReturn<TReturn> Invoke<TTarget, TReturn>(TTarget instance, TReturn returnValue, Exception exception, LiveDebuggerMethodState state)
        {
            throw new NotImplementedException();
        }
    }
}