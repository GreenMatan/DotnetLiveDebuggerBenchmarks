using System;
using System.Runtime.CompilerServices;

namespace LiveDebugger.Common
{
    public readonly struct LiveDebuggerMethodState<T>
    {
        private readonly T _returnValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="LiveDebuggerMethodState{T}"/> struct.
        /// </summary>
        /// <param name="returnValue">Return value</param>
        public LiveDebuggerMethodState(T returnValue)
        {
            _returnValue = returnValue;
        }

        /// <summary>
        /// Gets the default call target return value (used by the native side to initialize the locals)
        /// </summary>
        /// <returns>Default call target return value</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LiveDebuggerMethodState<T> GetDefault()
        {
            return default;
        }

        /// <summary>
        /// Gets the return value
        /// </summary>
        /// <returns>Return value</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetReturnValue() => _returnValue;

        /// <summary>
        /// ToString override
        /// </summary>
        /// <returns>String value</returns>
        public override string ToString()
        {
            return $"{typeof(LiveDebuggerMethodState<T>).FullName}({_returnValue})";
        }
    }

    public readonly struct LiveDebuggerMethodState
    {
        private readonly int InstrumentedMethodId;

        /// <summary>
        /// Gets the default call target state (used by the native side to initialize the locals)
        /// </summary>
        /// <returns>Default call target state</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LiveDebuggerMethodState GetDefault()
        {
            return default;
        }
    }
}