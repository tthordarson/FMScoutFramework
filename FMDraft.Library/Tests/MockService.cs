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
            nationCity.Add("Canada", "Scarborough");
            nationCity.Add("Canada", "Mississauga");
            nationCity.Add("Canada", "Edmonton");
            nationCity.Add("Canada", "Hamilton");
            nationCity.Add("Canada", "Burlington");
            nationCity.Add("Canada", "London");
            nationCity.Add("Canada", "Niagara Falls");
            nationCity.Add("Canada", "Windsor");

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
            })
            .OrderBy(x => x.Name);
        }

        public IEnumerable<Manager> GetManagers()
        {
            var list = new List<Manager>();

            list.Add(new Manager() { FullName = "Rafa Benitez", ID = 4203246 });
            list.Add(new Manager() { FullName = "José Mourinho", ID = 4211801 });
            list.Add(new Manager() { FullName = "Jürgen Klopp", ID = 9318 });
            list.Add(new Manager() { FullName = "Frank de Boer", ID = 354 });
            list.Add(new Manager() { FullName = "Josep Guardiola", ID = 1120 });
            list.Add(new Manager() { FullName = "Brendan Rodgers", ID = 5112744 });
            list.Add(new Manager() { FullName = "Arsène Wenger", ID = 2020187 });
            list.Add(new Manager() { FullName = "Vladimir Weiss", ID = 2016378 });
            list.Add(new Manager() { FullName = "Pako Ayestarán", ID = 7449882 });
            list.Add(new Manager() { FullName = "André Villas-Boas", ID = 745487 });
            list.Add(new Manager() { FullName = "Bruce Arena", ID = 1200621 });
            list.Add(new Manager() { FullName = "Lucien Favre", ID = 69000919 });
            list.Add(new Manager() { FullName = "Louis van Gaal", ID = 4200588 });
            list.Add(new Manager() { FullName = "David Moyes", ID = 2004988 });
            list.Add(new Manager() { FullName = "Michael Laudrup", ID = 601710 });
            list.Add(new Manager() { FullName = "Roberto Mancini", ID = 12523 });
            list.Add(new Manager() { FullName = "Carlo Ancelotti", ID = 20178 });
            list.Add(new Manager() { FullName = "Unai Emery", ID = 5193 });
            list.Add(new Manager() { FullName = "Ernesto Valvarede", ID = 9338 });
            list.Add(new Manager() { FullName = "Rúnar Páll Sigmundsson", ID = 39004216 });

            return list;
        }
     }
}
