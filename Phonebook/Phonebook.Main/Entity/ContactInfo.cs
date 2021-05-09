using Phonebook.Main.Entity.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.Main.Entity
{
    public class ContactInfo : BaseEntity
    {
        public EnumContactType ContactType { get; set; }
        public string Value { get; set; }
        public int PersonId { get; set; }
        public virtual Person Person { get; set; }

        public (double, double)? GetRoundedLocation()
        {
            switch (ContactType)
            {
                case EnumContactType.GeoLocation:
                    var splitted = Value.Split(",");
                    if (splitted != null && splitted.Any())
                    {
                        double lattitude = Math.Round(double.Parse(splitted.First()));
                        double longitude = Math.Round(double.Parse(splitted.Last()));
                        return (lattitude, longitude);
                    }
                    return null;
                case EnumContactType.PhoneType:
                case EnumContactType.EmailAddress:
                default:
                    return null;
            }
        }
    }
}
