using Models.Entities;
using Models.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ViewModels;

namespace Services
{
    public class LeafletService : ILeafletService
    {
        private readonly IUserRepository _userRepository;

        public LeafletService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public LeafletViewModel GetLocation()
        {
            var me = GetCurentUser();
            if (!(me.GeoLocation is null))
            {
                var result = new LeafletViewModel()
                {
                    Lat = (double)me.GeoLocation.Latitude,
                    Lng = (double)me.GeoLocation.Longitude,
                    Address = me.Location
                };
                return result;
            }
            return null;
        }
        public bool Edit(LeafletViewModel leaflet)
        {
            var me = GetCurentUser();
            string strPointWellKnownText = string.Format(CultureInfo.InvariantCulture.NumberFormat, "POINT({0} {1})", leaflet.Lng, leaflet.Lat);
            DbGeography oDbGeography =
                DbGeography.PointFromText
                (pointWellKnownText: strPointWellKnownText, coordinateSystemId: 4326);
            me.GeoLocation = oDbGeography;
            me.Location = leaflet.Address;
            return _userRepository.Update(me);
        }
        private User GetCurentUser()
        {
            var me = _userRepository.Where(p => p.Username == HttpContext.Current.User.Identity.Name).Single();
            return me;
        }



    }
}

