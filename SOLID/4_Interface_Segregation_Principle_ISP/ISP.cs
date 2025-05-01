namespace SOLID._4_Interface_Segregation_Principle_ISP
{
    //clients should not be forced to depend upon interfaces that they do not use.

    //Importance
    //Decoupling: Reduces dependencies between classes, making the code more modular and maintainable.
    //Flexibility: Allows for more targeted implementations of interfaces.
    //Avoids unnecessary dependencies: Clients don't have to depend on methods they don't use.

    namespace Problem
    {
        public interface ILead
        {
            void CreateSubTask();
            void AssignTask();
            void WorkOnTask();
        }

        public class TeamLead : ILead
        {
            public void AssignTask()
            {
                //Code to assign a task.
            }
            public void CreateSubTask()
            {
                //Code to create a sub task
            }
            public void WorkOnTask()
            {
                //Code to implement perform assigned task.
            }
        }

        namespace NewRequirement_AddManager_ManagerCantWorkOnTask
        {
            public class Manager : ILead
            {
                public void AssignTask()
                {
                    //Code to assign a task.
                }

                public void CreateSubTask()
                {
                    //Code to create a sub task.
                }

                public void WorkOnTask()
                {
                    throw new Exception("Manager can't work on Task");
                }
            }
        }
    }

    namespace Solution
    {
        public interface IProgrammer
        {
            void WorkOnTask();
        }
        public interface ILead
        {
            void AssignTask();
            void CreateSubTask();
        }
        public class Programmer : IProgrammer
        {
            public void WorkOnTask()
            {
                //code to implement to work on the Task.
            }
        }
        public class Manager : ILead
        {
            public void AssignTask()
            {
                //Code to assign a Task
            }
            public void CreateSubTask()
            {
                //Code to create a sub taks from a task.
            }
        }

        public class TeamLead : IProgrammer, ILead
        {
            public void AssignTask()
            {
                //Code to assign a Task
            }
            public void CreateSubTask()
            {
                //Code to create a sub task from a task.
            }
            public void WorkOnTask()
            {
                //code to implement to work on the Task.
            }
        }
    }


}
