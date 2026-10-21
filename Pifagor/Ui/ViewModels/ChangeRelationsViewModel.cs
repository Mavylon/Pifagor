using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace Pifagor.Ui.ViewModels
{
    public partial class ChangeRelationsViewModel : ObservableObject
    {

        [ObservableProperty]
        private int _from;
        [ObservableProperty]
        private int _to;
        [ObservableProperty]
        private int _work;
        public ObservableCollection<int> NodesList { get; set; } = new ObservableCollection<int>();
        public ChangeRelationsViewModel(int NodeCount)
        {
            for(int i = 0; i < NodeCount; i++)
            {
                NodesList.Add(i);
            }
        }

        public override bool Equals(object? obj)
        {
            if(obj is ChangeRelationsViewModel other)
            {
                return this.From == other.From && this.To == other.To;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.From, this.To);
        }
    }
}
