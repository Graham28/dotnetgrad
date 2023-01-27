using System;
namespace dotnetgrad.tests
{
	public class ValueTestsBackwordProp
	{
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Backword_Addition()
        {
            //Arrange
            var firstValue = new Value(2.0);
            var secondValue = new Value(3.0);
            var firstPlusSecond = firstValue.Add(secondValue);
            firstPlusSecond.Gradient = 0.5;

            //Act
            firstPlusSecond.Backword();

            //Assert
            Assert.That(firstValue.Gradient.Equals(0.5));
            Assert.That(secondValue.Gradient.Equals(0.5));
        }

        [Test]
        public void Backword_Subtract()
        {
            //Arrange
            var firstValue = new Value(3.0);
            var secondValue = new Value(2.0);
            var firstMinusSecond = firstValue.Subtract(secondValue);
            firstMinusSecond.Gradient = 0.5;

            //Act
            firstMinusSecond.Backword();

            //Assert
            Assert.That(firstValue.Gradient.Equals(0.5));
            Assert.That(secondValue.Gradient.Equals(0.5));
        }

        [Test]
        public void Backword_Multiply()
        {
            //Arrange
            var firstValue = new Value(2.0);
            var secondValue = new Value(3.0);
            var firstTimesSecond = firstValue.Multiply(secondValue);
            firstTimesSecond.Gradient = 1.0;

            //Act
            firstTimesSecond.Backword();

            //Assert
            Assert.That(firstValue.Gradient.Equals(3.0));
            Assert.That(secondValue.Gradient.Equals(2.0));
        }

        [Test]
        public void Backword_Pow()
        {
            //Arrange
            var firstValue = new Value(2.0);
            var secondValue = new Value(3.0);
            var firstToThePowerSecond = firstValue.Pow(secondValue);
            firstToThePowerSecond.Gradient = 1.0;

            //Act
            firstToThePowerSecond.Backword();

            //Assert
            Assert.That(firstValue.Gradient, Is.EqualTo(12.0));
            Assert.That(secondValue.Gradient, Is.EqualTo(Math.Pow(firstValue.Data, secondValue.Data)*Math.Log(firstValue.Data)));
        }

        [Test]
        public void Backword_Devide()
        {
            //Arrange
            var firstValue = new Value(6.0);
            var secondValue = new Value(2.0);
            var firstToThePowerSecond = firstValue.Devide(secondValue);
            firstToThePowerSecond.Gradient = 1.0;

            //Act
            firstToThePowerSecond.Backword();

            //Assert
            Assert.That(firstValue.Gradient, Is.EqualTo(0.5));
            Assert.That(secondValue.Gradient, Is.EqualTo(-1.5));
        }

        [Test]
        public void Backword_Exp()
        {
            //Arrange
            var firstValue = new Value(0.5);
            var expOfFirstValue = firstValue.Exp();
            expOfFirstValue.Gradient = 1.0;

            //Act
            expOfFirstValue.Backword();

            //Assert
            Assert.That(firstValue.Gradient, Is.EqualTo(Math.Exp(0.5)).Within(0.0001));
        }

        [Test]
        public void Backword_TanH()
        {
            //Arrange
            var firstValue = new Value(0.5);
            var tanHOfFirstValue = firstValue.TanH();
            tanHOfFirstValue.Gradient = 1.0;

            //Act
            tanHOfFirstValue.Backword();

            //Assert
            Assert.That(firstValue.Gradient, Is.EqualTo(0.7864).Within(0.0001));
        }
    }
}

