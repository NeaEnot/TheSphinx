namespace TheSphinx.Core.Logic
{
    /// <include file='Docs.xml' path='docs/members[@name="ContextInitializer"]/ContextInitializer/*'/>
    public static class ContextInitializer
    {
        /// <include file='Docs.xml' path='docs/members[@name="ContextInitializer"]/Initialize/*'/>
        public static void Initialize(string storagePassword)
        {
            Context.Instance.StoragePassword = storagePassword;
            Context.Instance.Load();
        }
    }
}
