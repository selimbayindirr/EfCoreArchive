using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCore.Entities
{
    public class Address
    {
        //[Key,ForeignKey(nameof(Person))] //Data ANNOTATION
        public int Id { get; set; }
        public String Adres { get; set; }
        //public int PersonId { get; set; }//bunu ayırt ettirmek için yazıyoruz default Convertion
        public Person Person { get; set; }
    }
}
