namespace Manager.Domain.Entities
using System.Collections.Generic;
{
    public abstract class Base
    {
        public long Id { get; set; }

        internal List<string> _errors;
        
        public IReadOnlyCollections> Errors => _errors;
        public abstract bool Validade();
    }
}