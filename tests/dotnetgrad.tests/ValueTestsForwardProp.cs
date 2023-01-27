using System;
namespace dotnetgrad.tests
{
	public class ValueTestsForwardProp
	{
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Add()
        {
            //Arrange
            var firstValue = new Value(2.0);
            var secondValue = new Value(3.0);

            //Act
            var firstPlusSecond = firstValue.Add(secondValue);
            var secondPlusFirst = secondValue.Add(firstValue);
            //Assert
            Assert.That(firstPlusSecond.Data.Equals(5));
            Assert.That(secondPlusFirst.Data.Equals(5));
        }

        [Test]
        public void Multiply()
        {
            //Arrange
            var firstValue = new Value(2.0);
            var secondValue = new Value(3.0);

            //Act
            var firstPlusSecond = firstValue.Multiply(secondValue);
            var secondPlusFirst = secondValue.Multiply(firstValue);

            //Assert
            Assert.That(firstPlusSecond.Data.Equals(6));
            Assert.That(secondPlusFirst.Data.Equals(6));
        }

        [Test]
        public void Exp()
        {
            //Arrange
            var firstValue = new Value(2.0);

            //Act
            var expFirstValue = firstValue.Exp();
            //Assert
            Assert.That(expFirstValue.Data, Is.EqualTo(7.389).Within(0.01));
        }

        [Test]
        public void ToThePowerOf()
        {
            //Arrange
            var firstValue = new Value(2.0);
            var secondValue = new Value(3.0);

            //Act
            var firstValueToThePowerOfSecondValue = firstValue.Pow(secondValue);
            //Assert
            Assert.That(firstValueToThePowerOfSecondValue.Data, Is.EqualTo(8.0));
        }

        [Test]
        public void Devide()
        {
            //Arrange
            var firstValue = new Value(6.0);
            var secondValue = new Value(2.0);

            //Act
            var firstValueDevidedBySecondValue = firstValue.Devide(secondValue);
            //Assert
            Assert.That(firstValueDevidedBySecondValue.Data, Is.EqualTo(3.0));
        }

        [Test]
        public void Children()
        {
            //Arrange
            var firstValue = new Value(2.0);
            var secondValue = new Value(3.0);

            //Act
            var firstPlusSecond = firstValue.Add(secondValue);
            var secondPlusFirst = secondValue.Multiply(firstValue);

            //Assert
            Assert.That(firstPlusSecond.Children.Contains(firstValue));
            Assert.That(secondPlusFirst.Children.Contains(firstValue));
            Assert.That(firstPlusSecond.Children.Contains(secondValue));
            Assert.That(secondPlusFirst.Children.Contains(secondValue));
        }

        [Test]
        public void Opertion()
        {
            //Arrange
            var firstValue = new Value(2.0);
            var secondValue = new Value(3.0);

            //Act
            var firstPlusSecond = firstValue.Add(secondValue);
            var secondPlusFirst = secondValue.Multiply(firstValue);

            //Assert
            Assert.That(firstPlusSecond.Operation.Equals(Operation.Add));
            Assert.That(secondPlusFirst.Operation.Equals(Operation.Multiply));
        }
    }
}

