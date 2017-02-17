using System.ComponentModel;

namespace UCWAUWP.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        #region Property

        private bool isLoading;
        public bool IsLoading
        {
            get { return isLoading; }
            set { if (isLoading == value) return; isLoading = value; NotifyPropertyChanged(); }
        }

        #endregion

        #region INotifyPropertyChanged 

        public event PropertyChangedEventHandler PropertyChanged;
        
        internal void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberNameAttribute] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
