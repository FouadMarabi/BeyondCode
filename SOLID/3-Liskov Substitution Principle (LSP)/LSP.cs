using System.Text;

namespace SOLID.Liskov_Substitution_Principle__LSP_
{
    namespace Problem
    {
        public class SqlFile
        {
            public string FilePath { get; set; }
            public string FileText { get; set; }
            public string LoadText()
            {
                return "";
            }
            public string SaveText()
            {
                return "";
            }
        }

        public class SqlFileManager
        {
            private List<SqlFile> _sqlFiles { get; set; }

            public SqlFileManager(List<SqlFile> sqlFiles)
            {
                _sqlFiles = sqlFiles;
            }

            public string GetTextFromFiles()
            {
                StringBuilder objStrBuilder = new StringBuilder();
                foreach (var objFile in _sqlFiles)
                {
                    objStrBuilder.Append(objFile.LoadText());
                }
                return objStrBuilder.ToString();
            }
            public void SaveTextIntoFiles()
            {
                foreach (var objFile in _sqlFiles)
                {
                    objFile.SaveText();
                }
            }
        }

        namespace NewRequirement_ReadOnlyFileCantSaveAndThrowException_BadSolution
        {
            public class ReadOnlySqlFile : SqlFile
            {

            }

            public class SqlFileManager
            {
                public List<SqlFile> lstSqlFiles { get; set; }
                public string GetTextFromFiles()
                {
                    StringBuilder objStrBuilder = new StringBuilder();
                    foreach (var objFile in lstSqlFiles)
                    {
                        objStrBuilder.Append(objFile.LoadText());
                    }
                    return objStrBuilder.ToString();
                }
                public void SaveTextIntoFiles()
                {
                    foreach (var objFile in lstSqlFiles)
                    {
                        //Check whether the current file object is read-only or not.If yes, skip calling it's
                        // SaveText() method to skip the exception.

                        if (objFile is not ReadOnlySqlFile)
                            objFile.SaveText();
                    }
                }
            }

        }
        

    }

    namespace Solution
    {
        public interface IReadableSqlFile
        {
            string LoadText();
        }
        public interface IWritableSqlFile
        {
            void SaveText();
        }

        public class SqlFile : IWritableSqlFile, IReadableSqlFile
        {
            public string FilePath { get; set; }
            public string FileText { get; set; }
            public string LoadText()
            {
                return "";
            }
            public void SaveText()
            {

            }
        }

        public class SqlFileManager
        {
            public string GetTextFromFiles(List<IReadableSqlFile> aLstReadableFiles)
            {
                StringBuilder objStrBuilder = new StringBuilder();
                foreach (var objFile in aLstReadableFiles)
                {
                    objStrBuilder.Append(objFile.LoadText());
                }
                return objStrBuilder.ToString();
            }
            public void SaveTextIntoFiles(List<IWritableSqlFile> aLstWritableFiles)
            {
                foreach (var objFile in aLstWritableFiles)
                {
                    objFile.SaveText();
                }
            }
        }
    }
}
