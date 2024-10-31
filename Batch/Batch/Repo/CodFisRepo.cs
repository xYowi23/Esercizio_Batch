using Batch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Batch.Repo
{
    internal class CodFisRepo : IRepoLettura<CodiceFiscale>
    {
        private static CodFisRepo? instance;
        public static CodFisRepo GetInstance()
        {
            if (instance == null)
                instance = new CodFisRepo();
            return instance;
        }
        public CodFisRepo() { }

        public List<CodiceFiscale> GetAll()
        {
           List<CodiceFiscale> elenco = new List<CodiceFiscale>();
            using (var ctx = new  BatchContext())
            {
                elenco = ctx.CodiceFiscales.ToList();
            }
            return elenco;
        }
    }
}
