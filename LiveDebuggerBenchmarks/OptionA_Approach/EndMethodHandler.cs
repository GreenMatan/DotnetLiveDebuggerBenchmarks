using LiveDebugger.Common;
using System;

namespace LiveDebugger.OptionA_Approach
{
    internal class EndMethodHandler<TIntegration, TTarget, TReturn>
    {
        internal static CallTargetReturn<TReturn> Invoke<TTarget, TReturn>(TTarget instance, TReturn returnValue, Exception exception, LiveDebuggerMethodState state)
        {
            throw new NotImplementedException();
        }
    }
}