using BL.Student;
using EL.Student;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitTestCases
{
    [TestFixture]
    public class NUnitTestCaseDemo
    {
        CheckStudentValidations sut;

        [TestCaseSource(typeof(GetStudentValidSource))]
        public void ShouldValidateTheStudentInformation(StudentEntity student)
        {
            var result = sut.IsInformationValid(student);

            Assert.That(result, Is.True);
        }

        [SetUp]
        // to be called before any other method.
        public void InitializeSUnderTest()
        {
            sut = new CheckStudentValidations();
        }

        [TestCaseSource(typeof(GetStudentValidSource))]
        public void ShouldInsertNewStudent(StudentEntity student)
        {
            var result = sut.SaveStudent(student);

            Assert.That(result, Is.EqualTo(1));
        }

        [TestCaseSource(typeof(GetStudentValidSource))]
        public void ShouldUpdateStudent(StudentEntity student)
        {
            var result = sut.UpdateStudent(student);

            Assert.That(result, Is.EqualTo(1));
        }

        [TestCaseSource(typeof(DeleteStudentSource))]
        public void ShouldDeleteStudent(StudentEntity student)
        {
            var result = sut.DeleteStudent(student);

            Assert.That(result, Is.InRange(0,1));
        }

        [TestCaseSource(typeof(GetStudentValidSource))]
        public void ShouldSearchStudentById(StudentEntity student)
        {
            var result = sut.SearchStudentById(student);

            Assert.That(result, Is.Not.Null);
        }

        [TestCaseSource(typeof(GetStudentValidSource))]
        public void ShouldSearchStudentByName(StudentEntity student)
        {
            var result = sut.SearchStudentByName(student);

            Assert.That(result, Is.Not.Null);
        }
    }

    public class GetStudentValidSource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return new StudentEntity { Id = 18, Name = "Rajneesh", Age = 18 , Gender = 'M'};
            yield return new StudentEntity { Id = 19, Name = "RaJNEEsh", Age = 23, Gender = 'M' };
        }
    }

    public class DeleteStudentSource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return new StudentEntity { Id = 2 };
            yield return new StudentEntity { Id = 1 };
        }
    }
}
