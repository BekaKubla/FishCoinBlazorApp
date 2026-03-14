using System.Text;

namespace FishCoinBlazorApp.Helpers
{
    public static class StringExtensions
    {
        public static string ToUrlSlug(this string value)
        {
            if (string.IsNullOrEmpty(value)) return "";

            var geoToLat = new Dictionary<char, string>
        {
            {'ა',"a"}, {'ბ',"b"}, {'გ',"g"}, {'დ',"d"}, {'ე',"e"}, {'ვ',"v"}, {'ზ',"z"}, {'თ',"t"}, {'ი',"i"}, {'კ',"k"},
            {'ლ',"l"}, {'მ',"m"}, {'ნ',"n"}, {'ო',"o"}, {'პ',"p"}, {'ჟ',"zh"}, {'რ',"r"}, {'ს',"s"}, {'ტ',"t"}, {'უ',"u"},
            {'ფ',"p"}, {'ქ',"k"}, {'ღ',"gh"}, {'ყ',"q"}, {'შ',"sh"}, {'ჩ',"ch"}, {'ც',"ts"}, {'ძ',"dz"}, {'წ',"ts"}, {'ჭ',"ch"},
            {'ხ',"kh"}, {'ჯ',"j"}, {'ჰ',"h"}
        };

            var str = value.ToLower();
            var result = new StringBuilder();

            foreach (var c in str)
            {
                if (geoToLat.ContainsKey(c)) result.Append(geoToLat[c]);
                else if (char.IsLetterOrDigit(c)) result.Append(c);
                else if (c == ' ') result.Append('-');
            }

            // ვასუფთავებთ ზედმეტ ტირეებს
            string slug = result.ToString();
            slug = System.Text.RegularExpressions.Regex.Replace(slug, @"-+", "-").Trim('-');

            return slug;
        }
    }
}
