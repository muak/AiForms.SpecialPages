using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Services;

namespace Sample.ViewModels
{
	public class MainPageViewModel : BindableBase, INavigationAware
	{
		private string _title;
		public string Title {
			get { return _title; }
			set { SetProperty(ref _title, value); }
		}

		private DelegateCommand _LeftIconCommand;
		public DelegateCommand LeftIconCommand {
			get {
				return _LeftIconCommand = _LeftIconCommand ?? new DelegateCommand(async () => {
					await _pageDlg.DisplayAlertAsync("", "LeftIcon Tap", "OK");
				});
			}
		}

		private DelegateCommand _NextCommand;
		public DelegateCommand NextCommand {
			get {
				return _NextCommand = _NextCommand ?? new DelegateCommand(() => {
					_navi.NavigateAsync("ModalNavi/NextPage", null, true);
				});
			}
		}

		private DelegateCommand _MoveCommand;
		public DelegateCommand MoveCommand {
			get { return _MoveCommand = _MoveCommand ?? new DelegateCommand(() => {
				_navi.NavigateAsync("SecondPage");
			}); }
		}

		INavigationService _navi;
		IPageDialogService _pageDlg;
		public MainPageViewModel(INavigationService navigationService, IPageDialogService pageDlg)
		{
			_navi = navigationService;
			_pageDlg = pageDlg;
		}

		public void OnNavigatedFrom(NavigationParameters parameters)
		{

		}

		public void OnNavigatedTo(NavigationParameters parameters)
		{
			if (parameters.ContainsKey("title"))
				Title = (string)parameters["title"] + " and Prism";
		}

		public void OnNavigatingTo(NavigationParameters parameters)
		{
		}
	}
}

