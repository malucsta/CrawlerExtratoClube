using Nest;

namespace Crawler.Infra.Elasticsearch
{
    internal class ElasticField : Components.Interfaces.Search.IField
    {
        private readonly Field _nestField;

        public ElasticField(Field nestField)
        {
            _nestField = nestField;
        }

        public string Name => _nestField.Name;
    }
}
