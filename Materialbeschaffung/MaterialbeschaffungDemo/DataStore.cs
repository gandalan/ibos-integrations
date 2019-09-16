using Gandalan.IDAS.WebApi.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MaterialbeschaffungDemo
{
    /// <summary>
    /// This DataStore matches IBOS3's local data store for material. In the future, 
    /// another store will access the IDAS WebApi instead
    /// </summary>
    class DataStore
    {
        private readonly string _dataDir;

        public DataStore()
        {
            var ibos3DataDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Gandalan", "IBOS3");
            _dataDir = Path.Combine(ibos3DataDir, "Produktion", "Materialbeschaffung");
        }

        public IList<MaterialBeschaffungsJobDTO> Get(Func<MaterialBeschaffungsJobDTO, bool> filter)
        {
            var all = GetAll();
            return all.Where(i => filter(i)).ToList();
        }

        public IList<MaterialBeschaffungsJobDTO> GetAll()
        {
            List<MaterialBeschaffungsJobDTO> result = new List<MaterialBeschaffungsJobDTO>();
            if (Directory.Exists(_dataDir))
            {
                foreach (var fn in Directory.GetFiles(_dataDir, "*.mdata")) // not really efficient! pr
                {
                    var temp = JsonConvert.DeserializeObject<MaterialBeschaffungsJobDTO>(File.ReadAllText(fn));
                    result.Add(temp);
                }
            }
            return result;
        }

        public void Put(MaterialBeschaffungsJobDTO item)
        {
            if (!Directory.Exists(_dataDir))
                Directory.CreateDirectory(_dataDir);
            var filename = Path.Combine(_dataDir, item.MaterialBeschaffungsJobGuid.ToString() + ".mdata");
            File.WriteAllText(filename, JsonConvert.SerializeObject(item));
        }

        public void Remove(MaterialBeschaffungsJobDTO item)
        {
            if (!Directory.Exists(_dataDir))
                Directory.CreateDirectory(_dataDir);
            var filename = Path.Combine(_dataDir, item.MaterialBeschaffungsJobGuid.ToString() + ".mdata");

            if (File.Exists(filename))
                File.Delete(filename);
        }
    }
}
