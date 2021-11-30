namespace LiveDebuggerPlayground.OptionA_Approach
{
    public class CallTargetReturn<TReturn>
    {
        private TReturn returnValue;

        public CallTargetReturn(TReturn returnValue)
        {
            this.returnValue = returnValue;
        }
    }
}