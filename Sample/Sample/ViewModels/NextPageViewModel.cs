using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;

namespace Sample.ViewModels
{
	public class NextPageViewModel : BindableBase
	{
		private DelegateCommand _BackCommand;
		public DelegateCommand BackCommand {
			get { return _BackCommand = _BackCommand ?? new DelegateCommand(() => {
				_navi.GoBackAsync(null,true);
			}); }
		}

		INavigationService _navi;
		public NextPageViewModel(INavigationService navigationService)
		{
			_navi = navigationService;
		}
	}
}
