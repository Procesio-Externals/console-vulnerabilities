namespace VulnerableSolution.EndlessLoop
{
    internal class LoopCode
    {
        private readonly LoopManager _loopManager;
        public LoopCode()
        {
            _loopManager = new LoopManager();
        }

        public void UseLoopCode()
        {
            _loopManager.UseLoopBll();
        }
    }
}
