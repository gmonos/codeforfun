namespace Zags.Logging.Events
{
    public class UserActionEvent : LogEvent
    {
        protected readonly ILog Logger = LogManager.GetLogger("DomainTrakingEvent");

        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public override LogLevel Level => LogLevel.Info;

        public UserActionEvent(string controllerName, string actionName)
        {
            ControllerName = controllerName;
            ActionName = actionName;
        }

        public override void Log()
        {
            Logger.Info("{@UserActionEvent}", this);
        }
    }
}
