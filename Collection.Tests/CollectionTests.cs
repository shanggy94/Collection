using Collections;
using NUnit.Framework;
using System;
using System.Collections;
using System.Linq;

namespace Collection.Tests
{
    public class Tests
    {
   
        [Test]
        public void Test_Collection_EmptyConstructor()
        {
            //Arrange
            var nums = new Collection<int>();

            //Assert
            Assert.That(nums.ToString(), Is.EqualTo("[]"));
        }

        [Test]
        public void Test_Collection_ConstructorSingleItem()
        {
            //Arrange
            var nums = new Collection<int>(20);

            //Assert
            Assert.That(nums.ToString(), Is.EqualTo("[20]"));
        }

        [Test]
        public void Test_Collection_ConstructorMultipleItems()
        {
            //Arrange
            var nums = new Collection<int>(20, 15, 4, -8, 10);

            //Assert
            Assert.That(nums.ToString(), Is.EqualTo("[20, 15, 4, -8, 10]"));
        }

        [Test]
        public void Test_Collection_Add()
        {
            //Arrange
            var nums = new Collection<int>();

            //Act
            nums.Add(45);

            //Assert
            Assert.That(nums.ToString(), Is.EqualTo("[45]"));
        }

        [Test]
        public void Test_Collection_AddRangeWithGrow()
        {
            //Arrange
            var nums = new Collection<int>();
            int oldCapacity = nums.Capacity;
            var newNums = Enumerable.Range(1000, 2000).ToArray();

            //Act
            nums.AddRange(newNums);

            string expectedNums = "[" + string.Join(", ", newNums) + "]";

            //Assert
            Assert.That(nums.ToString(), Is.EqualTo(expectedNums));
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(oldCapacity));
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(nums.Count));
        }

        [Test]
        public void Test_Collection_GetByIndex()
        {
            //Arrange
            var names = new Collection<string>("Java", "C#");

            //Act
            var item0 = names[0];
            var item1 = names[1];

            //Assert
            Assert.That(item0, Is.EqualTo("Java"));
            Assert.That(item1, Is.EqualTo("C#"));
        }

        [Test]
        public void Test_Collection_GetByInvalidIndex()
        {
            //Arrange
            var names = new Collection<string>("Java", "C#");

            //Assert
            Assert.That(() => { var language = names[-1]; },
                Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => { var language = names[2]; },
                Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => { var language = names[500]; },
                Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(names.ToString(), Is.EqualTo("[Java, C#]"));
        }

        [Test]
        public void Test_Collection_ToStringNestedCollections()
        {
            //Arrange
            var names = new Collection<string>("Teddy", "Gerry");
            var nums = new Collection<int>(10, 20);
            var dates = new Collection<DateTime>();
            var nested = new Collection<object>(names, nums, dates);
            string nestedToString = nested.ToString();

            //Assert
            Assert.That(nestedToString,
              Is.EqualTo("[[Teddy, Gerry], [10, 20], []]"));
        }

        [Test]
        [Timeout(1000)]
        public void Test_Collection_1MillionItems()
        {
            //Arrange
            const int itemsCount = 1000000;
            var nums = new Collection<int>();

            //Act
            nums.AddRange(Enumerable.Range(1, itemsCount).ToArray());

            //Assert
            Assert.That(nums.Count == itemsCount);
            Assert.That(nums.Capacity >= nums.Count);
            for (int i = itemsCount - 1; i >= 0; i--)
                nums.RemoveAt(i);
            Assert.That(nums.ToString() == "[]");
            Assert.That(nums.Capacity >= nums.Count);
        }


    }

}
