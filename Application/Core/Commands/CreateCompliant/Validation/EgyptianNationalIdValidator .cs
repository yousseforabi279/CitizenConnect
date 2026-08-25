using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Commands.CreateCompliant.Validation
{
    public class EgyptianNationalIdValidator : INationalIdValidator
    {
        private static readonly HashSet<string> ValidGovernorateCodes =
        new()
        {
            "01", // Cairo
            "02", // Alexandria
            "03", // Port Said
            "04", // Suez
            "11", // Damietta
            "12", // Dakahlia
            "13", // Sharkia
            "14", // Qalyubia
            "15", // Kafr El Sheikh
            "16", // Gharbia
            "17", // Menoufia
            "18", // Beheira
            "19", // Ismailia
            "21", // Giza
            "22", // Beni Suef
            "23", // Fayoum
            "24", // Minya
            "25", // Assiut
            "26", // Sohag
            "27", // Qena
            "28", // Aswan
            "29", // Luxor
            "31", // Red Sea
            "32", // New Valley
            "33", // Matrouh
            "34", // North Sinai
            "35", // South Sinai
            "88"  // Foreign-born
        };

        public bool IsValid(string nationalId)
        {

            if (string.IsNullOrWhiteSpace(nationalId))
                return false;

            if (nationalId.Length != 14)
                return false;

            if (!nationalId.All(char.IsDigit))
                return false;
            // 2. Century
            int centuryDigit = nationalId[0] - '0';

            if (centuryDigit != 2 && centuryDigit != 3)
                return false;

            // 3. Birth date
            int year = int.Parse(nationalId.Substring(1, 2));
            int month = int.Parse(nationalId.Substring(3, 2));
            int day = int.Parse(nationalId.Substring(5, 2));

            int fullYear = centuryDigit == 2
                ? 1900 + year
                : 2000 + year;

            if (!DateTime.TryParse(
                    $"{fullYear:0000}-{month:00}-{day:00}",
                    out _))
            {
                return false;
            }

            // 4. Governorate code
            string governorateCode = nationalId.Substring(7, 2);

            if (!ValidGovernorateCodes.Contains(governorateCode))
                return false;

            // 5. Check digit
            return ValidateCheckDigit(nationalId);
        }
        private bool ValidateCheckDigit(string nationalId)
        {
            // TODO: implement the Egyptian checksum algorithm
            return true;
        }
    }
}
