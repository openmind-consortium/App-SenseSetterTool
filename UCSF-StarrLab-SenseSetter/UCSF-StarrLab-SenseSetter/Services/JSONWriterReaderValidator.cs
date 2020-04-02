using Caliburn.Micro;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UCSF_StarrLab_SenseSetter.Models;

namespace UCSF_StarrLab_SenseSetter.Services
{
    class JSONWriterReaderValidator
    {
        #region Variables and Constructors
        //deviceIDForPath, patientIDForPath, projectIDForPath are used when writing files. Needed for path to Medtronic directory
        //filepath is the path to the medtronic json directory found in GetDirectoryPathForCurrentSession()
        private string filepath;
        private ILog _log;

        /// <summary>
        /// Constructor for not writing files
        /// </summary>
        /// <param name="_log">Caliburn Micro Logger</param>
        public JSONWriterReaderValidator(ILog _log)
        {
            this._log = _log;
        }

        #endregion

        /// <summary>
        /// Validates that the json matches the schema provided
        /// </summary>
        /// <param name="jsonToValidate">json string from the config file</param>
        /// <param name="schema">schema text from SchemaModel.cs that matches the appropriate jsonToValidate string structure</param>
        /// <returns>true is valid and false if not</returns>
        public bool ValidateJSON(string jsonToValidate, string schema)
        {
            //Set to false as default
            bool isValid = false;
            //Messages for anything wrong with json validation. Not used 
            IList<string> messages;
            try
            {
                JSchema jsonSchema = JSchema.Parse(schema);
                Newtonsoft.Json.Linq.JObject person = JObject.Parse(jsonToValidate);
                //Only set to true if json is valid
                isValid = person.IsValid(jsonSchema, out messages);
                if (!isValid)
                {
                    MessageBox.Show("Error messages after failing to validate json file: " + String.Join(",", messages));
                }
            }
            catch (Exception e)
            {
                _log.Error(e);
            }
            return isValid;
        }

        #region Get Sense, Adaptive and Report, Montage and Stim sweep Config files
        /// <summary>
        /// Gets the file from the filepath, validates the file, and converts it to the sense model
        /// </summary>
        /// <param name="filePath">File path for the sense_config.json file to be used to convert</param>
        /// <returns>SenseModel if successful or null if unsuccessful</returns>
        public SenseModel GetSenseModelFromFile(string filePath)
        {
            SenseModel model = null;
            string json = null;
            try
            {
                //read sense config file into string
                using (StreamReader sr = new StreamReader(filePath))
                {
                    json = sr.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("The sense config file could not be read from the file. Please check that it exists.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                _log.Error(e);
                return model;
            }
            if (string.IsNullOrEmpty(json))
            {
                MessageBox.Show("Sense JSON file is empty. Please check that the sense config is correct.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                _log.Warn("Sense JSON file is empty after loading file.");
                return model;
            }
            else
            {
                SchemaModel schemaModel = new SchemaModel();
                if (ValidateJSON(json, schemaModel.GetSenseSchema()))
                {
                    //if correct json format, write it into SenseModel
                    try
                    {
                        model = JsonConvert.DeserializeObject<SenseModel>(json);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Could not convert sense config file. Please be sure that sense config file is correct.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        _log.Error(e);
                        return model;
                    }
                }
                else
                {
                    MessageBox.Show("Could not validate sense config file. Please be sure that sense config file is correct.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    _log.Warn("Could not validate sense config file.");
                    return model;
                }
            }
            return model;
        }
        #endregion

        #region Write Sense Config Files
        /// <summary>
        /// Writes the Sense Model to a json config file in specified path
        /// </summary>
        /// <param name="senseModel">Model to be written to json file</param>
        /// <param name="path">path where the file goes including filename and extension</param>
        /// <returns>true if success and false if unsuccessful</returns>
        public bool WriteSenseConfigToFile(SenseModel senseModel, string path)
        {
            bool success = false;
            try
            {
                using (StreamWriter file = File.CreateText(path))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, senseModel);
                    success = true;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Could not write sense config to file at the path: " + path + ". Please save your current sense config file if you would like them saved for later use.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                _log.Error(e);
                success = false;
            }
            return success;
        }
        #endregion

        #region Helper Methods for Getting directory path, Checking if path exists or creating it

        /// <summary>
        /// Checks to see if the path already. If it does not, it will write the directory path for placing files into
        /// </summary>
        /// <param name="path">This is the final path where the files will be written and checked to make sure it is valid or else writes it</param>
        private void CheckIfDirectoryExistsOtherwiseCreateIt(string path)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(path);
                if (!fileInfo.Exists)
                    Directory.CreateDirectory(fileInfo.Directory.FullName);
            }
            catch (Exception e)
            {
                MessageBox.Show("Could not create directory for writing files. Please be sure the application_config.json BasePathToJSONFiles has the same path as DataDirectory in the Registry Editor for the path Computer\\HKEY_LOCAL_MACHINE\\SOFTWARE\\WOW6432Node\\Medtronic\\ORCA. Please fix and restart application", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                _log.Error(e);
            }
        }
        #endregion
    }
}
