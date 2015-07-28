using FMDraft.Library;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMDraft.WPF.Templates.Drafts
{
    public class DraftCardViewModel : AbstractViewModel
    {
        public DraftCardViewModel(GameCore core) : base(core)
        {

        }

        private int _PickNumber;

        public int PickNumber
        {
            get { return _PickNumber; }
            set
            {
                _PickNumber = value;
                NotifyPropertyChanged("PickNumber");
                NotifyPropertyChanged("PickNumberString");
            }
        }

        public string PickNumberString
        {
            get
            {
                return string.Format("Pick #{0}", PickNumber);
            }
        }

        private int _WeeklySalary;

        public int WeeklySalary
        {
            get { return _WeeklySalary; }
            set
            {
                _WeeklySalary = value;
                NotifyPropertyChanged("WeeklySalary");
                NotifyPropertyChanged("WeeklySalaryString");
            }
        }

        public String WeeklySalaryString
        {
            get
            {
                var culture = CultureInfo.GetCultureInfo("en-GB");
                return WeeklySalary.ToString("C", culture);
            }
        }

        private int _ContractLength;

        public int ContractLength
        {
            get { return _ContractLength; }
            set
            {
                _ContractLength = value;
                NotifyPropertyChanged("ContractLength");
            }
        }

        private int _MinimumAbility;

        public int MinimumAbility
        {
            get { return _MinimumAbility; }
            set
            {
                _MinimumAbility = value;
                NotifyPropertyChanged("MinimumAbility");
            }
        }

        private int _MaximumAbility;

        public int MaximumAbility
        {
            get { return _MaximumAbility; }
            set
            {
                _MaximumAbility = value;
                NotifyPropertyChanged("MaximumAbility");
            }
        }

    }
}
