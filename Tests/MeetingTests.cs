using Meets.BL.Entities;
using NUnit.Framework;
using System;

namespace Tests
{
    public class Tests
    {
        MeetingTime firstTime;
        MeetingTime secondTime;

        [SetUp]
        public void Setup()
        {
            var dateTimeNow = DateTime.Now;
            firstTime = MeetingTime.CreateFromDateTime(dateTimeNow.AddHours(1));
            secondTime = MeetingTime.CreateFromDateTime(dateTimeNow.AddHours(2));
        }

        [Test]
        public void MoreThan_False()
        {
            var result = firstTime >= secondTime;

            Assert.IsFalse(result);
        }

        [Test]
        public void MoreThan_True()
        {
            var result = secondTime >= firstTime;

            Assert.IsTrue(result);
        }

        [Test]
        public void LessThan_False()
        {
            var result = secondTime <= firstTime;

            Assert.IsFalse(result);
        }

        [Test]
        public void LessThan_True()
        {
            var result = firstTime <= secondTime;

            Assert.IsTrue(result);
        }

        [Test]
        public void Equals_False()
        {
            var now = DateTime.Now;

            firstTime = new MeetingTime(DateOnly.FromDateTime(now), TimeOnly.FromDateTime(now));
            secondTime = new MeetingTime(DateOnly.FromDateTime(now), TimeOnly.FromDateTime(now));

            var result = firstTime <= secondTime;

            Assert.IsFalse(result);
        }
    }
}