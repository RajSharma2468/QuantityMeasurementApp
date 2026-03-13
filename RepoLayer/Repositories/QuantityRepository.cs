using ModelLayer.Models;
using RepoLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;  // Add this using

namespace RepoLayer.Repositories
{
    public class QuantityRepository : IQuantityRepository
    {
        private static readonly object _lock = new object();
        private static QuantityRepository _instance;
        private readonly List<QuantityMeasurementEntity> _measurements;
        private readonly string _filePath = "quantity_measurements.json";  // Changed to .json

        private QuantityRepository()
        {
            _measurements = new List<QuantityMeasurementEntity>();
            LoadFromDisk();
        }

        public static QuantityRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new QuantityRepository();
                        }
                    }
                }
                return _instance;
            }
        }

        public void Save(QuantityMeasurementEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _measurements.Add(entity);
            SaveToDisk();
        }

        public List<QuantityMeasurementEntity> GetAll()
        {
            return new List<QuantityMeasurementEntity>(_measurements);
        }

        public void SaveToDisk()
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                string jsonString = JsonSerializer.Serialize(_measurements, options);
                File.WriteAllText(_filePath, jsonString);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving to disk: {ex.Message}");
            }
        }

        public void LoadFromDisk()
        {
            try
            {
                if (File.Exists(_filePath))
                {
                    string jsonString = File.ReadAllText(_filePath);
                    var loaded = JsonSerializer.Deserialize<List<QuantityMeasurementEntity>>(jsonString);
                    if (loaded != null)
                    {
                        _measurements.Clear();
                        _measurements.AddRange(loaded);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading from disk: {ex.Message}");
            }
        }

        public void Clear()
        {
            _measurements.Clear();
            if (File.Exists(_filePath))
            {
                File.Delete(_filePath);
            }
        }
    }
}