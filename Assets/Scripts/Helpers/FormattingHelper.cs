using Constants;
using Data;

namespace Helpers
{
    public class FormattingHelper
    {
        public static string FormatStat(int stat) => stat + "/" + StatConstants.MaxStatPoints;
    }
}