namespace VulnerableSolution.InjectionLoop
{
    public class InjectionLoopBll : IInjectionLoopBll
    {
        private readonly IInjectionLoopManager _injectionLoopManager;
        public InjectionLoopBll(IInjectionLoopManager injectionLoopManager)
        {
            _injectionLoopManager = injectionLoopManager;
        }
    }
}
