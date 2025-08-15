using System.Collections.Generic;

namespace DevExpressBindingDialog;

/// <summary>
///    Represents a coalition consisting of multiple countries.
/// </summary>
public static class Coalition
{
   /// <summary>
   ///    Represents a static, pre-defined list of countries collaborating within a coalition.
   /// </summary>
   public static readonly List<Country> Collaborators = new()
   {
      new Country { Id = 1, Name = "Germany" },
      new Country { Id = 2, Name = "USA" },
      new Country { Id = 3, Name = "Japan" },
      new Country { Id = 4, Name = "Ukraine" },
      new Country { Id = 5, Name = "France" },
      new Country { Id = 6, Name = "Sweden" },
      new Country { Id = 7, Name = "United Kingdom" },
      new Country { Id = 8, Name = "Canada" },
      new Country { Id = 9, Name = "Italy" },
      new Country { Id = 10, Name = "Poland" },
      new Country { Id = 11, Name = "Spain" },
      new Country { Id = 12, Name = "Netherlands" },
      new Country { Id = 13, Name = "Denmark" },
      new Country { Id = 14, Name = "Norway" },
      new Country { Id = 15, Name = "Finland" },
      new Country { Id = 16, Name = "Portugal" },
      new Country { Id = 17, Name = "Belgium" },
      new Country { Id = 18, Name = "Czech Republic" },
      new Country { Id = 19, Name = "Slovakia" },
      new Country { Id = 20, Name = "Estonia" },
      new Country { Id = 21, Name = "Latvia" },
      new Country { Id = 22, Name = "Lithuania" },
      new Country { Id = 23, Name = "Romania" },
      new Country { Id = 24, Name = "Bulgaria" },
      new Country { Id = 25, Name = "Greece" },
      new Country { Id = 26, Name = "Hungary" },
      new Country { Id = 27, Name = "Luxembourg" },
      new Country { Id = 28, Name = "Ireland" },
      new Country { Id = 29, Name = "Iceland" },
      new Country { Id = 30, Name = "Australia" },
      new Country { Id = 31, Name = "New Zealand" },
      new Country { Id = 32, Name = "South Korea" },
      new Country { Id = 33, Name = "Switzerland" },
      new Country { Id = 34, Name = "Austria" },
      new Country { Id = 35, Name = "Georgia" },
      new Country { Id = 36, Name = "Moldova" },
      new Country { Id = 37, Name = "Slovenia" },
      new Country { Id = 38, Name = "Croatia" },
      new Country { Id = 39, Name = "Montenegro" },
      new Country { Id = 40, Name = "North Macedonia" },
      new Country { Id = 41, Name = "Albania" }
   };
}