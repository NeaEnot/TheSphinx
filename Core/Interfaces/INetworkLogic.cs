using System;

namespace TheSphinx.Core.Interfaces
{
    /// <include file='Docs.xml' path='docs/members[@name="INetworkLogic"]/Load/*'/>
    public interface INetworkLogic
    {
        /// <include file='Docs.xml' path='docs/members[@name="INetworkLogic"]/Connect/*'/>
        void Connect(Func<string> getCode);
        /// <include file='Docs.xml' path='docs/members[@name="INetworkLogic"]/Download/*'/>
        void Download(string path);
        /// <include file='Docs.xml' path='docs/members[@name="INetworkLogic"]/Upload/*'/>
        void Upload(string path);
    }
}
