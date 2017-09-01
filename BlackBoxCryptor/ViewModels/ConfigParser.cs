using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BlackBoxCryptor.ViewModels
{
    public class ConfigParser : IDisposable
    {
        #region Local Variables
        private const string FILE_PATH = "config.json";
        private static List<Setting> _appSettings = new List<Setting>();
        private static FileStream _configFileStream;

        public IList<Setting> AppSettings { get => _appSettings; }
        public FileStream ConfigFileStream { get => _configFileStream; }
        #endregion



        /// <summary>
        /// Constructor
        /// </summary>
        public ConfigParser()
        {
            
        }

        //open config file
        public bool OpenConfig()
        {
            try
            {
                //check if it exists
                bool fileExists = File.Exists(FILE_PATH);

                //open file
                if (fileExists)
                    _configFileStream = File.Open(FILE_PATH, FileMode.Open);
                else
                {
                    //otherwise create and open
                    _configFileStream = File.Create(FILE_PATH);


                    Dispose();

                    //initialize data with schema
                    InitializeFileData();
                }

                //validate
                if (_configFileStream != null)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void InitializeFileData()
        {
            try
            {
                _configFileStream = File.Open(FILE_PATH, FileMode.Open);


                _appSettings = new List<Setting>();
                _appSettings.Add(new Setting() { key = "cryptographic_key", value = "#Shannon123" });


                AppSettings appSettings = new AppSettings()
                {
                    appSettings = _appSettings.ToArray()
                };

                string json = JsonConvert.SerializeObject(appSettings);

                using (StreamWriter writer = new StreamWriter(_configFileStream))
                {
                    writer.Write(json);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //read config file or its respective file stream
        public bool ReadConfig()
        {
            try
            {
                if (_configFileStream == null)
                {
                    bool openResults = OpenConfig();

                    if (!openResults)
                        return ReadConfig();
                }

                //make stream readable
                Dispose();
                _configFileStream = File.Open(FILE_PATH, FileMode.Open);

                using (StreamReader reader = new StreamReader(_configFileStream))
                {

                    string content = reader.ReadToEnd();

                    AppSettings settings = JsonConvert.DeserializeObject<AppSettings>(content);


                    if (_appSettings == null)
                        _appSettings = new List<Setting>();
                    else
                        _appSettings = settings.appSettings.ToList();
                }


                if (AppSettings != null)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //change a setting in config file
        public bool ChangeSetting(string settingName,string value)
        {
            try
            {
                //check setting
                var item = _appSettings.FirstOrDefault(x => x.key == settingName);

                if (item == null)
                    return false;

                //remove item
                _appSettings.Remove(item);

                //change value
                item.value = value;
                _appSettings.Add(item);

                //empty file
                _configFileStream = File.OpenWrite(FILE_PATH);

                _configFileStream.SetLength(0);
                _configFileStream.Flush();
                _configFileStream.Dispose();

                //write file
                AppSettings appSettings = new AppSettings();
                appSettings.appSettings = _appSettings.ToArray();

                _configFileStream = File.OpenWrite(FILE_PATH);

                using (StreamWriter writer = new StreamWriter(_configFileStream))
                {
                    string json = JsonConvert.SerializeObject(appSettings);

                    writer.Write(json);
                }


                //return state of stream to reading mode
                _configFileStream = File.Open(FILE_PATH, FileMode.Open);

                return ReadConfig();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        //get specific app setting value from config file

        public string GetAppSetting(string settingName)
        {
            var item = AppSettings.FirstOrDefault(x => x.key == settingName);

            if (item != null)
                return item.value;

            return "";
        }

        public void Dispose()
        {
            _configFileStream.Dispose();

            _configFileStream = null;
        }
    }
}
