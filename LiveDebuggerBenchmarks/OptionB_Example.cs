using LiveDebuggerPlayground.OptionB_Approach;
using LiveDebuggerPlayground.Common;
using System;

namespace LiveDebuggerPlayground
{
    /// <summary>
    /// Pros:
    ///     - Spare the need to create many EndMethod overloads (& source generator that will assist in creating these overloads).
    ///     - No need to fallback for a slow-path (object array).
    ///     - Ability to create more fine-grained overloads (such as passing certain value types by ref).
    ///     - Better probability for inlining?
    ///     - Nit: Higher probability of MethodSpecs sharing (compared to Option A).
    /// Cons:
    ///     - Increases IL size. (call instructions).
    ///     - * The markers might introduce complexity. We have to keep track of each StartMarker and EndMarker appropriately.
    /// </summary>
    class OptionB_Example
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
        public string OptionB_MethodProbe_Instrumented_Greeting(string name, int age)
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
                        state = OptionB.BeginMethod_StartMarker<OptionB_Example>(this);
                        OptionB.LogArg<string>(0, name, state);
                        OptionB.LogArg<int>(1, age, state);
                        OptionB.BeginMethod_EndMarker(state);
                    }
                    catch
                    {
                        OptionB.LogException<OptionB_Example>(exception);
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
                    OptionB.EndMethod_StartMarker(this, returnValue, exception, state);
                    OptionB.LogArg<string>(0, name, state);
                    OptionB.LogArg<int>(1, age, state);
                    OptionB.LogLocal<Person>(0, person, state);
                    OptionB.EndMethod_EndMarker(state);
                }
                catch
                {
                    OptionB.LogException<OptionB_Example>(exception);
                }
            }

            return returnValue;
        }
    }
}
