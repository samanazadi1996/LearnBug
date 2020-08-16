using Models.Entities;
using ViewModels;

namespace Services
{
    public interface ILeafletService
    {
        bool Edit(LeafletViewModel leaflet);
        LeafletViewModel GetLocation();
    }
}