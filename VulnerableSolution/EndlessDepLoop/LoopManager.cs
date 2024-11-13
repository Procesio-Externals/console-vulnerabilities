namespace VulnerableSolution.EndlessDepLoop
{
    internal class LoopManager
    {
        private readonly LoopBll _loopBll;
        public LoopManager()
        {
            _loopBll = new LoopBll();
        }

        public void UseLoopBll()
        {
            _loopBll.DoSomething();
        }
    }
}
