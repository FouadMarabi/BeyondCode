using SOLID._3_Liskov_Substitution_Principle_LSP.Solution;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SOLID._3_Liskov_Substitution_Principle_LSP
{
    //functions that use pointers or references to base classes must be able to use objects of derived classes without knowing it.
    // you should be able to use any derived class instead of a parent class and have it behave in the same manner without modification.

    //Importance
    //Polymorphism: Enables the use of polymorphic behavior, making code more flexible and reusable.
    //Reliability: Ensures that subclasses adhere to the contract defined by the superclass.
    //Predictability: Guarantees that replacing a superclass object with a subclass object won't break the program.

    namespace Problem
    {
        public class SqlFile
        {
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
                var objStrBuilder = new StringBuilder();
                foreach (SqlFile? objFile in _sqlFiles)
                {
                    objStrBuilder.Append(objFile.LoadText());
                }
                return objStrBuilder.ToString();
            }
            public void SaveTextIntoFiles()
            {
                foreach (SqlFile? objFile in _sqlFiles)
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
                private List<SqlFile> lstSqlFiles { get; set; }

                public SqlFileManager(List<SqlFile> lstSqlFiles)
                {
                    this.lstSqlFiles = lstSqlFiles;
                }


                public string GetTextFromFiles()
                {
                    var objStrBuilder = new StringBuilder();
                    foreach (SqlFile? objFile in lstSqlFiles)
                    {
                        objStrBuilder.Append(objFile.LoadText());
                    }
                    return objStrBuilder.ToString();
                }
                public void SaveTextIntoFiles()
                {
                    foreach (SqlFile? objFile in lstSqlFiles)
                    {
                        //Check whether the current file object is read-only or not.If yes, skip calling it's
                        // SaveText() method to skip the exception.

                        // The issue is fixed but ReadOnlySqlFile is violating LSP because it is not substitutable for SqlFile and throw exception in savetext
                        // This also violate the OCP because if we add another file type we need to modify the code
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


        public class ReadOnlySqlFile : IReadableSqlFile
        {
            public string LoadText()
            {
                return "";
            }
        }

        public class SqlFile : IWritableSqlFile, IReadableSqlFile
        {
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
                var objStrBuilder = new StringBuilder();
                foreach (IReadableSqlFile? objFile in aLstReadableFiles)
                {
                    objStrBuilder.Append(objFile.LoadText());
                }
                return objStrBuilder.ToString();
            }

            public void SaveTextIntoFiles(List<IWritableSqlFile> aLstWritableFiles)
            {
                foreach (IWritableSqlFile? objFile in aLstWritableFiles)
                {
                    objFile.SaveText();
                }
            }
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            IReadableSqlFile readOnlyFile = new ReadOnlySqlFile();
            IReadableSqlFile readOnlySqlFile = new SqlFile();
            IWritableSqlFile writableSqlFile = new SqlFile();

            var sqlFileManager = new SqlFileManager();
            sqlFileManager.GetTextFromFiles(new List<IReadableSqlFile>()
                { readOnlyFile, readOnlySqlFile }); // you cant pass in  writableSqlFile

            sqlFileManager.SaveTextIntoFiles(new List<IWritableSqlFile>()
                { writableSqlFile });
        }
    }
}
