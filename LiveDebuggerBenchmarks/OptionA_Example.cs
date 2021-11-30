using LiveDebuggerPlayground.Common;
using LiveDebuggerPlayground.OptionA_Approach;
using System;

namespace LiveDebuggerPlayground
{
    /// <summary>
    /// Pros:
    ///     - Minimizes IL pollution compared to Option B.
    /// Cons:
    ///     - Need to create many EndMethod overloads (& source generator that will assist in creating these overloads).
    ///     - Still need to have a slow fallback (boxing&casting).
    ///     - Incurs the overhead of copying very large value types (same as in CallTarget instrumentation).
    ///     - Nit: Less probability of sharing MethodSpec tokens for each BeginMethod and EndMethod.
    /// </summary>
    class OptionA_Example
    {
        /// <summary>
        /// Original method.
        /// </summary>
        public string Greeting(string name, int age)
        {
            Person person = Person.Create(name, age);
            return $"Hello {person}!";
        }

        /// <summary>
        /// Instrumented method.
        /// </summary>
        public string OptionA_MethodProbe_Instrumented_Greeting(string name, int age)
        {
            // Live debugger locals
            LiveDebuggerMethodState state = default;
            Exception exception = null;
            string returnValue = null;

            // Original local
            Person person = null;

            try
            {
                try
                {
                    try
                    {
                        state = OptionA.BeginMethod<OptionA_Example, string, int>(this, name, age);
                    }
                    catch
                    {
                        OptionA.LogException<OptionA_Example>(exception);
                    }

                    // Original method instructions
                    person = Person.Create(name, age);
                    returnValue = $"Hello {person}!";
                }
                catch (Exception ex)
                {
                    exception = ex;
                    throw;
                }
            }
            finally
            {
                try
                {
                    OptionA.EndMethod<OptionA_Example, string, string, int, Person>(this, returnValue, exception, state, 2, name, age, person);
                }
                catch
                {
                    OptionA.LogException<OptionA_Example>(exception);
                }
            }

            return returnValue;
        }
    }
}
