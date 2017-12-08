using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FES.AusleihSystem.ViewModels
{
    public class GerateTabelleViewModel
    {

        public List<GeraetViewModel> Geraete { get; private set; } = new List<GeraetViewModel>();

        public void AddGeraet(GeraetViewModel geraet)
        {
            Geraete.Add(geraet);
        }
        public void CleanList()
        {
            Geraete = new List<GeraetViewModel>();
        }
    }
}
