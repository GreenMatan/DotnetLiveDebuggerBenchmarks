using LiveDebuggerPlayground.Common;
using System;
using System.Runtime.CompilerServices;

namespace LiveDebuggerPlayground.OptionB_Approach
{
    /// <summary>
    /// Pros:
    ///     - Better probability for inlining? (minimizes value type copies).
    ///     - Spare the need to create many EndMethod overloads (& a dedicated tool that will assist in creating these overloads) - simplifies the problem.
    ///     - Ability to create more fine-grained overloads (such as passing value types using with ref rather than copying etc).
    ///     - Less MethodRefs & MethodSpec creations.
    /// Cons:
    ///     - Increases IL size. (call instructions).
    ///     - Cannot reuse the native side of the CallTarget instrumentation as to be consistent we will have to rewrite the BeginMethod as well with 
    ///         BeginMethod_StartMarker and BeginMethod_EndMarker.
    /// </summary>
    public static class OptionB
    {
        #region BeginMethod

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static LiveDebuggerMethodState BeginMethod_StartMarker<TTarget>(/*int instrumentedMethodId, */TTarget instance)
        {
            return LiveDebuggerMethodState.GetDefault();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BeginMethod_EndMarker(LiveDebuggerMethodState state)
        {
        }

        #endregion

        #region EndMethod

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LiveDebuggerMethodState<TReturn> EndMethod_StartMarker<TTarget, TReturn>(TTarget instance, TReturn returnValue, Exception exception, LiveDebuggerMethodState state)
        {
            return new LiveDebuggerMethodState<TReturn>(returnValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void EndMethod_EndMarker(LiveDebuggerMethodState state)
        {
        }

        internal static void LogException<TTarget>(Exception exception)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Line Probe

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void LineProbe_StartMarker(LiveDebuggerMethodState state, int originalILOffset)
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void LineProbe_EndMarker(LiveDebuggerMethodState state)
        {
        }

        #endregion

        #region Passing Params & Args

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void LogLocal<TLocal>(uint index, TLocal local, LiveDebuggerMethodState state)
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void LogLocalByRef<TLocal>(uint index, ref TLocal local, LiveDebuggerMethodState state)
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void LogArg<TArg>(uint index, TArg arg, LiveDebuggerMethodState state)
        {
            FakeSerializer.SerializeSingle(arg);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void LogArgByRef<TArg>(uint index, ref TArg arg, LiveDebuggerMethodState state)
        {
            FakeSerializer.SerializeSingle(arg);
        }

        #endregion
    }
}
