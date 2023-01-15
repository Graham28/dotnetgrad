using System;
namespace dotnetgrad.tests
{
	public class ValueTests
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
            Assert.That(secondValue.Gradient, Is.EqualTo(-1.5));//Math.Pow(firstValue.Data, secondValue.Data)*Math.Log(firstValue.Data)));
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

        [Test]
        public void Backword_TanH_BrokenDown_TopOfQuoient()
        {
            //Arrange
            var firstValue = new Value(0.5);
            
            var e = firstValue.Exp();
            var minusE = firstValue.Multiply(new Value(-1)).Exp();
            var zero = new Value(0.0);
            //var minusE = zero.Subtract(firstValue).Exp();
            var topOfFraction = e.Subtract(minusE);
            topOfFraction.Gradient = 1.0;
           
            //Act
            topOfFraction.Backword();

            //Assert
            Assert.That(firstValue.Gradient, Is.EqualTo(1.6487).Within(0.0001));
            Assert.That(minusE.Gradient, Is.EqualTo(1.0).Within(0.0001));
        }

        [Test]
        public void Backword_TanH_BrokenDown_EToMinusX()
        {
            //Arrange
            var firstValue = new Value(0.5);

            var e = firstValue.Exp();
            var minusOne = new Value(-1.0);
            var minusE = firstValue.Multiply(minusOne).Exp();
            minusE.Gradient = 1.0;

            //Act
            minusE.Backword();

            //Assert
            Assert.That(firstValue.Gradient, Is.EqualTo(-0.6065).Within(0.0001));
        }

        [Test]
        public void Backword_TanH_BrokenDown_EToX()
        {
            //Arrange
            var firstValue = new Value(0.5);

            var e = firstValue.Exp();
            e.Gradient = 1.0;

            //Act
            e.Backword();

            //Assert
            Assert.That(firstValue.Gradient, Is.EqualTo(1.6487).Within(0.0001));
        }

        [Test]
        public void Backword_TanH_BrokenDown_BottomOfQuoient()
        {
            //Arrange
            var firstValue = new Value(0.5);

            var e = firstValue.Exp();
            var minusOne = new Value(-1.0);
            var minusE = firstValue.Multiply(minusOne).Exp();
            var zero = new Value(0.0);
            //var minusE = zero.Subtract(firstValue).Exp();
            var bottomOfFraction = minusE.Add(e);//e.Add(minusE);
            bottomOfFraction.Gradient = 1.0;

            //Act
            bottomOfFraction.Backword();

            //Assert
            Assert.That(firstValue.Gradient, Is.EqualTo(2.255).Within(0.0001));
        }

        [Test]
        public void Backword_TanH_BrokenDown_BottomOfQuoient_Blah()
        {
            //Arrange
            var firstValue = new Value(0.5);
            var topOfFraction = new Value(1.0);
            var e = firstValue.Exp();
            //var minusE = firstValue.Multiply(new Value(-1)).Exp();
            var minusE = new Value(0.0).Subtract(firstValue).Exp();
            var tanHOfFirstValueBrokenDown = topOfFraction.Devide(e.Add(minusE));
            tanHOfFirstValueBrokenDown.Gradient = 1.0;

            //Act
            tanHOfFirstValueBrokenDown.Backword();

            //Assert
            Assert.That(topOfFraction.Gradient, Is.EqualTo(-0.2049).Within(0.0001));
        }

        [Test]
        public void Backword_TanH_BrokenDown_Full()
        {
            //Arrange
            var firstValue = new Value(0.5);
            var e = firstValue.Exp();
            //var minusE = firstValue.Multiply(new Value(-1)).Exp();
            var minusE = new Value(0.0).Subtract(firstValue).Exp();
            var tanHOfFirstValueBrokenDown = e.Subtract(minusE).Devide(e.Add(minusE));
            tanHOfFirstValueBrokenDown.Gradient = 1.0;

            //Act
            tanHOfFirstValueBrokenDown.Backword();

            //Assert
            Assert.That(firstValue.Gradient, Is.EqualTo(0.7864).Within(0.0001));
        }


    }
}

