using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BasicFrame.Services.Settings
{
    public class SettingsService : ISettingsService
    {
        #region Setting Constants
        private const string AccessToken = "access_token";
        private const string IdToken = "id_token";
        private const string IdUseMocks = "use_mocks";
        private const string IdIdentityBase = "url_base";
        private const string IdGatewayMarketingBase = "url_marketing";
        private const string IdGatewayShoppingBase = "url_shopping";
        private const string IdUseFakeLocation = "use_fake_location";
        private const string IdLatitude = "latitude";
        private const string IdLongitude = "longitude";
        private const string IdAllowGpsLocation = "allow_gps_location";
        private readonly string AccessTokenDefault = string.Empty;
        private readonly string IdTokenDefault = string.Empty;
        private readonly bool UseMocksDefault = true;
        private readonly bool UseFakeLocationDefault = false;
        private readonly bool AllowGpsLocationDefault = false;
        private readonly double FakeLatitudeDefault = 47.604610d;
        private readonly double FakeLongitudeDefault = -122.315752d;
        private readonly string UrlIdentityDefault = GlobalSetting.Instance.BaseIdentityEndpoint;
        private readonly string UrlGatewayMarketingDefault = GlobalSetting.Instance.BaseGatewayMarketingEndpoint;
        private readonly string UrlGatewayShoppingDefault = GlobalSetting.Instance.BaseGatewayShoppingEndpoint;

        // skan-keys
        private const string IdDashboardStartDate = "dashboard_start_date";
        private const string IdDashboardEndDate = "dashboard_end_date";
        private const string IdDebugMode = "debug_mode";
        // +new
        private const string IdEasyTapPay = "easy_tap_pay";
        private const string IdAutoPrintReceipt = "auto_print_receipt";
        private const string IdShowPhoto = "show_photo";
        private const string IdOfflineMode = "offline_mode";
        private const string IdEnableAnimation = "enable_animation";
        private const string IdTrainingMode = "training_mode";
        // skan-values
        private readonly bool DebugModeDefault = false;
        private readonly string DashboardStartDateDefault = DateTime.Now.ToString();
        private readonly string DashboardEndDateDefault = DateTime.Now.ToString();
        // + new
        private readonly bool EasyTapPayDefault = false;
        private readonly bool AutoPrintReceiptDefault = false; 
        private readonly bool ShowPhotoDefault = false;
        private readonly bool OfflineModeDefault = false;
        private readonly bool EnableAnimationDefault = false;
        private readonly bool TrainingModeDefault = false;
        #endregion

        #region Settings Properties
        public string AuthAccessToken
        {
            get => GetValueOrDefault(AccessToken, AccessTokenDefault);
            set => AddOrUpdateValue(AccessToken, value);
        }

        public string AuthIdToken
        {
            get => GetValueOrDefault(IdToken, IdTokenDefault);
            set => AddOrUpdateValue(IdToken, value);
        }

        public bool UseMocks
        {
            get => GetValueOrDefault(IdUseMocks, UseMocksDefault);
            set => AddOrUpdateValue(IdUseMocks, value);
        }

        public string IdentityEndpointBase
        {
            get => GetValueOrDefault(IdIdentityBase, UrlIdentityDefault);
            set => AddOrUpdateValue(IdIdentityBase, value);
        }

        public string GatewayShoppingEndpointBase
        {
            get => GetValueOrDefault(IdGatewayShoppingBase, UrlGatewayShoppingDefault);
            set => AddOrUpdateValue(IdGatewayShoppingBase, value);
        }

        public string GatewayMarketingEndpointBase
        {
            get => GetValueOrDefault(IdGatewayMarketingBase, UrlGatewayMarketingDefault);
            set => AddOrUpdateValue(IdGatewayMarketingBase, value);
        }

        public bool UseFakeLocation
        {
            get => GetValueOrDefault(IdUseFakeLocation, UseFakeLocationDefault);
            set => AddOrUpdateValue(IdUseFakeLocation, value);
        }

        public string Latitude
        {
            get => GetValueOrDefault(IdLatitude, FakeLatitudeDefault.ToString());
            set => AddOrUpdateValue(IdLatitude, value);
        }

        public string Longitude
        {
            get => GetValueOrDefault(IdLongitude, FakeLongitudeDefault.ToString());
            set => AddOrUpdateValue(IdLongitude, value);
        }

        public bool AllowGpsLocation
        {
            get => GetValueOrDefault(IdAllowGpsLocation, AllowGpsLocationDefault);
            set => AddOrUpdateValue(IdAllowGpsLocation, value);
        }

        //skan
        public string DashboardStartDate
        {
            get => GetValueOrDefault(IdDashboardStartDate, DashboardStartDateDefault);
            set => AddOrUpdateValue(IdDashboardStartDate, value);
        }
        public string DashboardEndDate
        {
            get => GetValueOrDefault(IdDashboardEndDate, DashboardEndDateDefault);
            set => AddOrUpdateValue(IdDashboardEndDate, value);
        }
        public bool DebugMode
        {
            get => GetValueOrDefault(IdDebugMode, DebugModeDefault);
            set => AddOrUpdateValue(IdDebugMode, value);
        }

        public bool EasyTapPay
        {
            get => GetValueOrDefault(IdEasyTapPay, EasyTapPayDefault);
            set => AddOrUpdateValue(IdEasyTapPay, value);
        }

        public bool AutoPrintReceipt
        {
            get => GetValueOrDefault(IdAutoPrintReceipt, AutoPrintReceiptDefault);
            set => AddOrUpdateValue(IdAutoPrintReceipt, value);
        }

        public bool ShowPhoto
        {
            get => GetValueOrDefault(IdShowPhoto, ShowPhotoDefault);
            set => AddOrUpdateValue(IdShowPhoto, value);
        }

        public bool OfflineMode
        {
            get => GetValueOrDefault(IdOfflineMode, OfflineModeDefault);
            set => AddOrUpdateValue(IdOfflineMode, value);
        }

        public bool EnableAnimation
        {
            get => GetValueOrDefault(IdEnableAnimation, EnableAnimationDefault);
            set => AddOrUpdateValue(IdEnableAnimation, value);
        }

        public bool TrainingMode
        {
            get => GetValueOrDefault(IdTrainingMode, TrainingModeDefault);
            set => AddOrUpdateValue(IdTrainingMode, value);
        }
        #endregion

        #region Public Methods

        public Task AddOrUpdateValue(string key, bool value) => AddOrUpdateValueInternal(key, value);
        public Task AddOrUpdateValue(string key, string value) => AddOrUpdateValueInternal(key, value);
        public bool GetValueOrDefault(string key, bool defaultValue) => GetValueOrDefaultInternal(key, defaultValue);
        public string GetValueOrDefault(string key, string defaultValue) => GetValueOrDefaultInternal(key, defaultValue);

        #endregion

        #region Internal Implementation

        async Task AddOrUpdateValueInternal<T>(string key, T value)
        {
            if (value == null)
            {
                await Remove(key);
            }

            Application.Current.Properties[key] = value;
            try
            {
                await Application.Current.SavePropertiesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to save: " + key, " Message: " + ex.Message);
            }
        }

        T GetValueOrDefaultInternal<T>(string key, T defaultValue = default(T))
        {
            object value = null;
            if (Application.Current.Properties.ContainsKey(key))
            {
                value = Application.Current.Properties[key];
            }
            return null != value ? (T)value : defaultValue;
        }

        async Task Remove(string key)
        {
            try
            {
                if (Application.Current.Properties[key] != null)
                {
                    Application.Current.Properties.Remove(key);
                    await Application.Current.SavePropertiesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to remove: " + key, " Message: " + ex.Message);
            }
        }
        #endregion
    }
}