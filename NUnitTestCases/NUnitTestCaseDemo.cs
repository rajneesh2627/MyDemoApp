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
        
        // to be called before any test case once
        [OneTimeSetUp]
        public void BeforeAnyTestCase()
        {
            Console.WriteLine("Before any test case");
        }

        // to be called after execution of all test cases once.
        [OneTimeTearDown]
        public void AfterAllTestCase()
        {
            Console.WriteLine("After all test cases");
        }

        // to be called before each test case.
        [SetUp]
        public void InitializeSystemUnderTest()
        {
            sut = new CheckStudentValidations();
            Console.WriteLine("Before Test {0}", TestContext.CurrentContext.Test.Name);
        }

        // It will execute after each test case.
        [TearDown]
        public void AfterTest()
        {
            sut = null;
            Console.WriteLine("After Test {0}", TestContext.CurrentContext.Test.Name);
        }

        [TestCaseSource(typeof(GetStudentValidSource))]
        [Ignore("Ignore Test")]
        public void ShouldValidateTheStudentInformation(StudentEntity student)
        {
            var result = sut.IsInformationValid(student);

            Assert.That(result, Is.True);
        }
        
        [TestCaseSource(typeof(GetStudentValidSource))]
        public void ShouldInsertNewStudent(StudentEntity student)
        {
            var result = sut.SaveStudent(student);

            Assert.That(result, Is.EqualTo(1));
        }

        // maxtime marks the maximum time allowed for a test case to complete.
        [TestCaseSource(typeof(GetStudentValidSource))]
        [MaxTime(1000)]
        public void ShouldUpdateStudent(StudentEntity student)
        {
            var result = sut.UpdateStudent(student);

            Assert.That(result, Is.EqualTo(1));
        }

        // marks another category in text explorer window.
        [TestCaseSource(typeof(DeleteStudentSource))]
        [Category("Diffrent Value Each Time")]
        public void ShouldDeleteStudent(StudentEntity student)
        {
            var result = sut.DeleteStudent(student);

            Assert.That(result, Is.EqualTo(1));
        }

        [TestCaseSource(typeof(GetStudentValidSource))]
        public void ShouldSearchStudentById(StudentEntity student)
        {
            var result = sut.SearchStudentById(student);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<StudentEntity>());
        }

        [TestCaseSource(typeof(GetStudentValidSource))]
        public void ShouldSearchStudentByName(StudentEntity student)
        {
            var result = sut.SearchStudentByName(student);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<StudentEntity>());
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
            yield return new StudentEntity { Id = 20 };
            yield return new StudentEntity { Id = 21 };
        }
    }
}
