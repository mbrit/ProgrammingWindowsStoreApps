using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;

namespace StreetFoo.Client
{
    // base class for view-model implementations...
    public interface IViewModel : INotifyPropertyChanged
    {
        void Initialize(IViewModelHost host);

        // shared busy flag...
        bool IsBusy { get; }

        // called when the view is activated...
        void Activated();

        // called when Windows wants data from the view...
        void ShareDataRequested(DataTransferManager sender, DataRequestedEventArgs args);
    }
}
