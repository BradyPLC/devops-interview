using System;
using System.Collections.Generic;
using System.Text;

namespace BradyWeather.Common.Constants
{
    public class FeatureConstants
    {
        public const string Label = "BradyWeather";

        public static TimeSpan CacheExpiration = TimeSpan.FromSeconds(10);
    }
}
