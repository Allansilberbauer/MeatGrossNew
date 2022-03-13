using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Repository
{
    public class ClassNotify : INotifyPropertyChanged
    {
        /// <summary>
        /// Denne class har til formål at tilføre funtionalitet til de class'es der nedarver denne class.
        /// Den funktionalitet ClassNotify tilfører, er muligheden for at styrer notifikationen af en hændelse i et Property.
        /// Dette opnåes ved at ClassNotify implementere interfacet INotifyPropertyChanged.
        /// Dette interface sætter krav om at der skal oprettes en public event af typen PropertyChangedEventHandler.
        /// I metoden Notify benyttes denne event til at afgøre om der er sket ændringer i den class som nedarver ClassNotify.
        /// Hvis der er sket ændringer benyttes instansen PropertyChanged til at igangsætte en overførelse af data fra et
        /// givent Property til GUI hvor der er oprettet en Binding med et id(navnet på Property) der modsvarer navnet på det Property der er 
        /// blevet opdateret med en ny værdi.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        public ClassNotify()
        {

        }
        protected void Notify(string propertyName)
        {
            if(this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
