using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public static class Day4
    {
        public static void Problem1()
        {
            var input = Utilities.ReadStringArray(4);
            int reqFields = 0;
            int valid = 0;
            foreach(var line in input)
            {
                if (string.IsNullOrEmpty(line))
                {
                    reqFields = 0;
                }
                else
                {
                    reqFields += line.Replace("cid:", "").Count(c => c == ':');
                    if (reqFields == 7)
                    {
                        valid++;
                        reqFields = 0;
                    }
                }
            }
            Console.WriteLine(valid);
        }

        public static void Problem2()
        {
            var input = Utilities.ReadStringArray(4);
            int valid = 0;
            Passport passport = new Passport();
            foreach (var line in input)
            {
                if (string.IsNullOrEmpty(line))
                {
                    passport = new Passport();
                }
                else
                {
                    var properties = line.Split(' ');
                    foreach (var prop in properties)
                    {
                        passport.AddProperty(prop);
                    }
                    if (passport.IsValid)
                    {
                        valid++;
                        passport = new Passport();
                    }
                }
            }
            Console.WriteLine(valid);
        }
    }

    public class Passport
    {
        public string BirthYear { get; set; }
        public string IssueYear { get; set; }
        public string ExpirationYear { get; set; }
        public string Height { get; set; }
        public string HairColor { get; set; }
        public string EyeColor { get; set; }
        public string PassportId { get; set; }

        public void AddProperty(string property)
        {
            var key = property.Split(':')[0];
            var value = property.Split(':')[1];
            switch (key)
            {
                case "byr":
                    BirthYear = value;
                    break;
                case "iyr":
                    IssueYear = value;
                    break;
                case "eyr":
                    ExpirationYear = value;
                    break;
                case "hgt":
                    Height = value;
                    break;
                case "hcl":
                    HairColor = value;
                    break;
                case "ecl":
                    EyeColor = value;
                    break;
                case "pid":
                    PassportId = value;
                    break;
            }
        }

        public bool IsValid
        {
            get
            {
                string[] eyeColors = { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
                var hcPattern = new Regex(@"^#([a-f0-9]{6})$");
                var pidPattern = new Regex(@"^([0-9]{9})$");
                return (BirthYear != null && BirthYear.Length == 4 && Int32.Parse(BirthYear) >= 1920 && Int32.Parse(BirthYear) <= 2002)
                    && (IssueYear != null && IssueYear.Length == 4 && Int32.Parse(IssueYear) >= 2010 && Int32.Parse(IssueYear) <= 2020)
                    && (ExpirationYear != null && ExpirationYear.Length == 4 && Int32.Parse(ExpirationYear) >= 2020 && Int32.Parse(ExpirationYear) <= 2030)
                    && (Height != null && ((Height.EndsWith("cm") && Int32.Parse(Height.Replace("cm","")) >= 150 && Int32.Parse(Height.Replace("cm", "")) <= 193)
                        || (Height.EndsWith("in") && Int32.Parse(Height.Replace("in", "")) >= 59 && Int32.Parse(Height.Replace("in", "")) <= 76)))
                    && (EyeColor != null && eyeColors.Contains(EyeColor))
                    && (HairColor != null && hcPattern.IsMatch(HairColor))
                    && (PassportId != null && pidPattern.IsMatch(PassportId));
            }
        }
    }
}
