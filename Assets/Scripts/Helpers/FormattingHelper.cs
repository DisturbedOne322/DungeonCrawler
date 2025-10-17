using Constants;

namespace Helpers
{
    public static class FormattingHelper
    {
        public static string FormatStat(int stat)
        {
            return stat + "/" + StatConstants.MaxStatPoints;
        }
    }
}