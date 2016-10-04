namespace WeatherDataHelper
{
    public class Location
    {
        #region Data_Members

        private string _strCity;
        private string _strCountry;

        #endregion

        #region Getters&Setters

        public string City
        {
            get
            {
                return _strCity;
            }

            set
            {
                _strCity = value;
            }
        }

        public string Country
        {
            get
            {
                return _strCountry;
            }

            set
            {
                _strCountry = value;
            }
        }
        #endregion

        #region Ctors

        public Location(string strCity, string strCountry)
        {
            _strCity = strCity;
            _strCountry = strCountry;
        }

        #endregion
    }
}