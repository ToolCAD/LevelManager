using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using LevelManager.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;

namespace LevelManager.Api
{
    public class LevelApiController
    {
        private readonly Document _Document;

        public LevelApiController(Document document)
        {
            _Document = document;
        }

        public List<LevelModel> GetAll()
        {
            var result = new List<LevelModel>();

            // Retrieve all levels from the document
            var collector = new FilteredElementCollector(_Document);
            var levels = collector
                .OfCategory(BuiltInCategory.OST_Levels)
                .WhereElementIsNotElementType()
                .ToElements();

            if (levels.Count <= 0)
            {
                return result;
            }

            foreach (var level in levels)
            {

                var revitLevel = level as Level;
                if (revitLevel != null)
                {
                    BasePointType basePointType = GetLevelBasePointType(level);

                    result.Add(new LevelModel(revitLevel.Name, new Elevation(revitLevel.Elevation), basePointType));

                }
            }

            return result;
        }

        public void Create(LevelModel input)
        {
            // Begin a transaction
            using (Transaction transaction = new Transaction(_Document, "Create Level"))
            {
                transaction.Start();

                try
                {
                    // Create a new level at the specified elevation
                    Level newLevel = Level.Create(_Document, input.Elevation.Value);
                    newLevel.Name = input.Name;

                    // Set the LEVEL_RELATIVE_BASE_TYPE parameter
                    Parameter baseTypeParameter = newLevel.get_Parameter(BuiltInParameter.LEVEL_RELATIVE_BASE_TYPE);
                    if (baseTypeParameter != null)
                    {
                        int baseTypeValue = (int)input.BasePointType;
                        baseTypeParameter.Set(baseTypeValue);
                    }

                    // Commit the transaction
                    transaction.Commit();

                }
                catch (Exception ex)
                {
                    // Handle any exceptions that may occur during the creation
                    // You can log the exception or handle it as needed
                    // Rollback the transaction in case of failure
                    transaction.RollBack();

                    Debug.WriteLine($"Error creating new level: {ex.Message}");
                }
            }

        }

        public void Delete(string levelName)
        {
            Level levelToDelete = FindByName(levelName);

            if (levelToDelete == null)
            {
                MessageBox.Show($"Level not found: {levelName}");
                return;
            }

            using (Transaction transaction = new Transaction(_Document, "Delete Level"))
            {
                transaction.Start();

                try
                {
                    _Document.Delete(levelToDelete.Id);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.RollBack();
                    TaskDialog.Show("Invalid Operation", $"Could not delete level {levelName}");
                    Debug.WriteLine(ex.Message);
                }
            }
        }

        private Level FindByName(string levelName)
        {
            // Retrieve all levels in the document
            var collector = new FilteredElementCollector(_Document);
            var levels = collector.OfClass(typeof(Level)).ToElements();

            // Find the level with the specified name
            Level levelToDelete = null;
            foreach (Element elem in levels)
            {
                var level = elem as Level;
                if (level != null && level.Name.Equals(levelName))
                {
                    levelToDelete = level;
                    break;
                }
            }

            return levelToDelete;
        }

        private BasePointType GetLevelBasePointType(Element level)
        {
            // Get TypeId of the element
            ElementId typeId = level.GetTypeId();

            // Retrieve the Element corresponding to the TypeId
            Element elementType = _Document.GetElement(typeId);

            // Accessing Type Name
            string typeName = elementType.Name;

            Parameter baseTypeParameter = elementType.get_Parameter(BuiltInParameter.LEVEL_RELATIVE_BASE_TYPE);

            BasePointType basePointType = BasePointType.Project;
            if (baseTypeParameter != null)
            {
                int baseTypeValue = baseTypeParameter.AsInteger();
                basePointType = baseTypeValue == 0 ? BasePointType.Project : BasePointType.Shared;
            }

            return basePointType;
        }
    }
}
