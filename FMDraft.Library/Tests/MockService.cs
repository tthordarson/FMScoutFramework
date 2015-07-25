using FMDraft.Common.Helpers;
using FMDraft.Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMDraft.Library.Tests
{
    internal class MockService
    {
        private string[] nations;
        private PairList<string, string> nationCity;
        //private List<Tuple<string, string>> nationCity;

        public MockService()
        {
            InitializeNations();
            InitializeCities();
        }

        private void InitializeNations()
        {
            this.nations = new string[] { "Argentina", "England", "Brazil", "Canada", "Iceland", "Denmark", "USA" };
        }

        private void InitializeCities()
        {
            nationCity = new PairList<string, string>();

            // Argentina
            nationCity.Add("Argentina", "Boca");
            nationCity.Add("Argentina", "Buenos Aires");

            // England
            nationCity.Add("England", "London");
            nationCity.Add("England", "Birmingham");
            nationCity.Add("England", "Liverpool");
            nationCity.Add("England", "Manchester");

            // Brazil
            nationCity.Add("Brazil", "Rio de Janeiro");
            nationCity.Add("Brazil", "Sao Paulo");

            // Canada
            nationCity.Add("Canada", "Toronto");
            nationCity.Add("Canada", "Montreal");
            nationCity.Add("Canada", "Vancouver");
            nationCity.Add("Canada", "Calgary");
            nationCity.Add("Canada", "Winnipeg");
            nationCity.Add("Canada", "Ottawa");
            nationCity.Add("Canada", "Halifax");
            nationCity.Add("Canada", "Quebec");
            nationCity.Add("Canada", "Thunder Bay");
            nationCity.Add("Canada", "Regina");

            // Iceland
            nationCity.Add("Iceland", "Reykjavik");
            nationCity.Add("Iceland", "Kopavogur");

            // Denmark
            nationCity.Add("Denmark", "Copenhagen");

            // USA
            nationCity.Add("USA", "New York");
            nationCity.Add("USA", "Boston");
            nationCity.Add("USA", "Los Angeles");
        }

        public IEnumerable<Nation> GetNations()
        {
            return this.nations.Select(x => { return new Nation() { Name = x }; }).OrderBy(x => x.Name);
        }

        public IEnumerable<City> GetCities(Nation nation)
        {
            var cityStrings = nationCity.Contents.Where(x => x.Item1 == nation.Name).Select(x => x.Item2);

            return cityStrings.Select(x =>
            {
                return new City()
                {
                    Name = x
                };
            });
        }
    }
}
