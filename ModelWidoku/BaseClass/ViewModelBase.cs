using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mvvmPilkarze.ModelWidoku.BaseClass
{
    class ViewModelBase: INotifyPropertyChanged
    {
      
        public event PropertyChangedEventHandler PropertyChanged;

     
        protected void onPropertyChanged(params string[] namesOfProperties)
        {
           
            if (PropertyChanged != null)
            {
                
                foreach (var prop in namesOfProperties)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(prop));
                }
            }
        }
    }
}
