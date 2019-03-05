using DAL.Student;
using EL.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BL.Student
{
    public class CheckStudentValidations
    {
        /// <summary>
        /// Checks if the given information is valid.
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public bool IsInformationValid(StudentEntity student)
        {
            var informationValid = false;
            if (student.Name != null)
            {
                if (Regex.IsMatch(student.Name, @"^[a-zA-Z]+$"))
                {
                    if (student.Age != 0)
                    {
                        if (Enumerable.Range(3, 30).Contains(student.Age))
                        {
                            informationValid = true;
                        }
                    }
                }
            }
            return informationValid;
        }

        /// <summary>
        /// Saveas the student information in DB.
        /// </summary>
        /// <param name="student"></param>
        public int SaveStudent(StudentEntity student)
        {
            if (IsInformationValid(student))
            {
                StudentDataOperation saveStudent = new StudentDataOperation();
                return saveStudent.SaveStudent(student);
            }
            return 0;
        }

        /// <summary>
        /// searches a student.
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public StudentEntity SearchStudentById(StudentEntity student)
        {
            if(student.Id != 0)
            {
                StudentDataOperation searchStudent = new StudentDataOperation();
                return searchStudent.SearchStudentById(student);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// searches a student.
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public StudentEntity SearchStudentByName(StudentEntity student)
        {
            if (student.Name != string.Empty)
            {
                StudentDataOperation searchStudent = new StudentDataOperation();
                List<StudentEntity> studentList = searchStudent.SearchStudentByName(student);
                if (studentList.Any())
                {
                    if(studentList.Count == 1)
                    {
                        return studentList[0];
                    }
                    else
                    {
                        studentList[0].IsMultipleRecordExist = true;
                        return studentList[0];
                    }
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Updates a student record.
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public int UpdateStudent(StudentEntity student)
        {
            if (student.Id > 0)
            {
                StudentDataOperation updateStudent = new StudentDataOperation();
                return updateStudent.UpdateStudent(student);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Deletes a student record.
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public int DeleteStudent(StudentEntity student)
        {
            if (student.Id > 0)
            {
                StudentDataOperation deleteStudent = new StudentDataOperation();
                return deleteStudent.DeleteStudent(student);
            }
            else
            {
                return 0;
            }
        }
    }
}
