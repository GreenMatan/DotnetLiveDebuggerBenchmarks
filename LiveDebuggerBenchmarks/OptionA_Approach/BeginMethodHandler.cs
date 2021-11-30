using LiveDebuggerPlayground.Common;
using System;

namespace LiveDebuggerPlayground.OptionA_Approach
{
    internal class BeginMethodHandler<TIntegration, TTarget>
    {
        internal static LiveDebuggerMethodState Invoke<TTarget>(TTarget instance)
        {
            throw new NotImplementedException();
        }

        internal LiveDebuggerMethodState Invoke<TTarget, TArg1>(TTarget instance, TArg1 arg1)
        {
            throw new NotImplementedException();
        }

        internal LiveDebuggerMethodState Invoke<TTarget, TArg1, TArg2>(TTarget instance, TArg1 arg1, TArg2 arg2)
        {
            throw new NotImplementedException();
        }
    }
}