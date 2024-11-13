namespace VulnerableSolution.EndlessDepLoop
{
    internal class LoopBll
    {
        private readonly LoopCode _loopCode;
        public LoopBll()
        {
            _loopCode = new LoopCode();
        }

        public void DoSomething()
        {
            _loopCode.UseLoopCode();
        }
    }
}
