using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;

namespace StreetFoo.Client
{
    // base class for view-model implemenations. 
    public abstract class ViewModel : ModelItem, IViewModel
    {
        // somewhere to hold the host...
        protected IViewModelHost Host { get; private set; }

        // holds an overriden caption...
        private string _caption;

        // support field for IsBusy flag...
        private int BusyCount { get; set; }

        public ViewModel()
        {
        }

        public virtual void Initialize(IViewModelHost host)
        {
            this.Host = host;
        }

        // indicates whether the view model is busy...
        public bool IsBusy
        {
            get { return GetValue<bool>(); }
            private set { SetValue(value); }
        }

        public IDisposable EnterBusy()
        {
            this.BusyCount++;

            // trigger a UI change?
            if (this.BusyCount == 1)
                this.IsBusy = true;

            // return an object we can use to roll this back...
            return new BusyExiter(this);
        }

        private class BusyExiter : IDisposable
        {
            private ViewModel Owner { get; set; }

            internal BusyExiter(ViewModel owner)
            {
                this.Owner = owner;
            }

            public void Dispose()
            {
                this.Owner.ExitBusy();
            }
        }

        public void ExitBusy()
        {
            this.BusyCount--;

            // trigger a UI change?
            if (this.BusyCount == 0)
                this.IsBusy = false;
        }

        // called when the view is activated...
        public virtual void Activated(object args = null)
        {
            this.BusyCount = 0;
            this.IsBusy = false;
        }

        // called when the view-model might have some data to share...
        public virtual void ShareDataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            // no-op by default...
        }

        public string Caption
        {
            get
            {
                if (!(string.IsNullOrEmpty(_caption)))
                    return _caption;
                else
                    return this.ToString();
            }
            set
            {
                _caption = value;
            }
        }
    }
}
