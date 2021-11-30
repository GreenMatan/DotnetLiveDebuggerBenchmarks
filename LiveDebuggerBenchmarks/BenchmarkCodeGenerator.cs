using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace LiveDebuggerPlayground
{
    internal static class BenchmarkCodeGenerator
    {
        const string OptionBTemplate = @"
[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TResult Run<TTarget, GENERIC_ARGS_MARKER, TResult>(TTarget targetInstance, PASSING_GENERICS, delegate*<GENERIC_ARGS_MARKER, TResult> bodyCallback)
        {
            TResult result = default;
            LiveDebuggerMethodState state = LiveDebuggerMethodState.GetDefault();
            LiveDebuggerMethodState<TResult> cReturn = LiveDebuggerMethodState<TResult>.GetDefault();
            Exception exception = null;
            try
            {
                try
                {
                    state = OptionB.BeginMethod_StartMarker<TTarget>(targetInstance);
                    WRITEARG_MARKER
                    OptionB.BeginMethod_EndMarker(state);
                }
                catch (Exception ex)
                {
                    OptionB.LogException<TTarget>(ex);
                    throw;
                }
                result = bodyCallback(ARGS_PASSING);
            }
            catch (Exception ex)
            {
                exception = ex;
                throw;
            }
            finally
            {
                try
                {
                    cReturn = OptionB.EndMethod_StartMarker<TTarget, TResult>(targetInstance, result, exception, state);
                    WRITEARG_MARKER
                    OptionB.EndMethod_EndMarker(state);
                    result = cReturn.GetReturnValue();
                }
                catch (Exception ex)
                {
                    OptionB.LogException<TTarget>(ex);
                    throw;
                }
            }
            return result;
        }";

        const string OptionBByRefTemplate = @"
[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TResult RunByRef<TTarget, GENERIC_ARGS_MARKER, TResult>(TTarget targetInstance, PASSING_GENERICS, delegate*<GENERIC_ARGS_MARKER, TResult> bodyCallback)
        {
            TResult result = default;
            LiveDebuggerMethodState state = LiveDebuggerMethodState.GetDefault();
            LiveDebuggerMethodState<TResult> cReturn = LiveDebuggerMethodState<TResult>.GetDefault();
            Exception exception = null;
            try
            {
                try
                {
                    state = OptionB.BeginMethod_StartMarker<TTarget>(targetInstance);
                    WRITEARG_BYREF_MARKER
                    OptionB.BeginMethod_EndMarker(state);
                }
                catch (Exception ex)
                {
                    OptionB.LogException<TTarget>(ex);
                    throw;
                }
                result = bodyCallback(ARGS_PASSING);
            }
            catch (Exception ex)
            {
                exception = ex;
                throw;
            }
            finally
            {
                try
                {
                    cReturn = OptionB.EndMethod_StartMarker<TTarget, TResult>(targetInstance, result, exception, state);
                    WRITEARG_BYREF_MARKER
                    OptionB.EndMethod_EndMarker(state);
                    result = cReturn.GetReturnValue();
                }
                catch (Exception ex)
                {
                    OptionB.LogException<TTarget>(ex);
                    throw;
                }
            }
            return result;
        }";

        const string BenchmarkCodeTemplate = @"
[Benchmark]
public unsafe void OPTION_NAME_NUM_OF_ARGS_Arguments_Benchmark()
{
    var bench = new LiveDebuggerBenchmarks();
    ARGS_INITIALIZE
    OPTION_NAME_BenchmarkHelpers.Run<LiveDebuggerBenchmarks, ARGS_TYPES, string>(bench, ARGS_PASSING, &OPTION_NAME_DoSomething);
}

private static string OPTION_NAME_DoSomething(PASSED_ARGS)
{
    return string.Empty;
}
";

        const string BenchmarkCodeByRefTemplate = @"
[Benchmark]
public unsafe void OPTION_NAME_NUM_OF_ARGS_Arguments_ByRef_Benchmark()
{
    var bench = new LiveDebuggerBenchmarks();
    ARGS_INITIALIZE
    OPTION_NAME_BenchmarkHelpers.RunByRef<LiveDebuggerBenchmarks, ARGS_TYPES, string>(bench, ARGS_PASSING, &OPTION_NAME_ByRef_DoSomething);
}

private static string OPTION_NAME_ByRef_DoSomething(PASSED_ARGS)
{
    return string.Empty;
}
";

        const string OptionATemplate = @"
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TResult Run<TTarget, GENERIC_ARGS_MARKER, TResult>(TTarget targetInstance, PASSING_GENERICS, delegate*<GENERIC_ARGS_MARKER, TResult> bodyCallback)
        {
            TResult result = default;
            LiveDebuggerMethodState state = LiveDebuggerMethodState.GetDefault();
            LiveDebuggerMethodState<TResult> cReturn = LiveDebuggerMethodState<TResult>.GetDefault();
            Exception exception = null;
            try
            {
                try
                {
                    state = OptionA.BeginMethod<TTarget, GENERIC_ARGS_MARKER>(targetInstance, ARGS_PASSING);
                }
                catch (Exception ex)
                {
                    OptionA.LogException<TTarget>(ex);
                    throw;
                }
                result = bodyCallback(ARGS_PASSING);
            }
            catch (Exception ex)
            {
                exception = ex;
                throw;
            }
            finally
            {
                try
                {
                    cReturn = OptionA.EndMethod<TTarget, TResult, GENERIC_ARGS_MARKER>(targetInstance, result, exception, state, 1, ARGS_PASSING);
                    result = cReturn.GetReturnValue();
                }
                catch (Exception ex)
                {
                    OptionA.LogException<TTarget>(ex);
                    throw;
                }
            }
            return result;
        }";

        const string OptionAByRefTemplate = @"
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TResult RunByRef<TTarget, GENERIC_ARGS_MARKER, TResult>(TTarget targetInstance, PASSING_GENERICS, delegate*<GENERIC_ARGS_MARKER, TResult> bodyCallback)
        {
            TResult result = default;
            LiveDebuggerMethodState state = LiveDebuggerMethodState.GetDefault();
            LiveDebuggerMethodState<TResult> cReturn = LiveDebuggerMethodState<TResult>.GetDefault();
            Exception exception = null;
            try
            {
                try
                {
                    state = OptionA.BeginMethodByRef<TTarget, GENERIC_ARGS_MARKER>(targetInstance, ARGS_BYREF_PASSING);
                }
                catch (Exception ex)
                {
                    OptionA.LogException<TTarget>(ex);
                    throw;
                }
                result = bodyCallback(ARGS_PASSING);
            }
            catch (Exception ex)
            {
                exception = ex;
                throw;
            }
            finally
            {
                try
                {
                    cReturn = OptionA.EndMethodByRef<TTarget, TResult, GENERIC_ARGS_MARKER>(targetInstance, result, exception, state, 1, ARGS_BYREF_PASSING);
                    result = cReturn.GetReturnValue();
                }
                catch (Exception ex)
                {
                    OptionA.LogException<TTarget>(ex);
                    throw;
                }
            }
            return result;
        }";

        const string BeginMethodTemplate = @"
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LiveDebuggerMethodState BeginMethod<TTarget, GENERIC_ARGS_MARKER>(TTarget instance, PASSING_GENERICS)
        {
            FakeSerializer.Serialize(ARGS_PASSING);
            return LiveDebuggerMethodState.GetDefault();
        }";

        const string BeginMethodByRefTemplate = @"
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LiveDebuggerMethodState BeginMethodByRef<TTarget, GENERIC_ARGS_MARKER>(TTarget instance, PASSING_BYREF_GENERICS)
        {
            FakeSerializer.Serialize(ARGS_PASSING);
            return LiveDebuggerMethodState.GetDefault();
        }";

        const string EndMethodTemplate = @"
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LiveDebuggerMethodState<TReturn> EndMethod<TTarget, TReturn, GENERIC_ARGS_MARKER>(
            TTarget instance,
            TReturn returnValue,
            Exception exception,
            LiveDebuggerMethodState state,
            int numOfArgs,
            PASSING_GENERICS)
        {
            FakeSerializer.Serialize(ARGS_PASSING);
            return new LiveDebuggerMethodState<TReturn>(returnValue);
        }
";

        const string EndMethodByRefTemplate = @"
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LiveDebuggerMethodState<TReturn> EndMethodByRef<TTarget, TReturn, GENERIC_ARGS_MARKER>(
            TTarget instance,
            TReturn returnValue,
            Exception exception,
            LiveDebuggerMethodState state,
            int numOfArgs,
            PASSING_BYREF_GENERICS)
        {
            FakeSerializer.Serialize(ARGS_PASSING);
            return new LiveDebuggerMethodState<TReturn>(returnValue);
        }
";

        public static void Generate(int numOfArgs)
        {
            // OptionA
            var optionABenchmarkCode = ReplaceAll(BenchmarkCodeTemplate, numOfArgs, "OptionA");
            var optionA_BenchmarkHelpers = ReplaceAll(OptionATemplate, numOfArgs, "OptionA");
            var beginMethodCode = ReplaceAll(BeginMethodTemplate, numOfArgs, "OptionA");
            var endMethodCode = ReplaceAll(EndMethodTemplate, numOfArgs, "OptionA");

            // OptionA_ByRef
            var optionAByRefBenchmarkCode = ReplaceAll(BenchmarkCodeByRefTemplate, numOfArgs, "OptionA");
            var optionA_BenchmarkHelpers_ByRef = ReplaceAll(OptionAByRefTemplate, numOfArgs, "OptionA");
            var beginMethodByRefCode = ReplaceAll(BeginMethodByRefTemplate, numOfArgs, "OptionA");
            var endMethodByRefCode = ReplaceAll(EndMethodByRefTemplate, numOfArgs, "OptionA");

            // OptionB
            var optionBBenchmarkCode = ReplaceAll(BenchmarkCodeTemplate, numOfArgs, "OptionB");
            var optionB_BenchmarkHelpers = ReplaceAll(OptionBTemplate, numOfArgs, "OptionB");

            // OptionB_ByRef
            var optionBByRefBenchmarkCode = ReplaceAll(BenchmarkCodeByRefTemplate, numOfArgs, "OptionB");
            var optionB_BenchmarkHelpers_ByRef = ReplaceAll(OptionBByRefTemplate, numOfArgs, "OptionB");

            Debugger.Break();
        }

        private static string ReplaceAll(string template, int numOfArgs, string optionName)
        {
            const string GENERIC_ARGS_MARKER = nameof(GENERIC_ARGS_MARKER);
            const string PASSING_GENERICS = nameof(PASSING_GENERICS);
            const string DELEGATE_MARKER = nameof(DELEGATE_MARKER);
            const string ARGS_INITIALIZE = nameof(ARGS_INITIALIZE);
            const string ARGS_TYPES = nameof(ARGS_TYPES);
            const string ARGS_PASSING = nameof(ARGS_PASSING);
            const string PASSED_ARGS = nameof(PASSED_ARGS);
            const string WRITEARG_MARKER = nameof(WRITEARG_MARKER);
            const string OPTION_NAME = nameof(OPTION_NAME);
            const string NUM_OF_ARGS = nameof(NUM_OF_ARGS);
            const string PASSING_BYREF_GENERICS = nameof(PASSING_BYREF_GENERICS);
            const string ARGS_BYREF_PASSING = nameof(ARGS_BYREF_PASSING);
            const string WRITEARG_BYREF_MARKER = nameof(WRITEARG_BYREF_MARKER);

            var range = Enumerable.Range(1, numOfArgs + 1);

            var argsInit = string.Join("\n", range.Select(i => $"BigInteger arg{i} = new BigInteger({i});"));
            var argsTypes = string.Join(", ", range.Select(i => $"BigInteger"));
            var argsPassing = string.Join(", ", range.Select(i => $"arg{i}"));
            var argsByRefPassing = string.Join(", ", range.Select(i => $"ref arg{i}"));
            var passedArgs = string.Join(", ", range.Select(i => $"BigInteger arg{i}"));
            var genericArgs = string.Join(", ", range.Select(i => $"TArg{i}"));
            var passingGenerics = string.Join(", ", range.Select(i => $"TArg{i} arg{i}"));
            var passingGenericsByRef = string.Join(", ", range.Select(i => $"ref TArg{i} arg{i}"));
            var writeArgs = string.Join("\n", range.Select(i => $"OptionB.LogArg<TArg{i}>({i - 1}, arg{i}, state);"));
            var writeArgsByRef = string.Join("\n", range.Select(i => $"OptionB.LogArgByRef<TArg{i}>({i - 1}, ref arg{i}, state);"));

            return template
                .Replace(ARGS_INITIALIZE, argsInit)
                .Replace(ARGS_TYPES, argsTypes)
                .Replace(ARGS_PASSING, argsPassing)
                .Replace(PASSED_ARGS, passedArgs)
                .Replace(PASSING_GENERICS, passingGenerics)
                .Replace(GENERIC_ARGS_MARKER, genericArgs)
                .Replace(WRITEARG_MARKER, writeArgs)
                .Replace(OPTION_NAME, optionName)
                .Replace(NUM_OF_ARGS, numOfArgs.ToString())
                .Replace(PASSING_BYREF_GENERICS, passingGenericsByRef)
                .Replace(ARGS_BYREF_PASSING, argsByRefPassing)
                .Replace(WRITEARG_BYREF_MARKER, writeArgsByRef);
        }
    }
}
