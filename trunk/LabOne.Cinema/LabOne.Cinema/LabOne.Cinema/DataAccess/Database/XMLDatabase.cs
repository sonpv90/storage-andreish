﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Management.Instrumentation;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using LabOne.Cinema.Entities;

namespace LabOne.Cinema.DataAccess.Database
{
    public class XmlDataBase : DataBase
    {
        public XmlDataBase(string basePath, string fileExtension)
        {
            BasePath = basePath;
            FileExtension = fileExtension;
        }

        public override string WriteData<T>(T data)
        {
            string filename = GenerateFileName(((dynamic)data).ToArray().GetType());
            var serializer = new XmlSerializer(typeof(T));

            //XmlWriter writer = XmlWriter.Create(filename);
            using (TextWriter writer = new StreamWriter(filename))
            {
                serializer.Serialize(writer, data);
            }

            return filename;
        }

        public override IEnumerable<T> ReadFile<T>()
        {
            string filename = GenerateFileName(typeof(T));
            var readData = InternalRead<T>(filename);
            if (readData == null)
            {
                throw new FileNotFoundException(string.Format("The file on this path {0} was not found", filename));
            }
            return readData;
        }

        protected override IEnumerable<T> InternalRead<T>(string filename)
        {
            if (File.Exists(filename))
            {
                var list = new List<T>();
                var serializer = new XmlSerializer(typeof(T));

                using (XmlReader reader = XmlReader.Create(filename))
                {
                    while (reader.ReadToFollowing(typeof(T).Name))
                    {
                        list.Add((T)serializer.Deserialize(reader));
                    }

                    return list;
                }
            }
            return null;
        }
    }
}