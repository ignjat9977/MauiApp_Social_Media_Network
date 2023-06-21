using MediaNetwork.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaNetwork.ViewModels
{
    public class SingleGroupViewModel : BaseViewModel
    {

        public MProp<int> Id { get; set; } = new MProp<int>();


        public SingleGroupViewModel()
        {
            
        }
    }
}
