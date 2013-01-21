using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightstarDB.Graph
{
    public class Context
    {
        private readonly List<List<Uri>> _stages;
        private readonly Dictionary<string, int> _stageLabels; 

        public Context()
        {
            _stages = new List<List<Uri>>();
            _stageLabels = new Dictionary<string, int>();
        }

        public void AddSet(List<Uri> uris)
        {
            _stages.Add(uris);
        }

        public void LabelSet(string label)
        {
            _stageLabels.Add(label, _stages.Count - 1);
        }

        public List<Uri> Latest { get { return _stages.Last(); } } 

    }
}
