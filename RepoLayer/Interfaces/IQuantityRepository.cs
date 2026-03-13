using ModelLayer.Models;
using System.Collections.Generic;

namespace RepoLayer.Interfaces
{
    public interface IQuantityRepository
    {
        /// <summary>
        /// Saves a measurement entity to the repository
        /// </summary>
        void Save(QuantityMeasurementEntity entity);

        /// <summary>
        /// Gets all measurement entities from the repository
        /// </summary>
        List<QuantityMeasurementEntity> GetAll();

        /// <summary>
        /// Saves the repository data to disk
        /// </summary>
        void SaveToDisk();

        /// <summary>
        /// Loads the repository data from disk
        /// </summary>
        void LoadFromDisk();

        /// <summary>
        /// Clears all data from the repository
        /// </summary>
        void Clear();
    }
}