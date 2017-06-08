﻿using System;
using Android.Runtime;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.FullFragging.Fragments.EventSource;
using MvvmCross.Droid.Shared.Fragments;

namespace MvvmCross.Droid.FullFragging.Fragments
{
    [Register("mvvmcross.droid.fullfragging.fragments.MvxPreferenceFragment")]
    public abstract class MvxPreferenceFragment : MvxEventSourcePreferenceFragment, IMvxFragmentView
    {
        protected MvxPreferenceFragment()
        {
            this.AddEventListeners();
        }

        protected MvxPreferenceFragment(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {}

        public IMvxBindingContext BindingContext { get; set; }

        private object _dataContext;

        public object DataContext
        {
            get { return _dataContext; }
            set
            {
                _dataContext = value;
                if (BindingContext != null)
                    BindingContext.DataContext = value;
            }
        }

        public virtual IMvxViewModel ViewModel
        {
            get { return DataContext as IMvxViewModel; }
            set
            {
                DataContext = value;
                OnViewModelSet();
            }
        }

        public virtual void OnViewModelSet()
        {
        }

        public string UniqueImmutableCacheTag => Tag;

        public override void OnDestroy ()
        {
            base.OnDestroy ();
            ViewModel?.Destroy ();
        }

        public override void OnStart()
        {
            base.OnStart();
            ViewModel?.Appearing();
        }

        public override void OnResume()
        {
            base.OnResume();
            ViewModel?.Appeared();
        }

        public override void OnPause()
        {
            base.OnPause();
            ViewModel?.Disappearing();
        }

        public override void OnStop()
        {
            base.OnStop();
            ViewModel?.Disappeared();
        }
    }

    public abstract class MvxPreferenceFragment<TViewModel>
        : MvxPreferenceFragment
        , IMvxFragmentView<TViewModel> where TViewModel : class, IMvxViewModel
    {

        protected MvxPreferenceFragment()
        {

        }

        protected MvxPreferenceFragment(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer) { }


        public new TViewModel ViewModel
        {
            get { return (TViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }
    }
}