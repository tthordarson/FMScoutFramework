﻿using FMDraft.Library;
using FMDraft.Library.Entities;
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

        private int _RoundNumber;

        public int RoundNumber
        {
            get { return _RoundNumber; }
            set
            {
                _RoundNumber = value;
                NotifyPropertyChanged("PickNumber");
                NotifyPropertyChanged("PickNumberString");
            }
        }

        public string RoundNumberString
        {
            get
            {
                return string.Format("Round #{0} Pick", RoundNumber);
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

        private int? _MinimumAbility;

        public int? MinimumAbility
        {
            get { return _MinimumAbility; }
            set
            {
                _MinimumAbility = value;
                NotifyPropertyChanged("MinimumAbility");
            }
        }

        private int? _MaximumAbility;

        public int? MaximumAbility
        {
            get { return _MaximumAbility; }
            set
            {
                _MaximumAbility = value;
                NotifyPropertyChanged("MaximumAbility");
            }
        }

        private int? _MinimumAge;

        public int? MinimumAge
        {
            get { return _MinimumAge; }
            set
            {
                NotifyPropertyChanged("MinAge");
                _MinimumAge = value;
            }
        }

        private int? _MaximumAge;

        public int? MaximumAge
        {
            get { return _MaximumAge; }
            set
            { 
                _MaximumAge = value;
                NotifyPropertyChanged("MaxAge");
            }
        }
    }

    public static class DraftCardViewModelExtensions
    {
        public static DraftCardViewModel ToViewModel(this DraftCard draftCard, GameCore core)
        {
            return new DraftCardViewModel(core)
            {
                RoundNumber = draftCard.Round,
                ContractLength = draftCard.ContractYears,
                MaximumAbility = draftCard.MaxCurrentAbility,
                WeeklySalary = draftCard.ContractSalary
            };
        }

        public static DraftCard ToData(this DraftCardViewModel viewModel)
        {
            return new DraftCard()
            {
                ContractSalary = viewModel.WeeklySalary,
                ContractYears = viewModel.ContractLength,
                Round = viewModel.RoundNumber,
                MaxCurrentAbility = viewModel.MaximumAbility
            };
        }
    }
}